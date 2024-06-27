using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTransformOnTile : MonoBehaviour
{
    [SerializeField] private bool _isChangePosition;
    [SerializeField] private bool _isChangeRotation;
    private void Start()
    {
        SetRandomTransform();
    }

    private void SetRandomTransform()
    {
        if (_isChangePosition)
        {
            var rndX = Random.Range(-2.8f, 2.8f);
            var rndZ = Random.Range(-2.8f, 2.8f);
            transform.localPosition += new Vector3(rndX, 0, rndZ);
        }

        if (_isChangeRotation)
        {
            var rnd = Random.Range(0, 360);
            transform.localRotation = Quaternion.Euler(transform.rotation.x, rnd, transform.rotation.z);
        }
    }
}
