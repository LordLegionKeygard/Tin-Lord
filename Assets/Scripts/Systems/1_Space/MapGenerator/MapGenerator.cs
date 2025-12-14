using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("UI/Content")]
    [SerializeField] private RectTransform _contentTransform;   // Content из ScrollView
    [SerializeField] private ActInfo _currentAct;   // Все возможные данные

    public void SetActInfo(ActInfo info)
    {
        _currentAct = info;
    }

    [Header("Generation Settings")]
    private float _nodeXoffset = 300f;   // Шаг слоёв по X
    private float _nodeYoffset = 200;   // Шаг нод по Y
    private float _nodeYrandomSpread = 50;    // Случайное отклонение по Y
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
            NodeType.Start => _currentAct.StartNode,
            NodeType.Boss => _currentAct.BossNode,
            NodeType.Event => _currentAct.EventPools[0].Node,
            NodeType.RewardEvent => _currentAct.EventPools[1].Node,
            NodeType.ResourceTrader => _currentAct.ResourceTraders[0],
            NodeType.SkillTrader => _currentAct.SkillTraders[0],
            NodeType.WeaponTrader => _currentAct.WeaponTraders[0],
            NodeType.Mission => _currentAct.MissionNodeTemplate,
            _ => null
        };
    }


    public void GenerateMap()
    {
        // 0. Reset
        _generatedNodes.Clear();
        _layers.Clear();
        SavedMap = new SavedMapData();

        // 1. Исходные списки
        var rewardEvents = new List<EventEntry>();
        var hiddenEvents = new List<EventEntry>();

        foreach (var e in MapHelper.BuildEventEntries(_currentAct.EventPools))
            (e.Node is RewardEventNode ? rewardEvents : hiddenEvents).Add(e);

        var resourceTraders = new List<ResourceTraderNode>(_currentAct.ResourceTraders);
        var skillTraders = new List<SkillTraderNode>(_currentAct.SkillTraders);
        var weponEngineers = new List<WeaponTraderNode>(_currentAct.WeaponTraders);

        rewardEvents.Shuffle();
        hiddenEvents.Shuffle();
        resourceTraders.Shuffle();
        skillTraders.Shuffle();
        weponEngineers.Shuffle();

        int missionPlaceholders = _currentAct.MissionDeck.Length;

        // «открытые» = Reward + торговцы
        int openTotal = rewardEvents.Count + resourceTraders.Count + skillTraders.Count + weponEngineers.Count;

        const int maxPerLayer = 4;
        int totalContent = missionPlaceholders
                         + hiddenEvents.Count
                         + openTotal;
        int contentLayers = Mathf.CeilToInt(totalContent / (float)maxPerLayer);
        int totalLayers = contentLayers + 3;          // Start + content + Boss
        int lastContent = totalLayers - 2;

        // 1.1 выбираем СЛОИ, куда обязательно пойдут «открытые»
        const int firstContentLayer = 1;
        const int gapLayers = 2;           // ≥ 2 слоя между открытыми
        // слой 1 сразу после Start исключаем
        List<int> candidateLayers = Enumerable.Range(firstContentLayer + 1, lastContent - firstContentLayer - 1).ToList();
        candidateLayers.Shuffle();

        // если Reward+Traders больше, чем слоёв-кандидатов ⇒ урезаем
        if (openTotal > candidateLayers.Count)
            openTotal = candidateLayers.Count;

        // новый отбор с учётом «зазора»
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

        // 2. Start
        {
            var s = CreateNode(_currentAct.StartNode, 0);
            AddToLayer(0, s);
            _generatedNodes.Add(s);
            AddToSavedMap(s, NodeType.Start, 0);
        }

        void AddStub(int layer)
        {
            var stub = CreateNode(_currentAct.MissionNodeTemplate, layer);
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

        bool TryPopRandomOpen(bool allowTraders, out NodeData node, out NodeType nodeType, out int seq, out int pool)
        {
            node = null; nodeType = NodeType.None;
            seq = pool = -1;

            // какие списки ещё не пустые?
            var options = new List<int>(); // 0 = reward, 1 = res, 2 = skill, 3 = weapon
            if (rewardEvents.Count > 0) options.Add(0);
            if (allowTraders && resourceTraders.Count > 0) options.Add(1);
            if (allowTraders && skillTraders.Count > 0) options.Add(2);
            if (allowTraders && weponEngineers.Count > 0) options.Add(3);
            if (options.Count == 0) return false;

            switch (options[Random.Range(0, options.Count)])
            {
                // 0 Reward-event
                case 0:
                    {
                        var rev = rewardEvents[0]; // достали
                        rewardEvents.RemoveAt(0); // убрали
                        node = rev.Node;
                        nodeType = NodeType.RewardEvent;
                        seq = rev.SequenceIndex;
                        pool = rev.PoolIndex;
                        break;
                    }

                // 1 ResourceTrader
                case 1:
                    {
                        var tr = resourceTraders[0];
                        resourceTraders.RemoveAt(0);
                        node = tr;
                        nodeType = NodeType.ResourceTrader;
                        break;
                    }

                // 2 SkillTrader
                case 2:
                    {
                        var tr = skillTraders[0];
                        skillTraders.RemoveAt(0);
                        node = tr;
                        nodeType = NodeType.SkillTrader;
                        break;
                    }
                // 3 WeaponEngineer
                case 3:
                    {
                        var tr = weponEngineers[0];
                        weponEngineers.RemoveAt(0);
                        node = tr;
                        nodeType = NodeType.WeaponTrader;
                        break;
                    }
            }
            return true;
        }

        // генерация слоя
        int GenLayer(int layer, int minN, int maxN, bool allowTraders)
        {
            bool needVisible = targetLayers.Contains(layer);
            bool visiblePlaced = false;

            int need = Random.Range(minN, maxN + 1);
            int placed = 0;

            int visibleSlot = -1;
            if (needVisible)
            {
                int prevVis = MapHelper.GetVisibleIndexInLayer(_layers, layer - 1);
                visibleSlot = MapHelper.PickVisibleSlot(need, prevVis);
            }

            while (placed < need)
            {
                if (needVisible && !visiblePlaced && placed == visibleSlot &&
                    TryPopRandomOpen(allowTraders, out var openNode, out var openType, out var seq, out var pool))
                {
                    SpawnVisible(openNode, openType, layer, seq, pool);
                    visiblePlaced = true;
                }
                else
                {
                    AddStub(layer);
                }
                placed++;
            }

            // удалите/закомментируйте старую попытку «свапа» видимый↔видимый снизу — она бесполезна до GenerateConnections()
            return placed;
        }

        // 5-bis. Добрасываем оставшиеся открытые узлы
        void PlaceRemainingOpenNodes()
        {
            // RewardEvent
            foreach (var rev in rewardEvents)
            {
                var stub = MapHelper.PickSafeStubForVisible(_generatedNodes, _layers, SavedMap);
                if (stub == null) break;

                stub.nodeData = rev.Node;
                var s = SavedMap.Nodes[_generatedNodes.IndexOf(stub)];
                s.NodeType = NodeType.RewardEvent;
                s.EventPoolIndex = rev.PoolIndex;
                s.EventSequenceIndex = rev.SequenceIndex;
            }

            // ResourceTraders
            foreach (var tr in resourceTraders)
            {
                var stub = MapHelper.PickSafeStubForVisible(_generatedNodes, _layers, SavedMap);
                if (stub == null) break;

                stub.nodeData = tr;
                SavedMap.Nodes[_generatedNodes.IndexOf(stub)].NodeType = NodeType.ResourceTrader;
            }

            // SkillTraders
            foreach (var tr in skillTraders)
            {
                var stub = MapHelper.PickSafeStubForVisible(_generatedNodes, _layers, SavedMap);
                if (stub == null) break;

                stub.nodeData = tr;
                SavedMap.Nodes[_generatedNodes.IndexOf(stub)].NodeType = NodeType.SkillTrader;
            }

            // WeaponEnineers
            foreach (var tr in weponEngineers)
            {
                var stub = MapHelper.PickSafeStubForVisible(_generatedNodes, _layers, SavedMap);
                if (stub == null) break;

                stub.nodeData = tr;
                SavedMap.Nodes[_generatedNodes.IndexOf(stub)].NodeType = NodeType.WeaponTrader;
            }

            // списки опустошили — больше никто «невидимо» не появится
            rewardEvents.Clear();
            resourceTraders.Clear();
            skillTraders.Clear();
            weponEngineers.Clear();
        }

        // 5. Контент
        GenLayer(1, 2, 3, false); // первый слой

        bool lastWasPair = false; // флаг «предыдущий слой = 2 нода»

        for (int l = 2; l < lastContent; l++)
        {
            int minThis = lastWasPair ? 3 : 2; // если прошлый был парой → сейчас ≥3
            int placed = GenLayer(l, minThis, 4, true);
            lastWasPair = placed == 2;
        }

        GenLayer(lastContent, 2, 3, true);
        PlaceRemainingOpenNodes();
        MapHelper.ShuffleNonVisible(_layers);

        // 6. Boss
        var boss = CreateNode(_currentAct.BossNode, totalLayers - 1);
        AddToLayer(totalLayers - 1, boss);
        _generatedNodes.Add(boss);
        AddToSavedMap(boss, NodeType.Boss, _generatedNodes.Count - 1);

        // 7.Connections + Layout
        GenerateConnections();
        LayoutLayers();
    }


    private NodeInstance FindSafePlaceholder(int layerIdx)
    {
        return _layers[layerIdx].FirstOrDefault(n =>
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
            position = Vector2.zero, // будет назначено в LayoutLayers()
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
    public int PatternIndex = 0;   // сколько «символов» паттерна уже израсходовано
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
    WeaponTrader = 7,
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

