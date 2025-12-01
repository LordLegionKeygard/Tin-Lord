using UnityEngine;

public class BiomeRockTexture : MonoBehaviour
{
    [SerializeField] private Renderer[] _targetRenderers;
    private MaterialPropertyBlock _block;

    private void Awake()
    {
        _block = new MaterialPropertyBlock();
        CustomEvents.OnDataLoad += SetTexture;
    }

    private void SetTexture()
    {
        if (_targetRenderers == null || _targetRenderers.Length == 0) return;

        var currentBiomTileTextures = CurrentMissionInfo.Instance.GetCurrentLandscape().MissionView.RockTexture;

        foreach (var renderer in _targetRenderers)
        {
            if (renderer == null) continue;
            renderer.GetPropertyBlock(_block);
            _block.SetTexture("_TopAlbedo", currentBiomTileTextures);
            renderer.SetPropertyBlock(_block);
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnDataLoad -= SetTexture;
    }
}
