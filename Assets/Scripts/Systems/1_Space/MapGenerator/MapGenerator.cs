using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("UI/Content")]
    [SerializeField] private RectTransform _contentTransform;   // Content из ScrollView
    [SerializeField] private AllNodesInfo _allMissionsInfo;   // Все возможные данные

    [Header("Generation Settings")]
    private float _nodeXoffset = 300f;   // Шаг слоёв по X
    private float _nodeYoffset = 200;   // Шаг нод по Y
    private float _nodeYrandomSpread = 0;    // Случайное отклонение по Y
    private float _mainXOffset = 100;   // Смещение всей сетки по X
    private float _mainYOffset = 0;  // Смещение всей сетки по Y

    // Результат генерации
    [SerializeField] private List<NodeInstance> _generatedNodes = new();
    public List<NodeInstance> GetGeneratedNodes() => _generatedNodes;
    public SavedMapData SavedMap = new();

    private readonly Dictionary<int, List<NodeInstance>> _layers = new();

    public NodeData GetNodeTemplate(NodeType type)
    {
        return type switch
        {
            NodeType.Start => _allMissionsInfo.StartNode,
            NodeType.Boss => _allMissionsInfo.BossNode,
            NodeType.Event => _allMissionsInfo.EventPools[0].Node,
            NodeType.RewardEvent => _allMissionsInfo.EventPools[1].Node,
            NodeType.ResourceTrader => _allMissionsInfo.ResourceTraders[0],
            NodeType.SkillTrader => _allMissionsInfo.SkillTraders[0],
            NodeType.Mission => _allMissionsInfo.MissionNodeTemplate,
            _ => null
        };
    }


    public void GenerateMap()
    {
        // ───────── 0. reset ─────────
        _generatedNodes.Clear();
        _layers.Clear();
        SavedMap = new SavedMapData();

        // ───────── 1. исходные списки ─────────
        var rewardEvents = new List<EventEntry>();
        var hiddenEvents = new List<EventEntry>();

        foreach (var e in MapHelper.BuildEventEntries(_allMissionsInfo.EventPools))
            (e.Node is RewardEventNode ? rewardEvents : hiddenEvents).Add(e);

        var resTraders = new List<ResourceTraderNode>(_allMissionsInfo.ResourceTraders);
        var skillTraders = new List<SkillTraderNode>(_allMissionsInfo.SkillTraders);

        rewardEvents.Shuffle(); resTraders.Shuffle();
        skillTraders.Shuffle(); hiddenEvents.Shuffle();

        int missionPlaceholders = _allMissionsInfo.MissionDeck.Length;

        // «открытые» = Reward + оба Trader’а
        int openTotal = rewardEvents.Count + resTraders.Count + skillTraders.Count;

        const int maxPerLayer = 4;
        int totalContent = missionPlaceholders
                         + hiddenEvents.Count
                         + openTotal;
        int contentLayers = Mathf.CeilToInt(totalContent / (float)maxPerLayer);
        int totalLayers = contentLayers + 3;          // Start + content + Boss
        int lastContent = totalLayers - 2;

        // ────── 1.1  выбираем СЛОИ, куда обязательно пойдут «открытые» ──────
        const int gapLayers = 2;                        // ≥ 2 слоя между открытыми
        List<int> candidateLayers = Enumerable
                                    .Range(1, lastContent - 1)  // 1 … lastContent-1
                                    .ToList();
        candidateLayers.Shuffle();

        // если Reward+Traders больше, чем слоёв-кандидатов ⇒ урезаем
        if (openTotal > candidateLayers.Count)
            openTotal = candidateLayers.Count;

        // ----------- новый отбор с учётом «зазора» -----------
        var targetLayers = new HashSet<int>();

        foreach (int lay in candidateLayers)
        {
            // уже набрали всё нужное – выходим
            if (targetLayers.Count == openTotal) break;

            // хватает ли расстояния до любых ранее взятых?
            bool ok = targetLayers.All(prev => Mathf.Abs(prev - lay) >= gapLayers);

            if (ok) targetLayers.Add(lay);
        }

        // (если даже с фильтром не набрали – берём первые оставшиеся)
        if (targetLayers.Count < openTotal)
        {
            foreach (var lay in candidateLayers)
            {
                if (targetLayers.Count == openTotal) break;
                if (targetLayers.Add(lay)) { /* просто добрали */ }
            }
        }

        // ────── 2.  Start ──────
        {
            var s = CreateNode(_allMissionsInfo.StartNode, 0);
            AddToLayer(0, s);
            _generatedNodes.Add(s);
            AddToSavedMap(s, NodeType.Start, 0);
        }

        // ────── 3.  Локальные утилиты ──────
        bool TryPopReward(out EventEntry rev) => rewardEvents.TryPop(out rev);
        bool TryPopResTrader(out NodeData n) { if (resTraders.TryPop(out var t)) { n = t; return true; } n = null; return false; }
        bool TryPopSkillTr(out NodeData n) { if (skillTraders.TryPop(out var t)) { n = t; return true; } n = null; return false; }

        bool IsVisible(NodeData d) =>
               d is RewardEventNode || d is ResourceTraderNode || d is SkillTraderNode;

        void AddStub(int layer)
        {
            var stub = CreateNode(_allMissionsInfo.MissionNodeTemplate, layer);
            AddToLayer(layer, stub);
            _generatedNodes.Add(stub);
            AddToSavedMap(stub, NodeType.None, _generatedNodes.Count - 1);
        }

        void SpawnVisible(NodeData data, NodeType t, int layer,
                          int seq = -1, int pool = -1)
        {
            var n = CreateNode(data, layer);
            AddToLayer(layer, n);
            _generatedNodes.Add(n);
            AddToSavedMap(n, t, _generatedNodes.Count - 1, seq, pool);
        }

        void SwapNodes(NodeInstance a, NodeInstance b)
        {
            (a.nodeData, b.nodeData) = (b.nodeData, a.nodeData);

            int ia = _generatedNodes.IndexOf(a);
            int ib = _generatedNodes.IndexOf(b);

            (SavedMap.Nodes[ia].NodeType, SavedMap.Nodes[ib].NodeType) =
            (SavedMap.Nodes[ib].NodeType, SavedMap.Nodes[ia].NodeType);
            (SavedMap.Nodes[ia].EventPoolIndex, SavedMap.Nodes[ib].EventPoolIndex) =
            (SavedMap.Nodes[ib].EventPoolIndex, SavedMap.Nodes[ia].EventPoolIndex);
            (SavedMap.Nodes[ia].EventSequenceIndex, SavedMap.Nodes[ib].EventSequenceIndex) =
            (SavedMap.Nodes[ib].EventSequenceIndex, SavedMap.Nodes[ia].EventSequenceIndex);
        }

        // ────── 4.  Генерация слоя ──────
        void GenLayer(int layer, int minN, int maxN, bool allowTraders)
        {
            bool needVisible = targetLayers.Contains(layer);
            bool visiblePlaced = false;
            int need = Mathf.Min(maxPerLayer, Random.Range(minN, maxN + 1));

            for (int placed = 0; placed < need; placed++)
            {
                if (needVisible && !visiblePlaced)
                {
                    if (TryPopReward(out var rev))
                    {
                        SpawnVisible(rev.Node, NodeType.RewardEvent,
                                     layer, rev.SequenceIndex, rev.PoolIndex);
                        visiblePlaced = true;
                        continue;
                    }

                    if (allowTraders && TryPopResTrader(out var rTr))
                    {
                        SpawnVisible(rTr, NodeType.ResourceTrader, layer);
                        visiblePlaced = true;
                        continue;
                    }

                    if (allowTraders && TryPopSkillTr(out var sTr))
                    {
                        SpawnVisible(sTr, NodeType.SkillTrader, layer);
                        visiblePlaced = true;
                        continue;
                    }
                }

                AddStub(layer);
            }

            // если получилось V-V по ребру – пробуем разменять
            foreach (var inst in _layers[layer])
            {
                if (!IsVisible(inst.nodeData)) continue;

                foreach (var prev in inst.connectedNodes)
                {
                    if (prev.layer == layer - 1 && IsVisible(prev.nodeData))
                    {
                        var swap = FindSafePlaceholder(layer);
                        if (swap != null) SwapNodes(inst, swap);
                        break;
                    }
                }
            }
        }

        // ────── 5.  Контент ──────
        GenLayer(1, 2, 3, false);          // первый слой без Trader’ов
        for (int l = 2; l < lastContent; l++)              // середина
            GenLayer(l, 3, 4, true);
        GenLayer(lastContent, 2, 3, true);

        foreach (var kv in _layers) kv.Value.Shuffle();    // лёгкая рандомизация позиций

        // ────── 6.  Boss ──────
        var boss = CreateNode(_allMissionsInfo.BossNode, totalLayers - 1);
        AddToLayer(totalLayers - 1, boss);
        _generatedNodes.Add(boss);
        AddToSavedMap(boss, NodeType.Boss, _generatedNodes.Count - 1);

        // ────── 7.  Connections + Layout ──────
        GenerateConnections();
        LayoutLayers();
    }


    private NodeInstance FindSafePlaceholder(int layerIdx)
    {
        return _layers[layerIdx]
            .FirstOrDefault(n =>
                SavedMap.Nodes[_generatedNodes.IndexOf(n)].NodeType == NodeType.None &&
                n.connectedNodes.All(nn => !MapHelper.IsVisible(nn.nodeData)));
    }


    // Создание NodeInstance (без позиции!)
    private NodeInstance CreateNode(NodeData data, int layer)
    {
        return new NodeInstance
        {
            nodeData = data,
            layer = layer,
            position = Vector2.zero,   // будет назначено в LayoutLayers()
            connectedNodes = new List<NodeInstance>()
        };
    }

    private void AddToLayer(int layer, NodeInstance instance)
    {
        if (!_layers.ContainsKey(layer))
        {
            _layers[layer] = new List<NodeInstance>();
        }

        _layers[layer].Add(instance);
    }

    // Сохраняем в SavedMap
    private void AddToSavedMap(NodeInstance instance, NodeType type, int nodeIndex, int eventSequenceIndex = -1, int eventPoolIndex = -1)
    {
        SavedMap.Nodes.Add(new SavedNodeData
        {
            NodeIndex = nodeIndex,
            NodeType = type,
            MissionDeckIndex = -1,
            EventSequenceIndex = eventSequenceIndex,
            EventPoolIndex = eventPoolIndex,
            Position = Vector2.zero,
            Layer = instance.layer,
            ConnectedNodeIndices = new List<int>()
        });
    }

    // Финальный Layout всех слоёв
    private void LayoutLayers()
    {
        float totalWidth = _contentTransform.rect.width;

        foreach (var pair in _layers)
        {
            int layer = pair.Key;
            var nodesInLay = pair.Value;

            float x = layer * _nodeXoffset - totalWidth / 2f + _mainXOffset;

            for (int i = 0; i < nodesInLay.Count; i++)
            {
                // «0» по центру, дальше вниз положительные Y (Canvas-координаты)
                float y = (i - (nodesInLay.Count - 1) / 2f) * _nodeYoffset + _mainYOffset + Random.Range(-_nodeYrandomSpread, _nodeYrandomSpread);

                Vector2 pos = new(x, y);
                nodesInLay[i].position = pos;

                // записываем и в сохранённую структуру
                int idx = _generatedNodes.IndexOf(nodesInLay[i]);
                if (idx >= 0)
                {
                    SavedMap.Nodes[idx].Position = pos;
                }
            }
        }
    }

    // Связи: «верхняя к верхней» — без перекрёстий
    private void GenerateConnections()
    {
        var sortedLayers = new List<int>(_layers.Keys);
        sortedLayers.Sort();

        // 1.  Создаём связи
        for (int k = 0; k < sortedLayers.Count - 1; k++)
        {
            List<NodeInstance> cur = _layers[sortedLayers[k]];
            List<NodeInstance> next = _layers[sortedLayers[k + 1]];

            int max = Mathf.Max(cur.Count, next.Count);

            for (int i = 0; i < max; i++)
            {
                NodeInstance from = cur[Mathf.Clamp(i, 0, cur.Count - 1)];
                NodeInstance to = next[Mathf.Clamp(i, 0, next.Count - 1)];

                if (!from.connectedNodes.Contains(to))
                {
                    from.connectedNodes.Add(to);
                }

                // 30 % шанс добавить «резервную» нижнюю связь, не создавая пересечений
                if (Random.value < 0.5f && i + 1 < next.Count)
                {
                    NodeInstance alt = next[i + 1];
                    if (!from.connectedNodes.Contains(alt))
                    {
                        from.connectedNodes.Add(alt);
                    }
                }
            }
        }

        // 2.  Переводим во внутренние индексы SavedMap
        for (int i = 0; i < _generatedNodes.Count; i++)
        {
            var srcIns = _generatedNodes[i];
            var saveIns = SavedMap.Nodes[i];

            foreach (var target in srcIns.connectedNodes)
            {
                int tIdx = _generatedNodes.IndexOf(target);
                if (tIdx >= 0)
                {
                    saveIns.ConnectedNodeIndices.Add(tIdx);
                }
            }
        }
    }
}

