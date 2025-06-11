using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("UI/Content")]
    [SerializeField] private RectTransform _contentTransform;   // Content из ScrollView
    [SerializeField] private AllMissionsInfo _allMissionsInfo;   // Все возможные данные

    [Header("Generation Settings")]
    private float _nodeXoffset = 300f;   // Шаг слоёв по X
    private float _nodeYoffset = 250;   // Шаг нод по Y
    private float _nodeYrandomSpread = 0;    // Случайное отклонение по Y
    private float _mainXOffset = 100;   // Смещение всей сетки по X
    private float _mainYOffset = 0;  // Смещение всей сетки по Y

    // Результат генерации
    [SerializeField] private List<NodeInstance> _generatedNodes = new();
    public List<NodeInstance> GetGeneratedNodes() => _generatedNodes;
    public SavedMapData SavedMap = new();

    // Внутренние данные
    private readonly Dictionary<int, List<NodeInstance>> _layers = new();

    // -------------------------------------------------------
    //  PUBLIC API
    // -------------------------------------------------------
    public void GenerateMap()
    {
        _generatedNodes.Clear();
        _layers.Clear();
        SavedMap = new SavedMapData();

        // 1. Собираем несвязанные данные и перетасовываем
        var landscapes = new List<Landscape>(_allMissionsInfo.Landscapes);
        var objectives = new List<Objective>(_allMissionsInfo.Objectives);
        var spawners = new List<EnemiesSpawner>(_allMissionsInfo.EnemiesSpawnerInformation);
        var events = new List<EventNode>(_allMissionsInfo.Events);
        var traders = new List<TraderNode>(_allMissionsInfo.Traders);

        Shuffle(landscapes);
        Shuffle(objectives);
        Shuffle(spawners);
        Shuffle(events);
        Shuffle(traders);

        // 2. Подсчитываем, сколько нужно слоёв
        int totalContentNodes = landscapes.Count + events.Count + traders.Count;
        const int maxNodesPerLayer = 3;
        int contentLayers = Mathf.CeilToInt(totalContentNodes / (float)maxNodesPerLayer);
        int totalLayers = contentLayers + 3; // старт, минимум один слой миссий, босс

        int objectiveIndex = 0;
        int spawnerIndex = 0;

        // ---------------------------------------------------
        // 2.1  Стартовый узел
        // ---------------------------------------------------
        NodeInstance startNode = CreateNode(_allMissionsInfo.StartNode, 0);
        AddToLayer(0, startNode);
        _generatedNodes.Add(startNode);
        AddToSavedMap(startNode, NodeType.Start, _generatedNodes.Count - 1);

        // ---------------------------------------------------
        // 2.2  Первая обязательная миссия
        // ---------------------------------------------------
        MissionNode firstMissionData = CreateMissionNode(landscapes, objectives, spawners, ref objectiveIndex, ref spawnerIndex);
        NodeInstance firstMission = CreateNode(firstMissionData, 1);
        AddToLayer(1, firstMission);
        _generatedNodes.Add(firstMission);
        AddToSavedMap(firstMission, NodeType.Mission, _generatedNodes.Count - 1);

        // ---------------------------------------------------
        // 2.3  Промежуточные слои (2 … totalLayers-2)
        //       — стараемся держать 2-3 ноды
        // ---------------------------------------------------
        for (int layer = 2; layer < totalLayers - 1; layer++)
        {
            // Выбираем 2-3, но не больше maxNodesPerLayer
            int nodesPlanned = Mathf.Min(maxNodesPerLayer, Random.Range(2, maxNodesPerLayer + 1));
            int nodesThisLayer = 0;

            while (nodesThisLayer < nodesPlanned &&
                   (landscapes.Count > 0 || events.Count > 0 || traders.Count > 0))
            {
                NodeInstance inst = null;
                NodeType type = NodeType.Event; // значение «по умолчанию»

                // приоритет: миссии → ивенты → торговцы
                if (landscapes.Count > 0)
                {
                    var mission = CreateMissionNode(landscapes, objectives, spawners,
                                                    ref objectiveIndex, ref spawnerIndex);
                    inst = CreateNode(mission, layer);
                    type = NodeType.Mission;
                }
                else if (events.Count > 0)
                {
                    inst = CreateNode(events[0], layer);
                    events.RemoveAt(0);
                    type = NodeType.Event;
                }
                else if (traders.Count > 0)
                {
                    inst = CreateNode(traders[0], layer);
                    traders.RemoveAt(0);
                    type = NodeType.Trader;
                }

                if (inst != null)
                {
                    AddToLayer(layer, inst);
                    _generatedNodes.Add(inst);
                    AddToSavedMap(inst, type, _generatedNodes.Count - 1);
                    nodesThisLayer++;
                }
            }

            // Если осталась всего одна нода, а ресурсов ещё много —
            // докидываем вторую, чтобы не было «сиротских» слоёв.
            if (nodesThisLayer == 1 &&
                (landscapes.Count > 0 || events.Count > 0 || traders.Count > 0))
            {
                NodeInstance extra = null;
                NodeType type = NodeType.Event;

                if (landscapes.Count > 0)
                {
                    var mission = CreateMissionNode(landscapes, objectives, spawners,
                                                    ref objectiveIndex, ref spawnerIndex);
                    extra = CreateNode(mission, layer);
                    type = NodeType.Mission;
                }
                else if (events.Count > 0)
                {
                    extra = CreateNode(events[0], layer);
                    events.RemoveAt(0);
                    type = NodeType.Event;
                }
                else if (traders.Count > 0)
                {
                    extra = CreateNode(traders[0], layer);
                    traders.RemoveAt(0);
                    type = NodeType.Trader;
                }

                if (extra != null)
                {
                    AddToLayer(layer, extra);
                    _generatedNodes.Add(extra);
                    AddToSavedMap(extra, type, _generatedNodes.Count - 1);
                }
            }
        }

        // ---------------------------------------------------
        // 2.4  Финальный босс
        // ---------------------------------------------------
        NodeInstance bossNode = CreateNode(_allMissionsInfo.BossNode, totalLayers - 1);
        AddToLayer(totalLayers - 1, bossNode);
        _generatedNodes.Add(bossNode);
        AddToSavedMap(bossNode, NodeType.Boss, _generatedNodes.Count - 1);

        // ---------------------------------------------------
        // 3.  Генерируем связи и финально раскладываем ноды
        // ---------------------------------------------------
        GenerateConnections();
        LayoutLayers();          // ← координаты вычисляем в самом конце
    }

    // ====================================================================
    //  PRIVATE HELPERS
    // ====================================================================

    // ---------- Создание конкретной MissionNode из независимых частей -----
    private MissionNode CreateMissionNode(List<Landscape> landscapes,
                                          List<Objective> objectives,
                                          List<EnemiesSpawner> spawners,
                                          ref int objectiveIdx,
                                          ref int spawnerIdx)
    {
        var node = ScriptableObject.CreateInstance<MissionNode>();

        node.Landscape = landscapes[0]; landscapes.RemoveAt(0);
        node.Objective = objectives[objectiveIdx++ % objectives.Count];
        node.EnemiesSpawner = spawners[spawnerIdx++ % spawners.Count];
        node.Icon = _allMissionsInfo.MissionNodeTemplate.Icon;

        return node;
    }

    // ---------- Создание NodeInstance (без позиции!) ----------------------
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
            _layers[layer] = new List<NodeInstance>();

        _layers[layer].Add(instance);
    }

    // ---------- Сохраняем в SavedMap «болванку» ---------------------------
    private void AddToSavedMap(NodeInstance instance, NodeType type, int nodeIndex)
    {
        SavedMap.Nodes.Add(new SavedNodeData
        {
            NodeIndex = nodeIndex,
            NodeType = type,
            MissionIndex = -1,
            ObjectiveIndex = -1,
            SpawnerIndex = -1,
            Position = Vector2.zero,        // выставим позже
            Layer = instance.layer,
            ConnectedNodeIndices = new List<int>()
        });
    }

    // ---------- Финальный Layout всех слоёв ------------------------------
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
                float y = (i - (nodesInLay.Count - 1) / 2f) * _nodeYoffset
                          + _mainYOffset
                          + Random.Range(-_nodeYrandomSpread, _nodeYrandomSpread);

                Vector2 pos = new(x, y);
                nodesInLay[i].position = pos;

                // записываем и в сохранённую структуру
                int idx = _generatedNodes.IndexOf(nodesInLay[i]);
                if (idx >= 0)
                    SavedMap.Nodes[idx].Position = pos;
            }
        }
    }

    // ---------- Связи: «верхняя к верхней» — без перекрёстий --------------
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
                    from.connectedNodes.Add(to);

                // 30 % шанс добавить «резервную» нижнюю связь, не создавая пересечений
                if (Random.value < 0.3f && i + 1 < next.Count)
                {
                    NodeInstance alt = next[i + 1];
                    if (!from.connectedNodes.Contains(alt))
                        from.connectedNodes.Add(alt);
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
                    saveIns.ConnectedNodeIndices.Add(tIdx);
            }
        }
    }

    // ---------- Fisher–Yates shuffle --------------------------------------
    private void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);
            (list[i], list[rand]) = (list[rand], list[i]);
        }
    }
}

// ========================================================================
//  Данные сохранения и служебные структуры (без изменений)
// ========================================================================
[System.Serializable]
public class SavedMapData
{
    public List<SavedNodeData> Nodes = new();
}

[System.Serializable]
public class SavedNodeData
{
    public int NodeIndex;
    public NodeType NodeType;
    public int MissionIndex;
    public int ObjectiveIndex;
    public int SpawnerIndex;
    public Vector2 Position;
    public int Layer;
    public List<int> ConnectedNodeIndices = new();
}

public enum NodeType
{
    Start = 0,
    Mission = 1,
    Event = 2,
    Trader = 3,
    Boss = 4,
}

public class NodeInstance
{
    public NodeData nodeData;
    public int layer;
    public Vector2 position;
    public List<NodeInstance> connectedNodes = new();
}
