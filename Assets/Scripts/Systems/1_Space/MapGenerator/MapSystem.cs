using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class MapSystem : MonoBehaviour
{
    [Inject] private SpaceSaveGame _save;

    [SerializeField] private MapDragScroller _mapScroller;
    [SerializeField] private EventNodePanel _eventPanel;
    [SerializeField] private CosmosView _cosmosView;
    [SerializeField] private AllNodesInfo _allMissionsInfo;
    [SerializeField] private MapGenerator _generator;
    [SerializeField] private MapVisualizer _visualizer;
    [SerializeField] private ConnectionsDrawer _drawer;
    [SerializeField] private UIPanelsSpace _panels;
    [SerializeField] private MissionPanel _missionPanel;
    private TraderKind _activeTraderKind;

    [Header("UI")]
    [SerializeField] private RectTransform _currentTarget;
    private List<UINode> _uiNodes;
    private readonly HashSet<int> _reachable = new();
    private int _currentNodeIndex;

    public bool IsCurrent(int nodeIdx) => nodeIdx == _currentNodeIndex;

    public bool IsVisited(int nodeIdx)
    {
        var map = _save.SpaceSaveData.Map;
        return map != null && map.Nodes != null && nodeIdx >= 0 && nodeIdx < map.Nodes.Count && map.Nodes[nodeIdx].IsCompleted;
    }

    private void OnEnable() => CustomEvents.OnDataLoad += HandleDataLoaded;
    private void OnDisable() => CustomEvents.OnDataLoad -= HandleDataLoaded;

    private void HandleDataLoaded()
    {
        var data = _save.SpaceSaveData;

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

        var node = data.Map.Nodes[_currentNodeIndex];
        if ((node.NodeType == NodeType.Mission || node.NodeType == NodeType.Boss) && !node.IsCompleted)
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
        var map = _save.SpaceSaveData.Map;
        if (map == null || map.Nodes == null || nodeIndex < 0 || nodeIndex >= map.Nodes.Count) return;

        bool isCurrent = nodeIndex == _currentNodeIndex;
        var nodeType = map.Nodes[nodeIndex].NodeType;

        if (!isCurrent && (!IsReachable(nodeIndex) || !map.Nodes[_currentNodeIndex].IsCompleted)) return;

        if (nodeType == NodeType.Mission && map.Nodes[nodeIndex].IsCompleted) return;

        if (!isCurrent)
        {
            _currentNodeIndex = nodeIndex;
            map.CurrentNodeIndex = nodeIndex;

            MoveTargetTo(nodeIndex);
            RefreshHighlights();
        }

        switch (nodeType)
        {
            case NodeType.Boss:
            case NodeType.Mission:
                {
                    var save = map.Nodes[nodeIndex];

                    if (save.MissionDeckIndex < 0)
                    {
                        int completed = GetCompletedMissionsCount();
                        int deckIdx = Mathf.Clamp(completed, 0, _allMissionsInfo.MissionDeck.Length - 1);

                        var mission = BuildMissionRandom(deckIdx, isBossNode: nodeType == NodeType.Boss, out int landscapeIdx, out ObjectiveSave[] chosenObj);

                        save.MissionDeckIndex = deckIdx;
                        save.SavedLandscapeIndex = landscapeIdx;
                        save.SavedObjectives = chosenObj;

                        _generator.GetGeneratedNodes()[nodeIndex].nodeData = mission;
                    }

                    var mNode = _generator.GetGeneratedNodes()[nodeIndex].nodeData as MissionNode;

                    _missionPanel.RefreshInfo(mNode, nodeIndex);
                    _panels.MissionPanelOpen(true);
                    break;
                }
            case NodeType.Event:
                {
                    var evNode = _generator.GetGeneratedNodes()[nodeIndex].nodeData as EventNode;
                    int seq = map.Nodes[nodeIndex].EventSequenceIndex;

                    _eventPanel.Open(evNode.Dialogue[seq]);
                    _panels.EventPanelOpen();
                    break;
                }
            case NodeType.RewardEvent:
                {
                    var rNode = _generator.GetGeneratedNodes()[nodeIndex].nodeData
                                as RewardEventNode;

                    _eventPanel.Open(rNode.Dialogue);
                    _panels.EventPanelOpen();
                    break;
                }
            case NodeType.ResourceTrader:
            case NodeType.SkillTrader:
                {
                    var trader = _generator.GetGeneratedNodes()[nodeIndex].nodeData as BaseTraderNode;
                    OpenTrader(trader, nodeIndex);
                    break;
                }
        }

        if (!isCurrent) AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.Warp, transform.position);
        ApplyCosmos();
        _save.GetCommandCenterSaveGameDataWriter().WriteCommandCenterDataToSaveFile(_save.SpaceSaveData);
    }

    private void OpenTrader(BaseTraderNode node, int nodeIndex)
    {
        var map = _save.SpaceSaveData.Map;

        if (map.Nodes[nodeIndex].IsCompleted && nodeIndex == _currentNodeIndex)
        {
            _panels.OpenTraderPanel(node.TraderKind);
            return;
        }

        map.Nodes[nodeIndex].IsCompleted = true;
        _uiNodes[nodeIndex].SetCompleted(true);
        RefreshHighlights();

        _activeTraderKind = node.TraderKind;

        _eventPanel.OnChoiceSelected -= HandleTraderChoice;
        _eventPanel.OnChoiceSelected += HandleTraderChoice;

        _eventPanel.Open(node.Dialogue);
        _panels.EventPanelOpen();
    }

    private void HandleTraderChoice(int idx)
    {
        _eventPanel.OnChoiceSelected -= HandleTraderChoice;
        _eventPanel.Close();

        if (idx != 0) return;

        _panels.OpenTraderPanel(_activeTraderKind);
    }


    private void ApplyCosmos()
    {
        var nodeData = _generator.GetGeneratedNodes()[_currentNodeIndex].nodeData;
        var save = _save.SpaceSaveData.Map.Nodes[_currentNodeIndex];
        var variations = nodeData.CosmosVariations;

        if (nodeData is EventNode eventNode && eventNode.EachCosmosForEachDialogue)
        {
            int cosmosId = save.EventSequenceIndex;
            save.CosmosIndex = cosmosId;
            _cosmosView.ChangeCosmos(variations, cosmosId);
        }
        else
        {
            int cosmosId = save.CosmosIndex;
            cosmosId = _cosmosView.ChangeCosmos(variations, cosmosId);
            save.CosmosIndex = cosmosId;
        }
    }

    public void CompleteCurrentNode()
    {
        var map = _save.SpaceSaveData.Map;
        var nodeSave = map.Nodes[_currentNodeIndex];

        if (nodeSave.IsCompleted) return;

        nodeSave.IsCompleted = true;
        _uiNodes[_currentNodeIndex].SetCompleted(true);

        RefreshHighlights();

        _save.GetCommandCenterSaveGameDataWriter().WriteCommandCenterDataToSaveFile(_save.SpaceSaveData);
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

        var map = _save.SpaceSaveData.Map;
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
        var map = _save.SpaceSaveData.Map;
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
                case NodeType.Boss:
                    if (n.MissionDeckIndex >= 0 && n.SavedLandscapeIndex >= 0 && n.SavedObjectives != null && n.SavedObjectives.Length > 0)
                    {
                        data = BuildMissionFixed(n.MissionDeckIndex, n.SavedLandscapeIndex, n.SavedObjectives, true);
                    }
                    else
                    {
                        data = _generator.GetNodeTemplate(NodeType.Boss);
                    }
                    break;
                case NodeType.Mission:
                    if (n.MissionDeckIndex >= 0 && n.SavedLandscapeIndex >= 0 && n.SavedObjectives != null && n.SavedObjectives.Length > 0)
                    {
                        data = BuildMissionFixed(n.MissionDeckIndex, n.SavedLandscapeIndex, n.SavedObjectives, false);
                    }
                    else
                    {
                        data = _generator.GetNodeTemplate(NodeType.Mission);
                    }
                    break;
                case NodeType.Event:
                case NodeType.RewardEvent:
                    if (n.EventPoolIndex >= 0 && n.EventPoolIndex < _allMissionsInfo.EventPools.Length)
                    {
                        data = _allMissionsInfo.EventPools[n.EventPoolIndex].Node;
                    }
                    else
                    {
                        data = _generator.GetNodeTemplate(n.NodeType);
                    }
                    break;
                default:
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

        for (int i = 0; i < map.Nodes.Count; i++)
        {
            list[i].connectedNodes = map.Nodes[i].ConnectedNodeIndices.ConvertAll(idx => list[idx]);
        }
    }


    private int GetCompletedMissionsCount()
    {
        var map = _save.SpaceSaveData.Map;
        return map.Nodes.Count(n => n.NodeType == NodeType.Mission && n.IsCompleted);
    }

    // Восстанавливает MissionNode для «текущей миссии» при загрузке мира.
    private MissionNode RebuildMissionFromSave()
    {
        var currentMission = _save.SpaceSaveData.CurrentMission;
        if (currentMission == null) return null;

        var info = _allMissionsInfo;
        int deckIndex = currentMission.MissionDeckIndex;
        int landscapeIndex = currentMission.LandscapeId;

        if (deckIndex < 0 || deckIndex >= info.MissionDeck.Length) return null;
        if (landscapeIndex < 0 || landscapeIndex >= info.Landscapes.Length) return null;
        if (currentMission.SavedObjectives == null || currentMission.SavedObjectives.Length == 0) return null;

        var definition = info.MissionDeck[deckIndex];
        var landscape = info.Landscapes[landscapeIndex];

        var spawnerSO = definition.Spawner;
        var objectiveSO = BuildObjectiveFromSave(currentMission.SavedObjectives);
        var template = info.MissionNodeTemplate;
        var node = ScriptableObject.CreateInstance<MissionNode>();

        node.Landscape = landscape;
        node.EnemiesSpawner = spawnerSO;
        node.Objective = objectiveSO;

        node.Icon = template.Icon;
        node.IconColor = template.IconColor;
        node.IconWidth = template.IconWidth;
        node.IconHeight = template.IconHeight;
        node.DescriptionTextNumber = template.DescriptionTextNumber;
        node.CosmosVariations = landscape.CosmosVariations;

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

    private Objective BuildObjectiveFromRandomSet(MissionDefinition def, out ObjectiveSave[] saved)
    {
        var pickedSet = def.ObjectiveSets[Random.Range(0, def.ObjectiveSets.Length)];

        int objectivesLength = pickedSet.Objectives.Length;
        var wrappers = new ObjectiveWrapper[objectivesLength];
        saved = new ObjectiveSave[objectivesLength];

        for (int i = 0; i < objectivesLength; i++)
        {
            var range = pickedSet.Objectives[i];
            int amount;

            amount = range.Values[Random.Range(0, range.Values.Length)];

            wrappers[i] = new ObjectiveWrapper
            {
                ObjectiveEnum = range.ObjectiveEnum,
                ObjectiveAmount = amount
            };

            saved[i] = new ObjectiveSave
            {
                Objective = range.ObjectiveEnum,
                Amount = amount
            };
        }

        var obj = ScriptableObject.CreateInstance<Objective>();
        obj.Objectives = wrappers;
        return obj;
    }

    // Восстанавливаем Objective из сохранённого массива
    private Objective BuildObjectiveFromSave(ObjectiveSave[] saved)
    {
        var wrappers = new ObjectiveWrapper[saved.Length];
        for (int i = 0; i < saved.Length; i++)
        {
            wrappers[i] = new ObjectiveWrapper
            {
                ObjectiveEnum = saved[i].Objective,
                ObjectiveAmount = saved[i].Amount
            };
        }

        var obj = ScriptableObject.CreateInstance<Objective>();
        obj.Objectives = wrappers;
        return obj;
    }

    // Строит Mission-/Boss-Node для случайной планеты
    private MissionNode BuildMissionRandom(int deckIdx, bool isBossNode, out int landscapeIdx, out ObjectiveSave[] savedObj)
    {
        var definition = _allMissionsInfo.MissionDeck[deckIdx];
        var tplMission = _allMissionsInfo.MissionNodeTemplate;
        var tplBoss = _allMissionsInfo.BossNode;

        landscapeIdx = PickUniqueLandscape();
        var landscape = _allMissionsInfo.Landscapes[landscapeIdx];
        var spawnerSO = definition.Spawner;

        Objective objectiveSO;
        if (isBossNode)
        {
            objectiveSO = ScriptableObject.CreateInstance<Objective>();
            objectiveSO.Objectives = new[] { new ObjectiveWrapper { ObjectiveEnum = ObjectiveEnum.KillBoss, ObjectiveAmount = 1 } };
            savedObj = new[] { new ObjectiveSave { Objective = ObjectiveEnum.KillBoss, Amount = 1 } };
        }
        else
        {
            objectiveSO = BuildObjectiveFromRandomSet(definition, out savedObj);
        }

        var node = isBossNode ? ScriptableObject.CreateInstance<BossNode>() : ScriptableObject.CreateInstance<MissionNode>();

        node.Landscape = landscape;
        node.EnemiesSpawner = spawnerSO;
        node.Objective = objectiveSO;

        var tpl = isBossNode ? tplBoss : tplMission;
        node.Icon = tpl.Icon;
        node.IconColor = tpl.IconColor;
        node.IconWidth = tpl.IconWidth;
        node.IconHeight = tpl.IconHeight;
        node.DescriptionTextNumber = tpl.DescriptionTextNumber;
        node.CosmosVariations = landscape.CosmosVariations;

        return node;
    }


    // Восстанавливает Mission-/Boss-Node из сейва (без RNG)
    private MissionNode BuildMissionFixed(int deckIndex, int landscapeIdx, ObjectiveSave[] savedObj, bool isBossNode)
    {
        var definition = _allMissionsInfo.MissionDeck[deckIndex];
        var tplMission = _allMissionsInfo.MissionNodeTemplate;
        var tplBoss = _allMissionsInfo.BossNode;

        landscapeIdx = Mathf.Clamp(landscapeIdx, 0, _allMissionsInfo.Landscapes.Length - 1);
        var landscape = _allMissionsInfo.Landscapes[landscapeIdx];
        var spawnerSO = definition.Spawner;
        var objectiveSO = BuildObjectiveFromSave(savedObj);
        var node = isBossNode ? ScriptableObject.CreateInstance<BossNode>() : ScriptableObject.CreateInstance<MissionNode>();

        node.Landscape = landscape;
        node.EnemiesSpawner = spawnerSO;
        node.Objective = objectiveSO;

        var tpl = isBossNode ? tplBoss : tplMission;
        node.Icon = tpl.Icon;
        node.IconColor = tpl.IconColor;
        node.IconWidth = tpl.IconWidth;
        node.IconHeight = tpl.IconHeight;
        node.DescriptionTextNumber = tpl.DescriptionTextNumber;
        node.CosmosVariations = landscape.CosmosVariations;

        return node;
    }

    // возвращает индекс ландшафта, который ещё не использовался
    private int PickUniqueLandscape()
    {
        var landscapes = _allMissionsInfo.Landscapes;

        // 1) какие уже заняты?
        HashSet<int> used = new();
        foreach (var n in _save.SpaceSaveData.Map.Nodes)
        {
            if (n.NodeType == NodeType.Mission && n.SavedLandscapeIndex >= 0)
            {
                used.Add(n.SavedLandscapeIndex);
            }
        }

        // 2) формируем список свободных
        List<int> free = new();
        for (int i = 0; i < landscapes.Length; i++)
        {
            if (!used.Contains(i)) free.Add(i);
        }

        // 3) если всё использовано — разрешаем повторения
        if (free.Count == 0)
        {
            for (int i = 0; i < landscapes.Length; i++)
            {
                free.Add(i);
            }
        }

        return free[Random.Range(0, free.Count)];
    }
}
