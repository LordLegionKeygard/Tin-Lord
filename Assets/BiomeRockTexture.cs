using UnityEngine;

public class BiomeRockTexture : MonoBehaviour
{
    [SerializeField] private Material[] _rockMaterials;
    [SerializeField] private BiomRockTexture[] _biomRockTextures;

    private void Start()
    {
        CustomEvents.OnDataLoad += SetTexture;
    }

    private void SetTexture()
    {
        var currentBiomTileTextures = _biomRockTextures[(int)CurrentMissionInfo.Instance.GetCurrentMission().Biome];

        foreach (var materials in _rockMaterials)
        {
            materials.SetTexture("_TopAlbedo", currentBiomTileTextures.Base);
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnDataLoad -= SetTexture;
    }
}

[System.Serializable]
public class BiomRockTexture
{
    public BiomeEnum BiomeEnum;
    public Texture Base;
}
