using System.Collections.Generic;
using UnityEngine;

public class MapVisualizer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MapGenerator _mapGenerator;
    [SerializeField] private RectTransform _contentTransform; // <-- Сюда ставим твой Content
    [SerializeField] private UINode _nodePrefab;
    public IReadOnlyList<UINode> GetSpawnedNodes() => spawnedNodes;

    private List<UINode> spawnedNodes = new List<UINode>();

    public List<UINode> GenerateAndDisplayMap()
    {
        // Чистим старые ноды
        foreach (var node in spawnedNodes)
        {
            Destroy(node.gameObject);
        }

        spawnedNodes.Clear();

        List<NodeInstance> nodes = _mapGenerator.GetGeneratedNodes();
        for (int i = 0; i < nodes.Count; i++)
        {
            var ui = Instantiate(_nodePrefab, _contentTransform);
            ui.Setup(nodes[i].nodeData, i, FindObjectOfType<MapSystem>());
            ui.GetComponent<RectTransform>().anchoredPosition = nodes[i].position;
            spawnedNodes.Add(ui);
        }
        
        return spawnedNodes;
    }
}
