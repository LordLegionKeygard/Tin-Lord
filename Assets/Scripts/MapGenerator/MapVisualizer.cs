using System.Collections.Generic;
using UnityEngine;

public class MapVisualizer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MapGenerator _mapGenerator;
    [SerializeField] private RectTransform _contentTransform; // <-- Сюда ставим твой Content
    [SerializeField] private UINode _nodePrefab;

    private List<UINode> spawnedNodes = new List<UINode>();

    public void GenerateAndDisplayMap()
    {
        // Чистим старые ноды
        foreach (var node in spawnedNodes)
            Destroy(node.gameObject);
        spawnedNodes.Clear();

        foreach (var nodeInstance in _mapGenerator.GetGeneratedNodes())
        {
            var newNode = Instantiate(_nodePrefab, _contentTransform);
            newNode.Setup(nodeInstance.nodeData);
            newNode.GetComponent<RectTransform>().anchoredPosition = nodeInstance.position;
            spawnedNodes.Add(newNode);
        }
    }
}
