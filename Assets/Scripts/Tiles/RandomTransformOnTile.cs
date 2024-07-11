using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTransformOnTile : MonoBehaviour
{
    [SerializeField] [Range(0f, 2.5f)] private float _randomRange = 2.5f;
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
            var rndX = Random.Range(-_randomRange, _randomRange);
            var rndZ = Random.Range(-_randomRange, _randomRange);
            transform.localPosition += new Vector3(rndX, 0, rndZ);
        }

        if (_isChangeRotation)
        {
            var rnd = Random.Range(0, 360);
            transform.localRotation = Quaternion.Euler(transform.rotation.x, rnd, transform.rotation.z);
        }
    }
}
