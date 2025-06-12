using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MapSystem : MonoBehaviour
{
    [Inject] private CommandCenterSaveGame _save;

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

            _save.GetCommandCenterSaveGameDataWriter()
                 .WriteCommandCenterDataToSaveFile(data);
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
        if (!IsReachable(nodeIndex)) return;

        var map = _save.CommandCenterSaveData.Map;

        // Нельзя уходить, если текущий узел ещё не завершён
        if (!map.Nodes[_currentNodeIndex].IsCompleted) return;

        _currentNodeIndex = nodeIndex;
        map.CurrentNodeIndex = nodeIndex;

        MoveTargetTo(nodeIndex);
        RefreshHighlights();

        if (map.Nodes[nodeIndex].NodeType == NodeType.Mission)
        {
            var missionNode = (MissionNode)_generator.GetGeneratedNodes()[nodeIndex].nodeData;

            _missionPanel.RefreshInfo(missionNode, nodeIndex); 
            _panels.MissionPanelToggle(); 
        }

        _save.GetCommandCenterSaveGameDataWriter().WriteCommandCenterDataToSaveFile(_save.CommandCenterSaveData);
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


    /* ----- восстановление NodeInstance-ов из SavedMapData -------- */
    private void RestoreNodes(SavedMapData map)
    {
        var list = _generator.GetGeneratedNodes();
        list.Clear();

        foreach (var n in map.Nodes)
        {
            list.Add(new NodeInstance
            {
                nodeData = _generator.GetNodeTemplate(n.NodeType),
                layer = n.Layer,
                position = n.Position
            });
        }
        for (int i = 0; i < map.Nodes.Count; i++)
        {
            list[i].connectedNodes = map.Nodes[i].ConnectedNodeIndices.ConvertAll(idx => list[idx]);
        }
    }
}