[System.Serializable]
public class SavedMapData
{
    public List<SavedNodeData> Nodes = new();
    public int CurrentNodeIndex;
    public int PatternCursor = 0;   // сколько «символов» паттерна уже израсходовано
}

[System.Serializable]
public struct ObjectiveSave
{
    public ObjectiveEnum Objective;
    public int Amount;
}

[System.Serializable]
public class SavedNodeData
{
    public int MissionDeckIndex = -1;
    public int SavedLandscapeIndex = -1;
    public ObjectiveSave[] SavedObjectives = null;
    public int NodeIndex;
    public NodeType NodeType;
    public int EventPoolIndex;
    public int EventSequenceIndex = -1;
    public Vector2 Position;
    public int Layer;
    public List<int> ConnectedNodeIndices = new();
    public bool IsCompleted;
    public int CosmosIndex = -1;
}

public enum NodeType
{
    None = -1,
    Start = 0,
    Mission = 1,
    Event = 2,
    ResourceTrader = 3,
    SkillTrader = 4,
    Boss = 5,
    RewardEvent = 6,
}

public class NodeInstance
{
    public NodeData nodeData;
    public int layer;
    public Vector2 position;
    public List<NodeInstance> connectedNodes = new();
}

public struct EventEntry
{
    public NodeData Node;
    public int PoolIndex;
    public int SequenceIndex;
}

public enum MapPatternEnum { NonMission, Mission }

