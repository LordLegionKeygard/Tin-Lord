using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MapSystem : MonoBehaviour
{
    [Inject] private CommandCenterSaveGame _save;

    [SerializeField] private MapDragScroller _mapScroller;
    [SerializeField] private EventNodePanel _eventPanel;
    [SerializeField] private CosmosView _cosmosView;
    [SerializeField] private AllNodesInfo _allMissionsInfo;
    [SerializeField] private MapGenerator _generator;
    [SerializeField] private MapVisualizer _visualizer;
    [SerializeField] private ConnectionsDrawer _drawer;
    [SerializeField] private UIPanelsCommandCenter _panels;
    [SerializeField] private MissionPanel _missionPanel;

    [Header("UI")]
    [SerializeField] private RectTransform _currentTarget;
    private List<UINode> _uiNodes;
    private readonly HashSet<int> _reachable = new();
    private int _currentNodeIndex;

    public bool IsCurrent(int nodeIdx) => nodeIdx == _currentNodeIndex;

    public bool IsVisited(int nodeIdx)
    {
        var map = _save.CommandCenterSaveData.Map;
        return map != null &&
               map.Nodes != null &&
               nodeIdx >= 0 &&
               nodeIdx < map.Nodes.Count &&
               map.Nodes[nodeIdx].IsCompleted;
    }

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
        ApplyCosmos();

        if (data.Map.Nodes[_currentNodeIndex].NodeType == NodeType.Mission)
        {
            var missionNode = _generator.GetGeneratedNodes()[_currentNodeIndex].nodeData as MissionNode;

            if (missionNode == null || missionNode.Landscape == null)
            {
                missionNode = RebuildMissionFromSave();
                _generator.GetGeneratedNodes()[_currentNodeIndex].nodeData = missionNode;
            }

            _missionPanel.RefreshInfo(missionNode, _currentNodeIndex);
            _panels.MissionPanelOpen(false);
        }
    }

    public void TrySelectNode(int nodeIndex)
    {
        var map = _save.CommandCenterSaveData.Map;
        bool isCurrent = nodeIndex == _currentNodeIndex;

        if (!isCurrent && !IsReachable(nodeIndex)) return;
        if (!isCurrent && !map.Nodes[_currentNodeIndex].IsCompleted) return;

        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.Warp, transform.position);

        if (!isCurrent)
        {
            _currentNodeIndex = nodeIndex;
            map.CurrentNodeIndex = nodeIndex;
            MoveTargetTo(nodeIndex);
            RefreshHighlights();
        }

        if (map.Nodes[nodeIndex].NodeType == NodeType.Mission)
        {
            var missionNode = _generator.GetGeneratedNodes()[nodeIndex].nodeData as MissionNode;

            if (missionNode == null || missionNode.Landscape == null)
            {
                missionNode = RebuildMissionFromSave();
                _generator.GetGeneratedNodes()[nodeIndex].nodeData = missionNode;
            }

            _missionPanel.RefreshInfo(missionNode, nodeIndex);
            _panels.MissionPanelOpen(true);
        }
        if (map.Nodes[nodeIndex].NodeType == NodeType.Event)
        {
            var eventNode = _generator.GetGeneratedNodes()[nodeIndex].nodeData as EventNode;
            int sequenceIndex = map.Nodes[nodeIndex].EventSequenceIndex;
            _eventPanel.Open(eventNode.Dialogue[sequenceIndex]);
            _panels.EventPanelOpen();
        }
        if (map.Nodes[nodeIndex].NodeType == NodeType.RewardEvent)
        {
            var healNode = _generator.GetGeneratedNodes()[nodeIndex].nodeData as RewardEventNode;
            _eventPanel.Open(healNode.Dialogue);
            _panels.EventPanelOpen();
        }

        ApplyCosmos();
        _save.GetCommandCenterSaveGameDataWriter().WriteCommandCenterDataToSaveFile(_save.CommandCenterSaveData);
    }

    private void ApplyCosmos()
    {
        var nodeData = _generator.GetGeneratedNodes()[_currentNodeIndex].nodeData;
        var save = _save.CommandCenterSaveData.Map.Nodes[_currentNodeIndex];
        var variations = nodeData.CosmosVariations;

        if (nodeData is EventNode eventNode && eventNode.EachCosmosForEachDialogue)
        {
            // Привязываем строго: один-к-одному по индексу диалога
            int cosmosId = save.EventSequenceIndex;
            // сохраняем в map
            save.CosmosIndex = cosmosId;
            // и сразу рисуем точно его
            _cosmosView.ChangeCosmos(variations, cosmosId);
        }
        else
        {
            int cosmosId = save.CosmosIndex;
            cosmosId = _cosmosView.ChangeCosmos(variations, cosmosId);
            save.CosmosIndex = cosmosId;
        }
    }

    // Тестовый вызов
    public void CompleteCurrentNode()
    {
        var map = _save.CommandCenterSaveData.Map;
        var nodeSave = map.Nodes[_currentNodeIndex];

        if (nodeSave.IsCompleted) return;

        nodeSave.IsCompleted = true;
        _uiNodes[_currentNodeIndex].SetCompleted(true);

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
        _drawer.ResetAllLineColors();
        _reachable.Clear();

        foreach (var ui in _uiNodes) ui.SetAvailable(false);

        var map = _save.CommandCenterSaveData.Map;
        if (!map.Nodes[_currentNodeIndex].IsCompleted) return;

        var cur = _generator.GetGeneratedNodes()[_currentNodeIndex];

        foreach (var t in cur.connectedNodes)
        {
            int idx = _generator.GetGeneratedNodes().IndexOf(t);
            _uiNodes[idx].SetAvailable(true);
            _reachable.Add(idx);
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
        _currentTarget.SetAsLastSibling();
    }

    // структурное восстановление карты после загрузки сейва (Логики миссии внутри узлов нет)
    // загружает кол-во нодов, их порядок, соденинения между и индексы миссий
    private void RestoreNodes(SavedMapData map)
    {
        var list = _generator.GetGeneratedNodes();
        list.Clear();

        for (int i = 0; i < map.Nodes.Count; i++)
        {
            var n = map.Nodes[i];
            NodeData data;

            switch (n.NodeType)
            {
                case NodeType.Mission:
                    // Восстанавливаем миссию по сохранённым индексам
                    var info = _allMissionsInfo;
                    var mNode = ScriptableObject.CreateInstance<MissionNode>();
                    mNode.Landscape = info.Landscapes[n.MissionIndex];
                    mNode.Objective = info.Objectives[n.ObjectiveIndex];
                    mNode.EnemiesSpawner = info.EnemiesSpawnerInformation[n.SpawnerIndex];
                    mNode.Icon = info.MissionNodeTemplate.Icon;
                    mNode.IconColor = info.MissionNodeTemplate.IconColor;
                    mNode.IconWidth = info.MissionNodeTemplate.IconWidth;
                    mNode.IconHeight = info.MissionNodeTemplate.IconHeight;
                    mNode.CosmosVariations = mNode.Landscape.CosmosVariations;
                    mNode.DescriptionTextNumber = info.MissionNodeTemplate.DescriptionTextNumber;
                    data = mNode;
                    break;

                case NodeType.Event:
                case NodeType.RewardEvent:
                    // Берём нод из правильного EventPool
                    if (n.EventPoolIndex >= 0 && n.EventPoolIndex < _allMissionsInfo.EventPools.Length)
                    {
                        var pool = _allMissionsInfo.EventPools[n.EventPoolIndex];
                        data = pool.Node;
                    }
                    else
                    {
                        // fallback на шаблон
                        data = _generator.GetNodeTemplate(n.NodeType);
                    }
                    break;

                default:
                    // Старт, трейдеры, босс и т.д.
                    data = _generator.GetNodeTemplate(n.NodeType);
                    break;
            }

            list.Add(new NodeInstance
            {
                nodeData = data,
                layer = n.Layer,
                position = n.Position,
                connectedNodes = new List<NodeInstance>()
            });
        }

        // Восстановливаем связи
        for (int i = 0; i < map.Nodes.Count; i++)
        {
            list[i].connectedNodes =
                map.Nodes[i].ConnectedNodeIndices
                    .ConvertAll(idx => list[idx]);
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
        node.CosmosVariations = node.Landscape.CosmosVariations;

        return node;
    }

    public void OnHoverNode(int nodeIdx, bool enter)
    {
        if (!_reachable.Contains(nodeIdx)) return;

        _uiNodes[nodeIdx].SetOnPointerColor(enter);
        _drawer.SetLineHighlight(_currentNodeIndex, nodeIdx, enter);
    }

    public void FocusOnCurrentNode()
    {
        if (_uiNodes == null || _uiNodes.Count == 0) return;

        var curRect = _uiNodes[_currentNodeIndex].GetComponent<RectTransform>();
        _mapScroller.JumpTo(curRect);
    }
}
