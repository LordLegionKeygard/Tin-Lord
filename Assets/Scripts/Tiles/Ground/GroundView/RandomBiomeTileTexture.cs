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
        var currentBiomTileTextures = _tileTextures[(int)CurrentMissionInfo.Instance.GetCurrentMission().TileColorEnum];
        var rnd = Random.Range(0, currentBiomTileTextures.Base.Length);
        _meshRenderer.material.color = CurrentMissionInfo.Instance.GetCurrentMission().TileColor;
        _meshRenderer.material.SetTexture("_BaseMap", currentBiomTileTextures.Base[rnd]);
        _meshRenderer.material.EnableKeyword("_NORMALMAP");
        _meshRenderer.material.SetTexture("_BumpMap", _normal[rnd]);
    }
}

[System.Serializable]
public class TileTexture
{
    public TileColorEnum TileColorEnum;
    public Texture[] Base;
}

[System.Serializable]
public enum TileColorEnum
{
    Brown = 0,
    Grey = 1,
    White = 2,
}
