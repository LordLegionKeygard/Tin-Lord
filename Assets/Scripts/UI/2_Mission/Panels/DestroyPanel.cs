using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class DestroyPanel : MonoBehaviour
{
    [FormerlySerializedAs("_resourcesView")] [SerializeField] private ResourcesViewMission resourcesViewMission;
    [SerializeField] private TextMeshProUGUI _headerText;
    [SerializeField] private TextMeshProUGUI _descriptionText;

    public void SetInfo(bool isBuilding, TileObject tileObject)
    {
        _headerText.text = isBuilding ? Language.TextStatic[22] : Language.TextStatic[24];
        _descriptionText.text = isBuilding ? Language.TextStatic[23] : Language.TextStatic[25];

        if (isBuilding)
        {
            resourcesViewMission.SetReturnedResources(
                tileObject.BuildingTileObject().CurrentBuilding().ResourcesForBuild,
                tileObject.BuildingHealth().GetCurrentHealthPercent());
        }
        else
        {
            resourcesViewMission.SetResourcesView(new ResourceWrapper[]
            {
                new ResourceWrapper
                {
                    ResourceEnum = ResourceEnum.BeamEnergy,
                    RecourceAmount = tileObject.GroundTileObject().CurrentGroundTile().GetEnergyBeam(),
                }
            });
        }
    }
}
