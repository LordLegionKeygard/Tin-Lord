using System.Collections.Generic;
using UnityEngine;

public class MapVisualizer : MonoBehaviour
{
    [Header("References")]
    public MapGenerator mapGenerator;
    public RectTransform contentTransform; // <-- Сюда ставим твой Content
    public UINode nodePrefab;

    private List<UINode> spawnedNodes = new List<UINode>();

    private void Start()
    {
        GenerateAndDisplayMap();
    }

    public void GenerateAndDisplayMap()
    {
        mapGenerator.GenerateDesertMap();

        // Чистим старые ноды
        foreach (var node in spawnedNodes)
            Destroy(node.gameObject);
        spawnedNodes.Clear();

        foreach (var nodeInstance in mapGenerator.generatedNodes)
        {
            var newNode = Instantiate(nodePrefab, contentTransform);
            newNode.Setup(nodeInstance.nodeData);
            newNode.GetComponent<RectTransform>().anchoredPosition = nodeInstance.position;
            spawnedNodes.Add(newNode);
        }
    }
}
