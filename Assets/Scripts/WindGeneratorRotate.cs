using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindGeneratorRotate : MonoBehaviour
{
    [SerializeField] private Vector3 _vector;
    [SerializeField] private float _rotateSpeed;
    public bool IsRotate = true;

    private void Start()
    {
        SetRandomRotation();
    }

    private void SetRandomRotation()
    {
        var rnd = Random.Range(0, 360);
        transform.Rotate(0, 0, rnd);
    }
    private void Update()
    {
        if (IsRotate) transform.Rotate(_vector, _rotateSpeed * Time.deltaTime);
    }
}