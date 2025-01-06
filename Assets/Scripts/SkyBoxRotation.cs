using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxRotation : MonoBehaviour
{
    [SerializeField] private Material _skyBoxMat;

    private float _value;

    private void FixedUpdate()
    {
        _value += 0.1f * Time.deltaTime;
        _skyBoxMat.SetFloat("_Rotation", _value);
    }
}
