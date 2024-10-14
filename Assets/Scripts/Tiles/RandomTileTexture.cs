using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTileTexture : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Texture[] _base;
    [SerializeField] private Texture[] _normal;

    private void Start()
    {
        var rnd = Random.Range(0, _base.Length);
        _meshRenderer.material.SetTexture("_BaseMap", _base[rnd]);
        _meshRenderer.material.EnableKeyword ("_NORMALMAP");
        _meshRenderer.material.SetTexture("_BumpMap", _normal[rnd]);
    }
}
