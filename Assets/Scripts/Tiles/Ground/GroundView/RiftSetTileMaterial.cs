using UnityEngine;
using Zenject;

public class RiftSetTileMaterial : MonoBehaviour
{
    [Inject] private readonly TilesSystem _tilesSystem;
    [SerializeField] private MeshRenderer _meshRenderer;

    public void SetMaterial(int id)
    {
        _meshRenderer.material = _tilesSystem.GetGroundTileForId(id).MaterialForRift;     
    }
}
