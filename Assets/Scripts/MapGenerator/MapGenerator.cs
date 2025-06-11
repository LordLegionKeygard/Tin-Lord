using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private RectTransform _contentTransform;
    [SerializeField] private AllMissionsInfo _allMissionsInfo;

    [Header("Generation Settings")]
    [SerializeField] private float _nodeXoffset = 300f;
    [SerializeField] private float _nodeYoffset = 200f;
    [SerializeField] private float _nodeYrandomSpread = 25f;
    [SerializeField] private float _mainXOffset = 100f;
    [SerializeField] private float _mainYOffset = -150f;

    [SerializeField] private List<NodeInstance> _generatedNodes = new();
    public List<NodeInstance> GetGeneratedNodes() => _generatedNodes;
    public SavedMapData SavedMap = new();

    private readonly Dictionary<int, List<NodeInstance>> _layers = new();

    public void GenerateMap()
    {
        _generatedNodes.Clear();
        _layers.Clear();
        SavedMap = new SavedMapData();

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

        int totalContentNodes = landscapes.Count + events.Count + traders.Count;
        int maxNodesPerLayer = 3;
        int contentLayers = Mathf.CeilToInt(totalContentNodes / (float)maxNodesPerLayer);
        int totalLayers = contentLayers + 3; // start, at least one mission, boss

        int objectiveIndex = 0;
        int spawnerIndex = 0;

        // Стартовая нода
        NodeInstance startNode = CreateNode(_allMissionsInfo.StartNode, 0, 0);
        AddToLayer(0, startNode);
        _generatedNodes.Add(startNode);
        AddToSavedMap(startNode, NodeType.Start, 0);

        // Первая миссия
        MissionNode firstMission = CreateMissionNode(landscapes, objectives, spawners, ref objectiveIndex, ref spawnerIndex);
        NodeInstance firstMissionNode = CreateNode(firstMission, 1, 0);
        AddToLayer(1, firstMissionNode);
        _generatedNodes.Add(firstMissionNode);
        AddToSavedMap(firstMissionNode, NodeType.Mission, _generatedNodes.Count - 1);

        for (int layer = 2; layer < totalLayers - 1; layer++)
        {
            int nodesThisLayer = 0;
            while (nodesThisLayer < maxNodesPerLayer && (landscapes.Count > 0 || events.Count > 0 || traders.Count > 0))
            {
                NodeInstance instance = null;
                NodeType type = NodeType.Event;

                if (landscapes.Count > 0)
                {
                    var mission = CreateMissionNode(landscapes, objectives, spawners, ref objectiveIndex, ref spawnerIndex);
                    instance = CreateNode(mission, layer, nodesThisLayer);
                    type = NodeType.Mission;
                }
                else if (events.Count > 0)
                {
                    instance = CreateNode(events[0], layer, nodesThisLayer);
                    events.RemoveAt(0);
                    type = NodeType.Event;
                }
                else if (traders.Count > 0)
                {
                    instance = CreateNode(traders[0], layer, nodesThisLayer);
                    traders.RemoveAt(0);
                    type = NodeType.Trader;
                }

                if (instance != null)
                {
                    AddToLayer(layer, instance);
                    _generatedNodes.Add(instance);
                    AddToSavedMap(instance, type, _generatedNodes.Count - 1);
                    nodesThisLayer++;
                }
            }
        }

        // Финальный босс
        NodeInstance boss = CreateNode(_allMissionsInfo.BossNode, totalLayers - 1, 0);
        AddToLayer(totalLayers - 1, boss);
        _generatedNodes.Add(boss);
        AddToSavedMap(boss, NodeType.Boss, _generatedNodes.Count - 1);

        GenerateConnections();
    }

    private MissionNode CreateMissionNode(List<Landscape> landscapes, List<Objective> objectives, List<EnemiesSpawner> spawners, ref int objectiveIndex, ref int spawnerIndex)
    {
        var node = ScriptableObject.CreateInstance<MissionNode>();
        node.Landscape = landscapes[0]; landscapes.RemoveAt(0);
        node.Objective = objectives[objectiveIndex++ % objectives.Count];
        node.EnemiesSpawner = spawners[spawnerIndex++ % spawners.Count];
        node.Icon = _allMissionsInfo.MissionNodeTemplate.Icon;
        return node;
    }

    private void AddToSavedMap(NodeInstance instance, NodeType type, int nodeIndex)
    {
        SavedMap.Nodes.Add(new SavedNodeData
        {
            NodeIndex = nodeIndex,
            NodeType = type,
            MissionIndex = -1,
            ObjectiveIndex = -1,
            SpawnerIndex = -1,
            Position = instance.position,
            Layer = instance.layer,
            ConnectedNodeIndices = new List<int>()
        });
    }

    private NodeInstance CreateNode(NodeData data, int layer, int indexInLayer)
    {
        int nodesInLayer = _layers.ContainsKey(layer) ? _layers[layer].Count + 1 : 1;
        return new NodeInstance
        {
            nodeData = data,
            layer = layer,
            position = GetNodePosition(layer, indexInLayer, nodesInLayer),
            connectedNodes = new List<NodeInstance>()
        };
    }

    private void AddToLayer(int layer, NodeInstance instance)
    {
        if (!_layers.ContainsKey(layer))
            _layers[layer] = new List<NodeInstance>();
        _layers[layer].Add(instance);
    }

    private Vector2 GetNodePosition(int layer, int indexInLayer, int nodesInLayer)
    {
        float totalWidth = _contentTransform.rect.width;
        float x = layer * _nodeXoffset - totalWidth / 2f + _mainXOffset;

        float layerHeight = (nodesInLayer - 1) * _nodeYoffset;
        float y = -indexInLayer * _nodeYoffset + layerHeight / 2f + _mainYOffset + Random.Range(-_nodeYrandomSpread, _nodeYrandomSpread);

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
                int count = Mathf.Min(nextNodes.Count, Random.Range(1, 4));
                var targets = new List<NodeInstance>(nextNodes);
                Shuffle(targets);

                for (int j = 0; j < count; j++)
                    current.connectedNodes.Add(targets[j]);
            }
        }

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
