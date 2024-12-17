using TMPro;
using UnityEngine;

public class DestroyPanel : MonoBehaviour
{
    [SerializeField] private ResourcesView _resourcesView;
    [SerializeField] private TextMeshProUGUI _headerText;
    [SerializeField] private TextMeshProUGUI _descriptionText;

    public void SetInfo(bool isBuilding, TileObject tileObject)
    {
        _headerText.text = isBuilding ? Language.TextStatic[22] : Language.TextStatic[24];
        _descriptionText.text = isBuilding ? Language.TextStatic[23] : Language.TextStatic[25];

        if (isBuilding)
        {
            _resourcesView.SetReturnedResources(
                tileObject.BuildingTileObject().CurrentBuilding().ResourcesForBuild,
                tileObject.BuildingHealth().GetCurrentHealthPercent());
        }
        else
        {
            _resourcesView.SetResourcesView(new ResourcesForBuildWrapper[]
            {
                new ResourcesForBuildWrapper
                {
                    ResourcesForBuild = ResourceEnum.BeamEnergy,
                    RecourcesForBuildAmount = tileObject.GroundTileObject().CurrentGroundTile().EnergyBeam,
                }
            });
        }
    }
}
