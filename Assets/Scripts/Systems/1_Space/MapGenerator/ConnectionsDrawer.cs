using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionsDrawer : MonoBehaviour
{
    [SerializeField] private MapGenerator _mapGenerator;
    [SerializeField] private RectTransform _contentTransform;
    [SerializeField] private GameObject _linePrefab;
    private float _cut = 30f; //отступ от нод

    private readonly List<GameObject> _spawnedLines = new();
    private readonly Dictionary<(int, int), Image> _lineLookup = new();


    public void DrawConnections()
    {
        ClearConnections();
        _lineLookup.Clear();

        List<NodeInstance> nodes = _mapGenerator.GetGeneratedNodes();

        for (int i = 0; i < nodes.Count; i++)
        {
            var src = nodes[i];
            foreach (var trg in src.connectedNodes)
            {
                int j = nodes.IndexOf(trg);
                var img = CreateLine(src.position, trg.position);
                _lineLookup[(i, j)] = img;
            }
        }
    }

    public void SetLineHighlight(int fromIdx, int toIdx, bool active)
    {
        if (_lineLookup.TryGetValue((fromIdx, toIdx), out var img) ||
            _lineLookup.TryGetValue((toIdx, fromIdx), out img))
        {
            img.color = active ? Color.green : Color.white;
        }
    }

    public void ClearConnections()
    {
        foreach (var go in _spawnedLines) Destroy(go);
        _spawnedLines.Clear();
        _lineLookup.Clear();
    }


    private Image CreateLine(Vector2 start, Vector2 end)
    {
        Vector2 dir = end - start;
        float distance = dir.magnitude;

        if (distance <= _cut * 2f) return null;

        Vector2 cutDir = dir.normalized;
        Vector2 p1 = start + cutDir * _cut;
        Vector2 p2 = end - cutDir * _cut;
        Vector2 center = (p1 + p2) * 0.5f;

        GameObject go = Instantiate(_linePrefab, _contentTransform);
        _spawnedLines.Add(go);

        var rt = go.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(Vector2.Distance(p1, p2), 6);
        rt.anchoredPosition = center;
        rt.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);

        return go.GetComponent<Image>();
    }

    public void ResetAllLineColors()
    {
        foreach (var lines in _lineLookup)
        {
            lines.Value.color = Color.white;
        }
    }
}
