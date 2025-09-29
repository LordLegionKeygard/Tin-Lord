using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MapVisualizer : MonoBehaviour
{
    [Inject] private readonly DiContainer _diContainer;

    [Header("References")]
    [SerializeField] private Sprite[] _mapSprites;
    [SerializeField] private TextMeshProUGUI _mapProgressText;
    [SerializeField] private MapGenerator _mapGenerator;
    [SerializeField] private RectTransform _contentTransform;
    [SerializeField] private Image _mapImage;
    [SerializeField] private UINode _nodePrefab;
    private List<UINode> spawnedNodes = new();

    public void UpdateMapProgressText(int act, int completeNodes)
    {
        _mapProgressText.text = $"{Language.TextStatic[234]}: {act + 1}           {Language.TextStatic[235]}: {completeNodes}";
    }

    public List<UINode> GenerateAndDisplayMap(int act)
    {
        _mapImage.sprite = _mapSprites[act];

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
