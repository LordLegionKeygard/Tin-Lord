using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class MapSystem : MonoBehaviour
{
    [Inject] private SpaceSaveGame _save;
    [SerializeField] private ActInfo[] _actsInfo;
    [SerializeField] private MapDragScroller _mapScroller;
    [SerializeField] private EventNodePanel _eventPanel;
    [SerializeField] private CosmosView _cosmosView;
    [SerializeField] private MapGenerator _generator;
    [SerializeField] private MapVisualizer _visualizer;
    [SerializeField] private ConnectionsDrawer _drawer;
    [SerializeField] private UIPanelsSpace _panels;
    [SerializeField] private MissionPanel _missionPanel;
    private TraderKind _activeTraderKind;
    private List<EventEntry> _eventQueue = new();
    private List<ResourceTraderNode> _resourceTraders = new();
    private List<SkillTraderNode> _skillTraders = new();
    private List<WeaponEngineerNode> _weaponEngineers = new();

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

    private void Start()
    {
        CustomEvents.OnDataLoad += LoadMapData;
    }

    private void LoadMapData()
    {
        _generator.SetActInfo(_actsInfo[_save.SpaceSaveData.Act]);

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
        _uiNodes = _visualizer.GenerateAndDisplayMap(_save.SpaceSaveData.Act);

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

        InitSpawnQueues();
        UpdateMapVisual();
    }

    private void InitSpawnQueues()
    {
        // обычные Event’ы (без RewardEvent)
        _eventQueue = MapHelper.BuildEventEntries(_actsInfo[_save.SpaceSaveData.Act].EventPools).Where(e => !(e.Node is RewardEventNode)).ToList();

        // Traders
        _resourceTraders = new List<ResourceTraderNode>(_actsInfo[_save.SpaceSaveData.Act].ResourceTraders);
        _skillTraders = new List<SkillTraderNode>(_actsInfo[_save.SpaceSaveData.Act].SkillTraders);
        _weaponEngineers = new List<WeaponEngineerNode>(_actsInfo[_save.SpaceSaveData.Act].WeaponEngineers);

        _eventQueue.Shuffle();
        _resourceTraders.Shuffle();
        _skillTraders.Shuffle();
        _weaponEngineers.Shuffle();

        // убрать уже размещённых
        RemoveOpenNodeOnMap();
    }

    private void RemoveOpenNodeOnMap()
    {
        var map = _save.SpaceSaveData.Map;

        _eventQueue = _eventQueue.Where(e => !map.Nodes.Any(n => n.NodeType == NodeType.RewardEvent && n.EventPoolIndex == e.PoolIndex && n.EventSequenceIndex == e.SequenceIndex)).ToList();
        _resourceTraders = _resourceTraders.Where(tr => !map.Nodes.Any(n => n.NodeType == NodeType.ResourceTrader && _generator.GetGeneratedNodes()[n.NodeIndex].nodeData == tr)).ToList();
        _skillTraders = _skillTraders.Where(tr => !map.Nodes.Any(n => n.NodeType == NodeType.SkillTrader && _generator.GetGeneratedNodes()[n.NodeIndex].nodeData == tr)).ToList();
        _weaponEngineers = _weaponEngineers.Where(tr => !map.Nodes.Any(n => n.NodeType == NodeType.WeaponEngineer && _generator.GetGeneratedNodes()[n.NodeIndex].nodeData == tr)).ToList();
    }


    public void TrySelectNode(int nodeIndex)
    {
        var map = _save.SpaceSaveData.Map;
        if (map == null || map.Nodes == null ||
            nodeIndex < 0 || nodeIndex >= map.Nodes.Count) return;

        bool isCurrent = nodeIndex == _currentNodeIndex;
        var nodeType = map.Nodes[nodeIndex].NodeType;
        bool isTrader = nodeType is NodeType.ResourceTrader or NodeType.SkillTrader or NodeType.WeaponEngineer;

        if (map.Nodes[nodeIndex].IsCompleted && !isTrader) return;

        if (!isCurrent && (!IsReachable(nodeIndex) || !map.Nodes[_currentNodeIndex].IsCompleted)) return;

        bool isVisibleNonMission = nodeType is NodeType.ResourceTrader or NodeType.SkillTrader or NodeType.RewardEvent or NodeType.WeaponEngineer; ;

        if (isVisibleNonMission)
        {
            var seq = _actsInfo[_save.SpaceSaveData.Act].MapPattern.Sequence;
            int curSymId = map.PatternIndex % seq.Length;

            if (seq[curSymId] == MapPatternEnum.NonMission)
            {
                // съедаем ОДИН ожидаемый NON
                map.PatternIndex++;

                // необходим 50 % дополнительный «штраф» если паттерн non/non/mission
                // if (Random.value < 0.5f)
                // map.PatternIndex++;
            }
        }
        if (!isCurrent)
        {
            _currentNodeIndex = nodeIndex;
            map.CurrentNodeIndex = nodeIndex;

            MoveTargetTo(nodeIndex);
            RefreshHighlights();
        }
        if (nodeType == NodeType.None)
        {
            ResolveUnknownNode(nodeIndex);
            nodeType = map.Nodes[nodeIndex].NodeType;
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
                        int deckIdx = Mathf.Clamp(completed, 0, _actsInfo[_save.SpaceSaveData.Act].MissionDeck.Length - 1);

                        var mission = BuildMissionRandom(deckIdx, nodeType == NodeType.Boss, out int landIdx, out ObjectiveSave[] obj);

                        save.MissionDeckIndex = deckIdx;
                        save.SavedLandscapeIndex = landIdx;
                        save.SavedObjectives = obj;

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
                    var rNode = _generator.GetGeneratedNodes()[nodeIndex].nodeData as RewardEventNode;
                    _eventPanel.Open(rNode.Dialogue);
                    _panels.EventPanelOpen();
                    break;
                }
            case NodeType.ResourceTrader:
            case NodeType.SkillTrader:
            case NodeType.WeaponEngineer:
                {
                    var trader = _generator.GetGeneratedNodes()[nodeIndex].nodeData as BaseTraderNode;
                    OpenTrader(trader, nodeIndex);
                    break;
                }
        }
        if (!isCurrent) AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.Warp, transform.position);

        ApplyCosmos();
        _save.GetCommandCenterSaveGameDataWriter().WriteCommandCenterDataToSaveFile(_save.SpaceSaveData);

        UpdateMapVisual();
    }

    private void UpdateMapVisual()
    {
        _visualizer.UpdateMapProgressText(_save.SpaceSaveData.Act, _save.SpaceSaveData.Map.Nodes.Count(n => n.IsCompleted));
    }

    private void ResolveUnknownNode(int nodeIndex)
    {
        var map = _save.SpaceSaveData.Map;
        var pattern = _actsInfo[_save.SpaceSaveData.Act].MapPattern.Sequence;

        var symbol = pattern[map.PatternIndex % pattern.Length];
        map.PatternIndex++;

        switch (symbol)
        {
            case MapPatternEnum.Mission:
                {
                    int deckIdx = Mathf.Clamp(GetCompletedMissionsCount(), 0, _actsInfo[_save.SpaceSaveData.Act].MissionDeck.Length - 1);

                    var mNode = BuildMissionRandom(deckIdx, false, out int landIdx, out ObjectiveSave[] obj);

                    ReplacePlaceholder(nodeIndex, mNode, NodeType.Mission, deckIdx, landIdx, obj);
                    break;
                }
            default:
                {
                    if (MapHelper.TryPickNonMission(_eventQueue, _resourceTraders, _skillTraders, _weaponEngineers, out NodeData nd, out NodeType nt, out int pool, out int seq))
                    {
                        ReplacePlaceholder(nodeIndex, nd, nt, eventPool: pool, eventSeq: seq);
                    }
                    else
                    {
                        // если и Event'ов и Trader'ов совсем не осталось —
                        // паттерн превращаем в миссию
                        goto case MapPatternEnum.Mission;
                    }
                    break;
                }
        }
    }

    /// <summary>
    /// Полностью заменяет плейсхолдер (NodeType.None) на конкретный узел.
    /// Записывает данные и в «живую» карту, и в сохранённую структуру.
    /// </summary>
    private void ReplacePlaceholder(int nodeIndex, NodeData data, NodeType newType, int deckIdx = -1, int landscapeIdx = -1, ObjectiveSave[] savedObj = null, int eventPool = -1, int eventSeq = -1)
    {
        var inst = _generator.GetGeneratedNodes()[nodeIndex];
        inst.nodeData = data;

        _uiNodes[nodeIndex].Setup(data, nodeIndex, this);

        var saveNode = _save.SpaceSaveData.Map.Nodes[nodeIndex];
        saveNode.NodeType = newType;
        saveNode.MissionDeckIndex = deckIdx;
        saveNode.SavedLandscapeIndex = landscapeIdx;
        saveNode.SavedObjectives = savedObj;
        saveNode.EventPoolIndex = eventPool;
        saveNode.EventSequenceIndex = eventSeq;
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

    public void TestCompleteCurrentNode()
    {
        var map = _save.SpaceSaveData.Map;
        var nodeSave = map.Nodes[_currentNodeIndex];

        // 1. Если миссия/босс, но ещё не проставлен MissionDeckIndex
        if ((nodeSave.NodeType == NodeType.Mission || nodeSave.NodeType == NodeType.Boss) &&
            nodeSave.MissionDeckIndex < 0)
        {
            int deckIdx = Mathf.Clamp(GetCompletedMissionsCount(), 0, _actsInfo[_save.SpaceSaveData.Act].MissionDeck.Length - 1);

            var mission = BuildMissionRandom(deckIdx, nodeSave.NodeType == NodeType.Boss, out int landIdx, out ObjectiveSave[] obj);

            nodeSave.MissionDeckIndex = deckIdx;
            nodeSave.SavedLandscapeIndex = landIdx;
            nodeSave.SavedObjectives = obj;

            // обновляем runtime-ноду
            _generator.GetGeneratedNodes()[_currentNodeIndex].nodeData = mission;
        }

        // 2. Помечаем узел выполненным (если ещё не был)
        if (!nodeSave.IsCompleted)
        {
            nodeSave.IsCompleted = true;
            _uiNodes[_currentNodeIndex].SetCompleted(true);
            RefreshHighlights();
        }

        // 3. Сохраняем сейв
        _save.GetCommandCenterSaveGameDataWriter()
             .WriteCommandCenterDataToSaveFile(_save.SpaceSaveData);
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
                case NodeType.None:
                    // тот же шаблон, что и во время первоначальной генерации плейсхолдера
                    data = _actsInfo[_save.SpaceSaveData.Act].MissionNodeTemplate;
                    break;
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
                    if (n.EventPoolIndex >= 0 && n.EventPoolIndex < _actsInfo[_save.SpaceSaveData.Act].EventPools.Length)
                    {
                        data = _actsInfo[_save.SpaceSaveData.Act].EventPools[n.EventPoolIndex].Node;
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

        var info = _actsInfo[_save.SpaceSaveData.Act];
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
        var definition = _actsInfo[_save.SpaceSaveData.Act].MissionDeck[deckIdx];
        var tplMission = _actsInfo[_save.SpaceSaveData.Act].MissionNodeTemplate;
        var tplBoss = _actsInfo[_save.SpaceSaveData.Act].BossNode;

        landscapeIdx = PickUniqueLandscape();
        var landscape = _actsInfo[_save.SpaceSaveData.Act].Landscapes[landscapeIdx];
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
        var definition = _actsInfo[_save.SpaceSaveData.Act].MissionDeck[deckIndex];
        var tplMission = _actsInfo[_save.SpaceSaveData.Act].MissionNodeTemplate;
        var tplBoss = _actsInfo[_save.SpaceSaveData.Act].BossNode;

        landscapeIdx = Mathf.Clamp(landscapeIdx, 0, _actsInfo[_save.SpaceSaveData.Act].Landscapes.Length - 1);
        var landscape = _actsInfo[_save.SpaceSaveData.Act].Landscapes[landscapeIdx];
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
        var landscapes = _actsInfo[_save.SpaceSaveData.Act].Landscapes;

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

    private void OnDestroy()
    {
        CustomEvents.OnDataLoad -= LoadMapData;
    }
}
