using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    [SerializeField] private Vector3 _vector;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private bool _isRotate = true;
    [SerializeField] private bool _isRandomStartRotation = true;

    private void Start()
    {
        if(_isRandomStartRotation) SetRandomRotation();
    }

    private void SetRandomRotation()
    {
        var rnd = Random.Range(0, 360);
        transform.Rotate(0, 0, rnd);
    }
    private void Update()
    {
        if (_isRotate) transform.Rotate(_vector, _rotateSpeed * Time.deltaTime);
    }
}