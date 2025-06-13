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

    public NodeData GetNodeTemplate(NodeType type)
    {
        return type switch
        {
            NodeType.Start => _allMissionsInfo.StartNode,
            NodeType.Boss => _allMissionsInfo.BossNode,
            NodeType.Event => _allMissionsInfo.Events.Length > 0 ? _allMissionsInfo.Events[0] : null,
            NodeType.ModuleTrader => _allMissionsInfo.ModuleTraders.Length > 0 ? _allMissionsInfo.ModuleTraders[0] : null,
            NodeType.SkillTrader => _allMissionsInfo.SkillTraders.Length > 0 ? _allMissionsInfo.SkillTraders[0] : null,
            NodeType.Mission => _allMissionsInfo.MissionNodeTemplate,
            _ => null
        };
    }


    public void GenerateMap()
    {
        // --------------------------------------------------------------------
        // 0. Сброс старых данных
        // --------------------------------------------------------------------
        _generatedNodes.Clear();
        _layers.Clear();
        SavedMap = new SavedMapData();

        // --------------------------------------------------------------------
        // 1. Копируем и перемешиваем исходные списки
        // --------------------------------------------------------------------
        var landscapes = new List<Landscape>(_allMissionsInfo.Landscapes);
        var objectives = new List<Objective>(_allMissionsInfo.Objectives);
        var spawners = new List<EnemiesSpawner>(_allMissionsInfo.EnemiesSpawnerInformation);
        var events = new List<EventNode>(_allMissionsInfo.Events);
        var moduleTraders = new List<ModuleTraderNode>(_allMissionsInfo.ModuleTraders);
        var skillTraders = new List<SkillTraderNode>(_allMissionsInfo.SkillTraders);

        Shuffle(landscapes);
        Shuffle(objectives);
        Shuffle(spawners);
        Shuffle(events);
        Shuffle(moduleTraders);
        Shuffle(skillTraders);

        // --------------------------------------------------------------------
        // 2. Считаем, сколько потребуется слоёв
        // --------------------------------------------------------------------
        int totalContentNodes = landscapes.Count + events.Count + moduleTraders.Count + skillTraders.Count;
        const int maxNodesPerLayer = 3;
        int contentLayers = Mathf.CeilToInt(totalContentNodes / (float)maxNodesPerLayer);
        int totalLayers = contentLayers + 3; // старт, минимум один слой миссий, босс

        int objectiveIdx = 0;
        int spawnerIdx = 0;

        // --------------------------------------------------------------------
        // 2.1  Стартовый узел
        // --------------------------------------------------------------------
        NodeInstance start = CreateNode(_allMissionsInfo.StartNode, 0);
        AddToLayer(0, start);
        _generatedNodes.Add(start);
        AddToSavedMap(start, NodeType.Start, _generatedNodes.Count - 1);

        // --------------------------------------------------------------------
        // 2.2  Первая обязательная миссия
        // --------------------------------------------------------------------
        MissionNode firstMissionData = CreateMissionNode(landscapes, objectives, spawners, ref objectiveIdx, ref spawnerIdx);
        NodeInstance firstMission = CreateNode(firstMissionData, 1);
        AddToLayer(1, firstMission);
        _generatedNodes.Add(firstMission);
        AddToSavedMap(firstMission, NodeType.Mission, _generatedNodes.Count - 1);

        // --------------------------------------------------------------------
        // 2.3  Промежуточные слои (2 … totalLayers-2) — случайное чередование типов
        // --------------------------------------------------------------------
        for (int layer = 2; layer < totalLayers - 1; layer++)
        {
            int nodesPlanned = Mathf.Min(maxNodesPerLayer, Random.Range(2, maxNodesPerLayer + 1));
            int nodesThisLayer = 0;

            while (nodesThisLayer < nodesPlanned && (landscapes.Count + events.Count + moduleTraders.Count + skillTraders.Count) > 0)
            {
                // -------- 1) формируем «мешок» доступных сейчас типов ---------
                var makers = new List<System.Action>();

                if (landscapes.Count > 0)
                    makers.Add(() =>
                    {
                        var m = CreateMissionNode(landscapes, objectives, spawners,
                                                  ref objectiveIdx, ref spawnerIdx);
                        AddNodeToLayer(m, NodeType.Mission);
                    });

                if (events.Count > 0)
                    makers.Add(() =>
                    {
                        var e = events[0];
                        events.RemoveAt(0);
                        AddNodeToLayer(e, NodeType.Event);
                    });

                if (moduleTraders.Count > 0)
                    makers.Add(() =>
                    {
                        var t = moduleTraders[0];
                        moduleTraders.RemoveAt(0);
                        AddNodeToLayer(t, NodeType.ModuleTrader);
                    });

                if (skillTraders.Count > 0)
                    makers.Add(() =>
                    {
                        var t = skillTraders[0];
                        skillTraders.RemoveAt(0);
                        AddNodeToLayer(t, NodeType.SkillTrader);
                    });

                // -------- 2) случайно выбираем лямбду и вызываем её -------------
                int pick = Random.Range(0, makers.Count);
                makers[pick].Invoke();
                nodesThisLayer++;
            }

            // --- локальная функция, чтобы не дублировать код вставки ----------
            void AddNodeToLayer(NodeData data, NodeType type)
            {
                var inst = CreateNode(data, layer);
                AddToLayer(layer, inst);
                _generatedNodes.Add(inst);
                AddToSavedMap(inst, type, _generatedNodes.Count - 1);
            }
        }

        // --------------------------------------------------------------------
        // 2.4  Финальный босс
        // --------------------------------------------------------------------
        NodeInstance boss = CreateNode(_allMissionsInfo.BossNode, totalLayers - 1);
        AddToLayer(totalLayers - 1, boss);
        _generatedNodes.Add(boss);
        AddToSavedMap(boss, NodeType.Boss, _generatedNodes.Count - 1);

        // --------------------------------------------------------------------
        // 3. Связи и финальный layout
        // --------------------------------------------------------------------
        GenerateConnections();
        LayoutLayers();

        FillMissionIndices();       // проставляем MissionIndex-ы
    }

    // заполняем в SaveMapData реальными сгенерирвоанными индексами 
    private void FillMissionIndices()
    {
        for (int i = 0; i < _generatedNodes.Count; i++)
        {
            var save = SavedMap.Nodes[i];
            if (save.NodeType != NodeType.Mission) continue;

            var mission = _generatedNodes[i].nodeData as MissionNode;
            if (mission == null) continue;

            save.MissionIndex = System.Array.IndexOf(_allMissionsInfo.Landscapes, mission.Landscape);
            save.ObjectiveIndex = System.Array.IndexOf(_allMissionsInfo.Objectives, mission.Objective);
            save.SpawnerIndex = System.Array.IndexOf(_allMissionsInfo.EnemiesSpawnerInformation, mission.EnemiesSpawner);
        }
    }


    // Создание конкретной MissionNode из независимых частей -----
    private MissionNode CreateMissionNode(List<Landscape> landscapes, List<Objective> objectives, List<EnemiesSpawner> spawners, ref int objectiveIdx, ref int spawnerIdx)
    {
        var node = ScriptableObject.CreateInstance<MissionNode>();

        node.Landscape = landscapes[0]; landscapes.RemoveAt(0);
        node.Objective = objectives[objectiveIdx++ % objectives.Count];
        node.EnemiesSpawner = spawners[spawnerIdx++ % spawners.Count];
        node.Icon = _allMissionsInfo.MissionNodeTemplate.Icon;
        node.IconColor = _allMissionsInfo.MissionNodeTemplate.IconColor;
        node.IconWidth = _allMissionsInfo.MissionNodeTemplate.IconWidth;
        node.IconHeight = _allMissionsInfo.MissionNodeTemplate.IconHeight;

        return node;
    }

    // Создание NodeInstance (без позиции!) ----------------------
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
    private void AddToSavedMap(NodeInstance instance, NodeType type, int nodeIndex)
    {
        SavedMap.Nodes.Add(new SavedNodeData
        {
            NodeIndex = nodeIndex,
            NodeType = type,
            MissionIndex = -1,
            ObjectiveIndex = -1,
            SpawnerIndex = -1,
            Position = Vector2.zero, // выставим позже
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

    // Связи: «верхняя к верхней» — без перекрёстий --------------
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
                if (Random.value < 0.3f && i + 1 < next.Count)
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

    private void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);
            (list[i], list[rand]) = (list[rand], list[i]);
        }
    }
}

[System.Serializable]
public class SavedMapData
{
    public List<SavedNodeData> Nodes = new();
    public int CurrentNodeIndex;
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
    public bool IsCompleted;
    public int CosmosIndex = -1;
}

public enum NodeType
{
    Start = 0,
    Mission = 1,
    Event = 2,
    ModuleTrader = 3,
    SkillTrader = 4,
    Boss = 5,
}

public class NodeInstance
{
    public NodeData nodeData;
    public int layer;
    public Vector2 position;
    public List<NodeInstance> connectedNodes = new();
}
