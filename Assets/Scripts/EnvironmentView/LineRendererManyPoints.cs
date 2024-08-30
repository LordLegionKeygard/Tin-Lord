using UnityEngine;

public class LineRendererManyPoints : MonoBehaviour
{
    [SerializeField] private LineRenderer _lr;

    [SerializeField] private Transform[] _points;

    private Vector3[] _previousPositions;

    private void Start()
    {
        _lr.positionCount = _points.Length;
        _previousPositions = new Vector3[_points.Length];
        UpdateLineRenderer();
    }

    private void LateUpdate()
    {
        bool needsUpdate = false;

        for (int i = 0; i < _points.Length; i++)
        {
            if (_points[i].position != _previousPositions[i])
            {
                _previousPositions[i] = _points[i].position;
                needsUpdate = true;
            }
        }

        if (needsUpdate)
        {
            UpdateLineRenderer();
        }
    }

    private void UpdateLineRenderer()
    {
        for (int i = 0; i < _points.Length; i++)
        {
            _lr.SetPosition(i, _points[i].position);
        }
    }
}