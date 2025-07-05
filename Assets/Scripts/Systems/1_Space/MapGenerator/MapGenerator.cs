using System.Collections.Generic;
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

        // для оценки количества узлов Reward + Traders — «видимые»
        int visibleCount = rewardEvents.Count + resTraders.Count + skillTraders.Count;

        int totalContent = missionPlaceholders + hiddenEvents.Count + visibleCount;
        const int maxPerLayer = 4;
        int contentLayers = Mathf.CeilToInt(totalContent / (float)maxPerLayer);
        int totalLayers = contentLayers + 3;                 // Start + content + Boss

        // ───────── 2. старт ─────────
        {
            var s = CreateNode(_allMissionsInfo.StartNode, 0);
            AddToLayer(0, s);
            _generatedNodes.Add(s);
            AddToSavedMap(s, NodeType.Start, 0);
        }

        // счётчик «сколько плейсхолдеров прошло после последнего видимого»
        int gap = 99;          // можно сразу ставить
        const int minGap = 1;  // хотя бы один «?» между видимыми

        // ───────── локальное тело слоя ─────────
        void GenLayer(int layer, int minN, int maxN, bool allowTraders, bool onlyOneVisibleHere = false)
        {
            bool visibleHere = false;
            gap = Mathf.Min(gap, 99);
            int need = Mathf.Min(maxPerLayer, Random.Range(minN, maxN + 1));
            int placed = 0;

            while (placed < need)
            {
                bool putVisible = !visibleHere && gap >= minGap;

                if (putVisible)
                {
                    // случайно решаем, кого брать первым – Reward или Trader
                    bool rewardFirst = Random.value < .5f;

                    if (rewardFirst && rewardEvents.Count > 0)
                        SpawnVisible(rewardEvents[0].Node, NodeType.RewardEvent,
                                     rewardEvents[0].SequenceIndex, rewardEvents[0].PoolIndex,
                                     rewardEvents.RemoveAtReturn(0));
                    else if (allowTraders && resTraders.Count > 0)
                        SpawnVisible(resTraders[0], NodeType.ResourceTrader,
                                     remove: resTraders.RemoveAtReturn(0));
                    else if (allowTraders && skillTraders.Count > 0)
                        SpawnVisible(skillTraders[0], NodeType.SkillTrader,
                                     remove: skillTraders.RemoveAtReturn(0));
                    else if (!rewardFirst && rewardEvents.Count > 0)
                        SpawnVisible(rewardEvents[0].Node, NodeType.RewardEvent,
                                     rewardEvents[0].SequenceIndex, rewardEvents[0].PoolIndex,
                                     rewardEvents.RemoveAtReturn(0));
                }

                if (!putVisible || !visibleHere)              // если не смогли — плейсхолдер
                {
                    var stub = CreateNode(_allMissionsInfo.MissionNodeTemplate, layer);
                    AddToLayer(layer, stub);
                    _generatedNodes.Add(stub);
                    AddToSavedMap(stub, NodeType.None, _generatedNodes.Count - 1);
                    gap++;
                }

                placed++;
                if (onlyOneVisibleHere && visibleHere) gap = 0;  // фикс, если слой ограничен
            }

            // ---------- вложенный помощник ----------
            void SpawnVisible(NodeData d, NodeType t, int seq = -1, int pool = -1, bool remove = true)
            {
                var n = CreateNode(d, layer);
                AddToLayer(layer, n);
                _generatedNodes.Add(n);
                AddToSavedMap(n, t, _generatedNodes.Count - 1, seq, pool);

                gap = 0;
                visibleHere = true;
            }

            void CleanAdjacentVisible()
            {
                foreach (var inst in _layers[layer])          // все узлы текущего слоя
                {
                    if (!IsVisible(inst.nodeData)) continue;  // пропускаем скрытые

                    foreach (var prev in inst.connectedNodes) // смотрим связи назад
                    {
                        if (prev.layer == layer - 1 && IsVisible(prev.nodeData))
                        {
                            // нашли V-V по ребру  → делаем этот inst плейсхолдером
                            ToPlaceholder(inst);
                            break;
                        }
                    }
                }

                // ——— локальные помощники ———
                bool IsVisible(NodeData d) => d is RewardEventNode ||
                                              d is ResourceTraderNode ||
                                              d is SkillTraderNode;

                void ToPlaceholder(NodeInstance ins)
                {
                    ins.nodeData = _allMissionsInfo.MissionNodeTemplate;
                    int idx = _generatedNodes.IndexOf(ins);
                    var save = SavedMap.Nodes[idx];
                    save.NodeType = NodeType.None;
                    save.EventPoolIndex = save.EventSequenceIndex = -1;
                }
            }

            CleanAdjacentVisible();
            if (gap == 0)          // слой закончился видимым узлом
                gap = 1;
        }

        // ───────── 3. контент ─────────

        // 1-й слой: 2-3 нода, трейдеры запрещены, видимый узел максимум один
        GenLayer(1, minN: 2, maxN: 3, allowTraders: false, onlyOneVisibleHere: true);

        // «Середина»: ≥3 нода (3-4), трейдеры разрешены
        for (int l = 2; l < totalLayers - 2; l++)          //  -2   ← последний контент-слой пропускаем
            GenLayer(l, minN: 3, maxN: 4, allowTraders: true);

        // Последний контент-слой (перед боссом): 2-3 нода
        int lastContent = totalLayers - 2;
        GenLayer(lastContent, minN: 2, maxN: 3, allowTraders: true);

        foreach (var kv in _layers)
            kv.Value.Shuffle();

        // ───────── 4. босс ─────────

        var boss = CreateNode(_allMissionsInfo.BossNode, totalLayers - 1);
        AddToLayer(totalLayers - 1, boss);
        _generatedNodes.Add(boss);
        AddToSavedMap(boss, NodeType.Boss, _generatedNodes.Count - 1);


        // ───────── 5. связи + layout ─────────
        GenerateConnections();
        LayoutLayers();
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

