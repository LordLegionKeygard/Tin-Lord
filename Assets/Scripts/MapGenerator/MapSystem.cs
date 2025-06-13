using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MapSystem : MonoBehaviour
{
    [Inject] private CommandCenterSaveGame _save;

    [SerializeField] private AllMissionsInfo _allMissionsInfo;
    [SerializeField] private MapGenerator _generator;
    [SerializeField] private MapVisualizer _visualizer;
    [SerializeField] private ConnectionsDrawer _drawer;
    [SerializeField] private UIPanelsCommandCenter _panels;
    [SerializeField] private MissionPanel _missionPanel;

    [Header("UI")]
    [SerializeField] private RectTransform _currentTarget;   // иконка-курсор

    private List<UINode> _uiNodes;
    private int _currentNodeIndex;

    private void OnEnable() => CustomEvents.OnDataLoad += HandleDataLoaded;
    private void OnDisable() => CustomEvents.OnDataLoad -= HandleDataLoaded;

    private void HandleDataLoaded()
    {
        var data = _save.CommandCenterSaveData;

        bool needGenerate = data.Map == null || data.Map.Nodes == null || data.Map.Nodes.Count == 0;

        if (needGenerate)
        {
            _generator.GenerateMap();
            data.Map = _generator.SavedMap;

            // стартовый узел сразу «пройден»
            data.Map.CurrentNodeIndex = 0;
            data.Map.Nodes[0].IsCompleted = true;

            _save.GetCommandCenterSaveGameDataWriter().WriteCommandCenterDataToSaveFile(data);
        }
        else
        {
            RestoreNodes(data.Map);
        }

        _drawer.DrawConnections();
        _uiNodes = _visualizer.GenerateAndDisplayMap();

        _currentNodeIndex = data.Map.CurrentNodeIndex;
        MoveTargetTo(_currentNodeIndex);
        RefreshHighlights();
        RefreshCompletedMarks();
    }

    // ---------------- публичные методы ---------------------------------------
    public void TrySelectNode(int nodeIndex)
    {
        var map = _save.CommandCenterSaveData.Map;
        bool isCurrent = nodeIndex == _currentNodeIndex;

        /* 1.  Проверяем достижимость:
               – если это тот же узел → всегда OK;
               – иначе можно только по обычным связям. */
        if (!isCurrent && !IsReachable(nodeIndex)) return;

        /* 2.  Запрет покинуть незавершённый узел
               (но проигнорировать, если кликаем по самому себе). */
        if (!isCurrent && !map.Nodes[_currentNodeIndex].IsCompleted) return;

        /* 3.  Переход на новый узел (если он не тот же самый). */
        if (!isCurrent)
        {
            _currentNodeIndex = nodeIndex;
            map.CurrentNodeIndex = nodeIndex;

            MoveTargetTo(nodeIndex);
            RefreshHighlights();
        }

        /* 4.  Если узел – миссия, показываем панель */
        if (map.Nodes[nodeIndex].NodeType == NodeType.Mission)
        {
            MissionNode missionNode = _generator.GetGeneratedNodes()[nodeIndex].nodeData as MissionNode;

            /* ---- ВАЖНО: если шаблон пустой ─ достраиваем по сейву ---- */
            if (missionNode == null || missionNode.Landscape == null)
            {
                missionNode = RebuildMissionFromSave();
                _generator.GetGeneratedNodes()[nodeIndex].nodeData = missionNode; // кеш
            }

            _missionPanel.RefreshInfo(missionNode, nodeIndex);
            _panels.MissionPanelOpen();
        }

        /* 5.  Сохраняем изменения (позиция курсора могла измениться). */
        _save.GetCommandCenterSaveGameDataWriter()
             .WriteCommandCenterDataToSaveFile(_save.CommandCenterSaveData);
    }


    /// <summary> Тестовый вызов из твоих скриптов </summary>
    public void CompleteCurrentNode()
    {
        var map = _save.CommandCenterSaveData.Map;
        var nodeSave = map.Nodes[_currentNodeIndex];

        if (nodeSave.IsCompleted) return;

        nodeSave.IsCompleted = true;
        _uiNodes[_currentNodeIndex].SetCompleted(true);

        // после завершения открываем следующие
        RefreshHighlights();

        _save.GetCommandCenterSaveGameDataWriter().WriteCommandCenterDataToSaveFile(_save.CommandCenterSaveData);
    }

    private bool IsReachable(int nodeIndex)
    {
        var curInst = _generator.GetGeneratedNodes()[_currentNodeIndex];
        var target = _generator.GetGeneratedNodes()[nodeIndex];
        return curInst.connectedNodes.Contains(target);
    }

    private void RefreshHighlights()
    {
        // 1. Сбрасываем все подсветки
        foreach (var ui in _uiNodes) ui.SetAvailable(false);

        // 2. Если текущий узел не завершён ‒ дальнейшие ноды недоступны
        var map = _save.CommandCenterSaveData.Map;
        if (!map.Nodes[_currentNodeIndex].IsCompleted) return;

        // 3. Подсвечиваем доступные
        var curInst = _generator.GetGeneratedNodes()[_currentNodeIndex];

        foreach (var target in curInst.connectedNodes)
        {
            int idx = _generator.GetGeneratedNodes().IndexOf(target);
            _uiNodes[idx].SetAvailable(true);
        }
    }

    private void RefreshCompletedMarks()
    {
        var map = _save.CommandCenterSaveData.Map;
        for (int i = 0; i < map.Nodes.Count; i++)
        {
            _uiNodes[i].SetCompleted(map.Nodes[i].IsCompleted);
        }
    }

    private void MoveTargetTo(int nodeIndex)
    {
        _currentTarget.anchoredPosition = _uiNodes[nodeIndex].GetComponent<RectTransform>().anchoredPosition;
    }

    // структурное восстановление карты после загрузки сейва (Логики миссии внутри узлов нет)
    // загружает кол-во нодов, их порядок, соденинения между и индексы миссий
    private void RestoreNodes(SavedMapData map)
    {
        var list = _generator.GetGeneratedNodes();
        list.Clear();

        foreach (var n in map.Nodes)
        {
            NodeData data;

            if (n.NodeType == NodeType.Mission &&              // ► миссия
                n.MissionIndex >= 0 &&
                n.ObjectiveIndex >= 0 &&
                n.SpawnerIndex >= 0)
            {
                // --- восстанавливаем полноценный MissionNode ---------
                var info = _allMissionsInfo;                   // ссылка есть в MapSystem
                var m = ScriptableObject.CreateInstance<MissionNode>();

                m.Landscape = info.Landscapes[n.MissionIndex];
                m.Objective = info.Objectives[n.ObjectiveIndex];
                m.EnemiesSpawner = info.EnemiesSpawnerInformation[n.SpawnerIndex];
                m.Icon = info.MissionNodeTemplate.Icon;
                m.IconColor = info.MissionNodeTemplate.IconColor;
                m.IconWidth = info.MissionNodeTemplate.IconWidth;
                m.IconHeight = info.MissionNodeTemplate.IconHeight;

                data = m;
            }
            else
            {
                // --- обычный узел либо миссия без индексов -----------
                data = _generator.GetNodeTemplate(n.NodeType);
            }

            list.Add(new NodeInstance
            {
                nodeData = data,
                layer = n.Layer,
                position = n.Position
            });
        }

        // связи — как было
        for (int i = 0; i < map.Nodes.Count; i++)
        {
            list[i].connectedNodes =
                map.Nodes[i].ConnectedNodeIndices.ConvertAll(idx => list[idx]);
        }
    }


    // восстанавливает полноценный MissionNode используя индексы из SaveMapData
    private MissionNode RebuildMissionFromSave()
    {
        var sel = _save.CommandCenterSaveData.CurrentMission;
        if (sel == null) return null;

        var info = _allMissionsInfo;

        var node = ScriptableObject.CreateInstance<MissionNode>();
        node.Landscape = info.Landscapes[sel.LandscapeId];
        node.Objective = info.Objectives[sel.ObjectiveId];
        node.EnemiesSpawner = info.EnemiesSpawnerInformation[sel.SpawnerId];
        node.Icon = info.MissionNodeTemplate.Icon;
        node.IconColor = info.MissionNodeTemplate.IconColor;
        node.IconWidth = info.MissionNodeTemplate.IconWidth;
        node.IconHeight = info.MissionNodeTemplate.IconHeight;

        return node;
    }

}
