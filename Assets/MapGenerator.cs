using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Node Pools")]
    public NodeDataPool nodeDataPool;

    [Header("Generation Settings")]
    public int totalLayers = 5;
    public float xOffset = 300f;
    public float yOffset = 400f;

    public List<NodeInstance> generatedNodes = new List<NodeInstance>();

    public void GenerateDesertMap()
    {
        generatedNodes.Clear();

        // Стартовая миссия
        MissionNode startMission = GetRandomNode(nodeDataPool.Missions);
        NodeInstance startInstance = new NodeInstance
        {
            nodeData = startMission,
            position = GetNodePosition(0, 0)
        };
        generatedNodes.Add(startInstance);

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
                    position = position
                };
                generatedNodes.Add(instance);
            }
            currentLayer++;
        }

        // Финальный босс
        NodeInstance bossInstance = new NodeInstance
        {
            nodeData = nodeDataPool.Boss,
            position = GetNodePosition(totalLayers - 1, 0)
        };
        generatedNodes.Add(bossInstance);

        Debug.Log($"Карта сгенерирована: {generatedNodes.Count} нодов");
    }

    private NodeData GetNodeDataByType(NodeType type)
    {
        switch (type)
        {
            case NodeType.Mission:
                return GetRandomNode(nodeDataPool.Missions);
            case NodeType.Event:
                return GetRandomNode(nodeDataPool.Events);
            case NodeType.Trader:
                return GetRandomNode(nodeDataPool.Traders);
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

    private void Shuffle(List<NodeType> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randIndex = Random.Range(i, list.Count);
            (list[i], list[randIndex]) = (list[randIndex], list[i]);
        }
    }

    private Vector2 GetNodePosition(int layer, int indexInLayer)
    {
        float x = layer * xOffset;
        float ySpread = 100f; // максимальный сдвиг по вертикали
        float y = (indexInLayer - 0.5f) * yOffset + Random.Range(-ySpread, ySpread);
        return new Vector2(x, y);
    }

}

public class NodeInstance
{
    public NodeData nodeData;
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

