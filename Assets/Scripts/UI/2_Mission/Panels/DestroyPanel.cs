using TMPro;
using UnityEngine;

public class DestroyPanel : MonoBehaviour
{
    [SerializeField] private ResourcesViewMission _resourcesViewMission;
    [SerializeField] private TextMeshProUGUI _headerText;
    [SerializeField] private TextMeshProUGUI _descriptionText;

    public void SetInfo(bool isBuilding, TileObject tileObject)
    {
        _headerText.text = isBuilding ? Language.TextStatic[22] : Language.TextStatic[24];
        _descriptionText.text = isBuilding ? Language.TextStatic[23] : Language.TextStatic[25];

        if (isBuilding)
        {
            if (tileObject.BuildingTileObject().IsConstructionNow()) // если мы уничтожили строящееся нами здание, то возвращаем половину ресурсов, передаем 100 хп
            {
                _resourcesViewMission.SetReturnedResources(tileObject.BuildingTileObject().GetCurrentBuilding().ResourcesForBuild, 100);
            }
            else // если мы уничтожаем обычное, не строящееся здание, то показываем процент ресурсы от % текущего здоровья здания
            {
                _resourcesViewMission.SetReturnedResources(tileObject.BuildingTileObject().GetCurrentBuilding().ResourcesForBuild, tileObject.BuildingHealth().GetCurrentHealthPercent());
            }
        }
        else
        {
            _resourcesViewMission.SetResourcesView(new ResourceWrapper[]
            {
                new ResourceWrapper
                {
                    ResourceEnum = ResourceEnum.BeamEnergy,
                    RecourceAmount = (int)tileObject.GroundTileObject().CurrentGroundTile().GetEnergyBeam(),
                }
            });
        }
    }
}
