using UnityEngine;

public class RandomBiomeTileTexture : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private TileTexture[] _tileTextures;
    [SerializeField] private Texture[] _normal;

    private void Start()
    {
        SetTexture();
    }

    private void SetTexture()
    {
        var currentBiomTileTextures = _tileTextures[(int)CurrentMissionInfo.Instance.GetCurrentLandscape().MissionView.BiomEnum];
        var rnd = Random.Range(0, currentBiomTileTextures.Base.Length);
        _meshRenderer.material.color = currentBiomTileTextures.Color;
        _meshRenderer.material.SetTexture("_BaseMap", currentBiomTileTextures.Base[rnd]);
        _meshRenderer.material.EnableKeyword("_NORMALMAP");
        _meshRenderer.material.SetTexture("_BumpMap", _normal[rnd]);
    }
}

[System.Serializable]
public class TileTexture
{
    public BiomEnum BiomTextureEnum;
    public Texture[] Base;
    public Color Color;
}

[System.Serializable]
public enum BiomEnum
{
    Canyon = 0,
    Desert = 1,
    Winter = 2,
    Scorched = 3,
}
