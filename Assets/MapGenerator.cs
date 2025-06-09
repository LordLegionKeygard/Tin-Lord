using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private RectTransform _contentTransform;

    [Header("Node Pools")]
    [SerializeField] private NodeDataPool _nodeDataPool;

    [Header("Generation Settings")]
    private int _totalLayers = 4;
    private float _xOffset = 300f;
    private float _yOffset = 300f;
    private float _yRandomSpread = 50f;
    private float _leftOffset = 100f;

    [SerializeField] private List<NodeInstance> _generatedNodes = new();
    public List<NodeInstance> GetGeneratedNodes() => _generatedNodes;

    private readonly Dictionary<int, List<NodeInstance>> _layers = new();

    public void GenerateDesertMap()
    {
        _generatedNodes.Clear();
        _layers.Clear();

        // Стартовая миссия (обязательно)
        MissionNode startMission = GetRandomNode(_nodeDataPool.Missions);
        NodeInstance startInstance = CreateNode(startMission, 0, 0);
        _generatedNodes.Add(startInstance);
        AddToLayer(0, startInstance);

        // Подготовка пулов
        var missionPool = new List<MissionNode>(_nodeDataPool.Missions);
        missionPool.Remove(startMission);
        var eventPool = new List<EventNode>(_nodeDataPool.Events);
        var traderPool = new List<TraderNode>(_nodeDataPool.Traders);
        Shuffle(missionPool);
        Shuffle(eventPool);
        Shuffle(traderPool);

        // Сначала размещаем все миссии в ближние слои
        for (int layer = 1; layer < _totalLayers - 1; layer++)
        {
            if (missionPool.Count > 0)
            {
                NodeData mission = missionPool[0];
                missionPool.RemoveAt(0);

                NodeInstance instance = CreateNode(mission, layer, 0);
                _generatedNodes.Add(instance);
                AddToLayer(layer, instance);
            }
        }

        // Гарантируем что все промежуточные слои существуют заранее
        for (int layer = 1; layer < _totalLayers - 1; layer++)
        {
            if (!_layers.ContainsKey(layer))
                _layers[layer] = new List<NodeInstance>();
        }

        // Теперь дополняем оставшиеся слоты ивентами и торговцами
        for (int layer = 1; layer < _totalLayers - 1; layer++)
        {
            int nodesInLayer = Random.Range(1, 3);
            List<NodeInstance> layerNodes = _layers[layer];

            for (int i = layerNodes.Count; i < nodesInLayer; i++)
            {
                NodeData nodeData = null;

                if (eventPool.Count > 0)
                {
                    nodeData = eventPool[0];
                    eventPool.RemoveAt(0);
                }
                else if (traderPool.Count > 0)
                {
                    nodeData = traderPool[0];
                    traderPool.RemoveAt(0);
                }

                if (nodeData == null)
                    continue;

                NodeInstance instance = CreateNode(nodeData, layer, i);
                _generatedNodes.Add(instance);
                layerNodes.Add(instance);
            }
        }

        // Финальный босс
        NodeInstance bossInstance = CreateNode(_nodeDataPool.Boss, _totalLayers - 1, 0);
        _generatedNodes.Add(bossInstance);
        AddToLayer(_totalLayers - 1, bossInstance);

        GenerateConnections();

        Debug.Log($"Карта сгенерирована: {_generatedNodes.Count} нодов");
    }

    private void AddToLayer(int layer, NodeInstance instance)
    {
        if (!_layers.ContainsKey(layer))
            _layers[layer] = new List<NodeInstance>();

        _layers[layer].Add(instance);
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
            int currentLayer = sortedLayers[i];
            int nextLayer = sortedLayers[i + 1];

            List<NodeInstance> currentLayerNodes = _layers[currentLayer];
            List<NodeInstance> nextLayerNodes = _layers[nextLayer];

            foreach (var currentNode in currentLayerNodes)
            {
                int connections = Mathf.Min(nextLayerNodes.Count, Random.Range(1, 3));

                var targets = new List<NodeInstance>(nextLayerNodes);
                Shuffle(targets);

                for (int j = 0; j < connections; j++)
                {
                    currentNode.connectedNodes.Add(targets[j]);
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
