using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererPoints : MonoBehaviour
{
    [SerializeField] private LineRenderer _lr;
    [SerializeField] private Transform[] _points;

    private void LateUpdate()
    {
        _lr.SetPosition(0, _points[0].position);
        _lr.SetPosition(1, _points[1].position);
    }
}
