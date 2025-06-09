using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Node Pools")]
    [SerializeField] private NodeDataPool _nodeDataPool;

    [Header("Generation Settings")]
    public int totalLayers = 5;
    private float _xOffset = 300f;
    private float _yOffset = 300f;
    private float _yRandomSpread = 100f;

    [SerializeField] private List<NodeInstance> _generatedNodes = new();
    public List<NodeInstance> GetGeneratedNodes() => _generatedNodes;

    private readonly Dictionary<int, List<NodeInstance>> _layers = new();

    public void GenerateDesertMap()
    {
        _generatedNodes.Clear();
        _layers.Clear();

        // Стартовая миссия
        MissionNode startMission = GetRandomNode(_nodeDataPool.Missions);
        NodeInstance startInstance = new NodeInstance
        {
            nodeData = startMission,
            layer = 0,
            position = GetNodePosition(0, 0)
        };
        _generatedNodes.Add(startInstance);
        _layers[0] = new List<NodeInstance> { startInstance };

        // Промежуточные
        List<NodeType> nodeTypes = new List<NodeType>
        {
            NodeType.Mission,
            NodeType.Event,
            NodeType.Event,
            NodeType.Trader,
            NodeType.Trader
        };

        Shuffle(nodeTypes);
        int currentLayer = 1;

        while (nodeTypes.Count > 0 && currentLayer < totalLayers - 1)
        {
            int nodesInLayer = Random.Range(1, 3);
            List<NodeInstance> layerNodes = new List<NodeInstance>();

            for (int i = 0; i < nodesInLayer; i++)
            {
                if (nodeTypes.Count == 0) break;

                NodeType type = nodeTypes[0];
                nodeTypes.RemoveAt(0);

                NodeData nodeData = GetNodeDataByType(type);
                Vector2 position = GetNodePosition(currentLayer, i);
                NodeInstance instance = new NodeInstance
                {
                    nodeData = nodeData,
                    layer = currentLayer,
                    position = position
                };

                _generatedNodes.Add(instance);
                layerNodes.Add(instance);
            }

            _layers[currentLayer] = layerNodes;
            currentLayer++;
        }

        // Финальный босс
        NodeInstance bossInstance = new NodeInstance
        {
            nodeData = _nodeDataPool.Boss,
            layer = totalLayers - 1,
            position = GetNodePosition(totalLayers - 1, 0)
        };
        _generatedNodes.Add(bossInstance);
        _layers[totalLayers - 1] = new List<NodeInstance> { bossInstance };

        GenerateConnections();

        Debug.Log($"Карта сгенерирована: {_generatedNodes.Count} нодов");
    }

    private NodeData GetNodeDataByType(NodeType type)
    {
        switch (type)
        {
            case NodeType.Mission:
                return GetRandomNode(_nodeDataPool.Missions);
            case NodeType.Event:
                return GetRandomNode(_nodeDataPool.Events);
            case NodeType.Trader:
                return GetRandomNode(_nodeDataPool.Traders);
            default:
                return null;
        }
    }

    private T GetRandomNode<T>(List<T> list) where T : NodeData
    {
        if (list == null || list.Count == 0) return null;
        int index = Random.Range(0, list.Count);
        return list[index];
    }

    private void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randIndex = Random.Range(i, list.Count);
            (list[i], list[randIndex]) = (list[randIndex], list[i]);
        }
    }


    private Vector2 GetNodePosition(int layer, int indexInLayer)
    {
        float x = layer * _xOffset;
        float y = (indexInLayer - 0.5f) * _yOffset + Random.Range(-_yRandomSpread, _yRandomSpread);
        return new Vector2(x, y);
    }

    private void GenerateConnections()
    {
        for (int layer = 0; layer < totalLayers - 1; layer++)
        {
            if (!_layers.ContainsKey(layer) || !_layers.ContainsKey(layer + 1))
                continue;

            List<NodeInstance> currentLayerNodes = _layers[layer];
            List<NodeInstance> nextLayerNodes = _layers[layer + 1];

            foreach (var currentNode in currentLayerNodes)
            {
                int connections = Random.Range(1, 3);
                var targets = new List<NodeInstance>(nextLayerNodes);
                Shuffle(targets);

                for (int i = 0; i < Mathf.Min(connections, targets.Count); i++)
                {
                    currentNode.connectedNodes.Add(targets[i]);
                }
            }
        }
    }
}

public class NodeInstance
{
    public NodeData nodeData;
    public int layer;
    public Vector2 position;
    public List<NodeInstance> connectedNodes = new List<NodeInstance>();
}

[System.Serializable]
public class NodeDataPool
{
    public List<MissionNode> Missions;
    public List<EventNode> Events;
    public List<TraderNode> Traders;
    public BossNode Boss;
}
