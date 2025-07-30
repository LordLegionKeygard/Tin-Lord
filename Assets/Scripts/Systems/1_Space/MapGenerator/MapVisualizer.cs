using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MapVisualizer : MonoBehaviour
{
    [Inject] private readonly DiContainer _diContainer;

    [Header("References")]
    [SerializeField] private MapGenerator _mapGenerator;
    [SerializeField] private RectTransform _contentTransform;
    [SerializeField] private UINode _nodePrefab;
    private List<UINode> spawnedNodes = new();

    public List<UINode> GenerateAndDisplayMap()
    {
        foreach (var node in spawnedNodes)
        {
            Destroy(node.gameObject);
        }

        spawnedNodes.Clear();

        List<NodeInstance> nodes = _mapGenerator.GetGeneratedNodes();
        for (int i = 0; i < nodes.Count; i++)
        {
            var go = _diContainer.InstantiatePrefab(_nodePrefab, _contentTransform);
            var uiNode = go.GetComponent<UINode>();
            uiNode.Setup(nodes[i].nodeData, i, FindObjectOfType<MapSystem>());
            uiNode.GetComponent<RectTransform>().anchoredPosition = nodes[i].position;
            spawnedNodes.Add(uiNode);
        }

        return spawnedNodes;
    }
}
