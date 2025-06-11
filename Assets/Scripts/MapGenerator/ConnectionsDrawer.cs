using System.Collections.Generic;
using UnityEngine;

public class ConnectionsDrawer : MonoBehaviour
{
    [SerializeField] private MapGenerator _mapGenerator;
    [SerializeField] private RectTransform _contentTransform;
    [SerializeField] private GameObject _linePrefab;

    private readonly List<GameObject> _spawnedLines = new();

    public void DrawConnections()
    {
        ClearConnections();

        foreach (var node in _mapGenerator.GetGeneratedNodes())
        {
            foreach (var target in node.connectedNodes)
            {
                DrawLine(node.position, target.position);
            }
        }
    }

    private void DrawLine(Vector2 start, Vector2 end)
    {
        GameObject line = Instantiate(_linePrefab, _contentTransform);
        Vector2 direction = end - start;
        float distance = direction.magnitude;
        line.GetComponent<RectTransform>().sizeDelta = new Vector2(distance, 3f);
        line.GetComponent<RectTransform>().anchoredPosition = start + direction / 2f;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        line.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, angle);

        _spawnedLines.Add(line);
    }

    public void ClearConnections()
    {
        foreach (var item in _spawnedLines)
        {
            Destroy(item);
        }

        _spawnedLines.Clear();
    }
}
