using UnityEngine;

public class BiomeRockTexture : MonoBehaviour
{
    [SerializeField] private Renderer[] _targetRenderers;
    private MaterialPropertyBlock _block;

    private void Awake()
    {
        _block = new MaterialPropertyBlock();
    }

    private void Start()
    {
        SetTexture();
    }

    private void SetTexture()
    {
        var landscape = CurrentMissionInfo.Instance.GetCurrentLandscape();
        var missionView = landscape.MissionView;
        var currentBiomTileTextures = missionView.RockTexture;

        if(currentBiomTileTextures == null) return;

        foreach (var renderer in _targetRenderers)
        {
            renderer.GetPropertyBlock(_block);
            _block.SetTexture("_TopAlbedo", currentBiomTileTextures);
            renderer.SetPropertyBlock(_block);
        }
    }
}
