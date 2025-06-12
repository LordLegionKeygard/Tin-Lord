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
        const float cutLength = 30f; // расстояние, на которое линия обрезается с каждой стороны

        Vector2 direction = end - start;
        float fullDistance = direction.magnitude;

        if (fullDistance <= cutLength * 2f)
            return; // если расстояние слишком маленькое — не рисуем

        Vector2 cutDirection = direction.normalized;
        Vector2 newStart = start + cutDirection * cutLength;
        Vector2 newEnd = end - cutDirection * cutLength;
        Vector2 midPoint = (newStart + newEnd) / 2f;

        float cutDistance = Vector2.Distance(newStart, newEnd);

        GameObject line = Instantiate(_linePrefab, _contentTransform);
        var rt = line.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(cutDistance, 7);
        rt.anchoredPosition = midPoint;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rt.rotation = Quaternion.Euler(0, 0, angle);

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
