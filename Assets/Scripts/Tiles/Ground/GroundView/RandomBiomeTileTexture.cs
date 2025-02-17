using UnityEngine;

public class RandomBiomeTileTexture : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private BiomTileTexture[] _biomTileTextures;
    [SerializeField] private Texture[] _normal;

    private void Start()
    {
        SetTexture();
    }

    private void SetTexture()
    {
        var currentBiomTileTextures = _biomTileTextures[(int)CurrentMissionInfo.Instance.GetCurrentMission().MusicTheme];
        var rnd = Random.Range(0, currentBiomTileTextures.Base.Length);
        _meshRenderer.material.SetTexture("_BaseMap", currentBiomTileTextures.Base[rnd]);
        _meshRenderer.material.EnableKeyword("_NORMALMAP");
        _meshRenderer.material.SetTexture("_BumpMap", _normal[rnd]);
    }
}

[System.Serializable]
public class BiomTileTexture
{
    public BiomeEnum BiomeEnum;
    public Texture[] Base;
}
