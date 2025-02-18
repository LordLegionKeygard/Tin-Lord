using UnityEngine;

public class BiomeRockTexture : MonoBehaviour
{
    [SerializeField] private Material[] _rockMaterials;

    private void Start()
    {
        CustomEvents.OnDataLoad += SetTexture;
    }

    private void SetTexture()
    {
        var currentBiomTileTextures = CurrentMissionInfo.Instance.GetCurrentMission().MissionView.RockTexture;

        foreach (var materials in _rockMaterials)
        {
            materials.SetTexture("_TopAlbedo", currentBiomTileTextures);
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnDataLoad -= SetTexture;
    }
}
