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
        var biome = CurrentMissionInfo.Instance.GetCurrentLandscape().MissionView.BiomEnum;

        var currentBiomTileTextures = GetTileTextureForBiome(biome);

        // Если для биома задан готовый материал — просто подставляем его и выходим
        if (currentBiomTileTextures.OverrideMaterial != null)
        {
            _meshRenderer.material = currentBiomTileTextures.OverrideMaterial;
            return;
        }

        var rnd = Random.Range(0, currentBiomTileTextures.Base.Length); 
        _meshRenderer.material.color = currentBiomTileTextures.Color;
        _meshRenderer.material.SetTexture("_BaseMap", currentBiomTileTextures.Base[rnd]);
        _meshRenderer.material.EnableKeyword("_NORMALMAP");
        _meshRenderer.material.SetTexture("_BumpMap", _normal[rnd]);
    }

    private TileTexture GetTileTextureForBiome(BiomEnum biome)
    {
        // Ищем первый элемент с совпадающим BiomTextureEnum
        for (int i = 0; i < _tileTextures.Length; i++)
        {
            var t = _tileTextures[i];
            if (t != null && t.BiomTextureEnum == biome)
                return t;
        }

        // Фолбэк: нулевой элемент
        return _tileTextures[0];
    }
}

[System.Serializable]
public class TileTexture
{
    public BiomEnum BiomTextureEnum;
    public Material OverrideMaterial; // если задан — будет использован напрямую
    public Texture[] Base;
    public Color Color = Color.white;
}

[System.Serializable]
public enum BiomEnum
{
    Default = 0,
    Desert = 1,
    Winter = 2,
    Scorched = 3,
    Zone = 4,
    AcidForest = 5,
}
