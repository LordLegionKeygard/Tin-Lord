using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererTwoPoints : MonoBehaviour
{
    [SerializeField] private LineRenderer _lr;
    [SerializeField] private Transform[] _points;

    private Vector3 _lastPos1;
    private Vector3 _lastPos2;

    private void Start()
    {
        _lastPos1 = _points[0].position;
        _lastPos2 = _points[1].position;

        _lr.SetPosition(0, _lastPos1);
        _lr.SetPosition(1, _lastPos2);
    }

    private void LateUpdate()
    {
        Vector3 currentPos1 = _points[0].position;
        Vector3 currentPos2 = _points[1].position;

        if (currentPos1 != _lastPos1 || currentPos2 != _lastPos2)
        {
            _lr.SetPosition(0, currentPos1);
            _lr.SetPosition(1, currentPos2);

            _lastPos1 = currentPos1;
            _lastPos2 = currentPos2;
        }
    }
}
