using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private RectTransform _contentTransform;
    [SerializeField] private NodeDataPool _nodeDataPool;
    [SerializeField] private AllMissionsInfo _allMissionsInfo;

    [Header("Generation Settings")]
    private int _totalLayers = 4;
    private float _xOffset = 300f;
    private float _yOffset = 300f;
    private float _yRandomSpread = 50f;
    private float _leftOffset = 100f;

    [SerializeField] private List<NodeInstance> _generatedNodes = new();
    public List<NodeInstance> GetGeneratedNodes() => _generatedNodes;
    public SavedMapData SavedMap = new();

    private readonly Dictionary<int, List<NodeInstance>> _layers = new();

    public void GenerateDesertMap()
    {
        _generatedNodes.Clear();
        _layers.Clear();
        SavedMap = new SavedMapData();

        MapChapter chapter = _allMissionsInfo.MapChapters[(int)ChaptersEnum.Desert];
        List<int> availableMissionIndices = new();
        for (int i = 0; i < chapter.Missions.Length; i++) availableMissionIndices.Add(i);
        Shuffle(availableMissionIndices);

        int objectiveIndex = 0;
        int spawnerIndex = 0;

        // Стартовая миссия
        int startIndex = availableMissionIndices[0];
        availableMissionIndices.RemoveAt(0);

        MissionNode startMission = CreateMissionNode(chapter, startIndex, objectiveIndex++, spawnerIndex++);
        NodeInstance startInstance = CreateNode(startMission, 0, 0);
        AddToLayer(0, startInstance);
        _generatedNodes.Add(startInstance);
        AddToSavedMap(startInstance, NodeType.Mission, 0, startIndex, objectiveIndex - 1, spawnerIndex - 1);

        // Заполнение остальных слоев
        for (int layer = 1; layer < _totalLayers - 1; layer++)
        {
            int nodesInLayer = Random.Range(1, 3);
            for (int i = 0; i < nodesInLayer; i++)
            {
                NodeInstance instance = null;
                NodeType type = NodeType.Event;
                int indexInChapter = -1;

                if (availableMissionIndices.Count > 0)
                {
                    indexInChapter = availableMissionIndices[0];
                    availableMissionIndices.RemoveAt(0);
                    MissionNode node = CreateMissionNode(chapter, indexInChapter, objectiveIndex++, spawnerIndex++);
                    instance = CreateNode(node, layer, i);
                    type = NodeType.Mission;
                }
                else if (_nodeDataPool.Events.Count > 0)
                {
                    var node = GetRandomNode(_nodeDataPool.Events);
                    instance = CreateNode(node, layer, i);
                    type = NodeType.Event;
                }
                else if (_nodeDataPool.Traders.Count > 0)
                {
                    var node = GetRandomNode(_nodeDataPool.Traders);
                    instance = CreateNode(node, layer, i);
                    type = NodeType.Trader;
                }

                if (instance != null)
                {
                    AddToLayer(layer, instance);
                    _generatedNodes.Add(instance);
                    AddToSavedMap(instance, type, _generatedNodes.Count - 1, indexInChapter, objectiveIndex - 1, spawnerIndex - 1);
                }
            }
        }

        // Финальный босс
        var boss = CreateNode(_nodeDataPool.Boss, _totalLayers - 1, 0);
        AddToLayer(_totalLayers - 1, boss);
        _generatedNodes.Add(boss);
        AddToSavedMap(boss, NodeType.Boss, _generatedNodes.Count - 1);

        GenerateConnections();
    }

    private MissionNode CreateMissionNode(MapChapter chapter, int missionIndex, int objectiveIndex, int spawnerIndex)
    {
        var node = ScriptableObject.CreateInstance<MissionNode>();
        node.Landscape = chapter.Missions[missionIndex];
        node.Objective = chapter.Objectives[objectiveIndex];
        node.EnemiesSpawner = chapter.EnemiesSpawnerInformation[spawnerIndex];
        node.Icon = _nodeDataPool.Missions[0].Icon;

        return node;
    }

    private void AddToSavedMap(NodeInstance instance, NodeType type, int nodeIndex, int missionIndex = -1, int objectiveIndex = -1, int spawnerIndex = -1)
    {
        SavedMap.Nodes.Add(new SavedNodeData
        {
            NodeIndex = nodeIndex,
            NodeType = type,
            MissionIndex = missionIndex,
            ObjectiveIndex = objectiveIndex,
            SpawnerIndex = spawnerIndex,
            Position = instance.position,
            Layer = instance.layer,
            ConnectedNodeIndices = new List<int>()
        });
    }

    private NodeInstance CreateNode(NodeData data, int layer, int indexInLayer)
    {
        return new NodeInstance
        {
            nodeData = data,
            layer = layer,
            position = GetNodePosition(layer, indexInLayer),
            connectedNodes = new List<NodeInstance>()
        };
    }

    private void AddToLayer(int layer, NodeInstance instance)
    {
        if (!_layers.ContainsKey(layer))
            _layers[layer] = new List<NodeInstance>();
        _layers[layer].Add(instance);
    }

    private Vector2 GetNodePosition(int layer, int indexInLayer)
    {
        float totalWidth = _contentTransform.rect.width;
        float xOffset = layer * _xOffset;
        float x = xOffset - totalWidth / 2f + _leftOffset;
        float y = -((indexInLayer - 0.5f) * _yOffset + Random.Range(-_yRandomSpread, _yRandomSpread));
        return new Vector2(x, y);
    }

    private void GenerateConnections()
    {
        var sortedLayers = new List<int>(_layers.Keys);
        sortedLayers.Sort();

        for (int i = 0; i < sortedLayers.Count - 1; i++)
        {
            var currentLayer = sortedLayers[i];
            var nextLayer = sortedLayers[i + 1];

            List<NodeInstance> currentNodes = _layers[currentLayer];
            List<NodeInstance> nextNodes = _layers[nextLayer];

            foreach (var current in currentNodes)
            {
                int count = Mathf.Min(nextNodes.Count, Random.Range(1, 3));
                var targets = new List<NodeInstance>(nextNodes);
                Shuffle(targets);

                for (int j = 0; j < count; j++)
                    current.connectedNodes.Add(targets[j]);
            }
        }

        // Сохраняем связи по индексам
        for (int i = 0; i < _generatedNodes.Count; i++)
        {
            var instance = _generatedNodes[i];
            var saved = SavedMap.Nodes[i];
            foreach (var target in instance.connectedNodes)
            {
                int targetIndex = _generatedNodes.IndexOf(target);
                if (targetIndex >= 0)
                    saved.ConnectedNodeIndices.Add(targetIndex);
            }
        }
    }

    private T GetRandomNode<T>(List<T> list) where T : NodeData
    {
        if (list == null || list.Count == 0) return null;
        return list[Random.Range(0, list.Count)];
    }

    private void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randIndex = Random.Range(i, list.Count);
            (list[i], list[randIndex]) = (list[randIndex], list[i]);
        }
    }
}

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
    Mission,
    Event,
    Trader,
    Boss
}

public class NodeInstance
{
    public NodeData nodeData;
    public int layer;
    public Vector2 position;
    public List<NodeInstance> connectedNodes = new();
}

[System.Serializable]
public class NodeDataPool
{
    public List<MissionNode> Missions;
    public List<EventNode> Events;
    public List<TraderNode> Traders;
    public BossNode Boss;
}