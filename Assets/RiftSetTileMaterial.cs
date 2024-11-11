using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiftSetTileMaterial : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Material[] _allMaterials;

    public void SetMaterial(GroundTileViewEnum groundTileViewEnum)
    {
        _meshRenderer.material = _allMaterials[(int)groundTileViewEnum - 1];
    }
}
