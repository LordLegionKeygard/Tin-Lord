using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GeneralRepairSkill : BaseSkill
{
    [SerializeField] private PlayerResources _playerResources;
    [SerializeField] private List<TileObject> _repairList;


    private void Start()
    {
        CustomEvents.OnChangeGeneralRepairTileObject += ChangeGeneralRepairTileObject;
        CustomEvents.OnBuildingDestroyed += BuildingDestroyed;
    }

    public void ChangeGeneralRepairTileObject(TileObject tileObject)
    {
        if (tileObject.IsGeneralRepairSelect())
        {
            if (!_repairList.Contains(tileObject))
            {
                _repairList.Add(tileObject);
            }
        }
        else
        {
            _repairList.Remove(tileObject);
        }
    }

    private void BuildingDestroyed(int id)
    {
        for (int i = 0; i < _repairList.Count; i++)
        {
            if (_repairList[i].GetId() == id)
            {
                _repairList.Remove(_repairList[i]);
            }
        }
    }

    public override void UseSkill()
    {       
        if (_repairList.Count == 0 || SkillView.IsCooldownNow() && IsOpen())
        {
            AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Error], transform.position);
            return;
        }


        AudioManager.Instance.PlayerOneShot(GetSkill().Sound, transform.position);

        for (int i = 0; i < _repairList.Count; i++)
        {
            var resources = GetResourcesForRepair(_repairList[i]);
            if (_playerResources.ResourcesEnough(resources) && !_repairList[i].BuildingTileObject().IsConstructionNow())
            {
                _playerResources.UseResourcesForBuilding(resources);
                _repairList[i].BuildingHealth().FullRepair();
            }
        }

        SkillView.StartSkillCooldown();
        CheckDuration(GetSkill().DurationTicks);
    }

    public ResourceWrapper[] GetResourcesForRepair(TileObject tileObject)
    {
        var building = tileObject.BuildingTileObject().CurrentBuilding();

        var buildingHealth = tileObject.BuildingHealth();
        float healthPercentage = (float)(buildingHealth.GetMaxHealth() - buildingHealth.GetCurrentHealth()) / buildingHealth.GetMaxHealth();

        return building.ResourcesForBuild.Select(resource => new ResourceWrapper
        {
            ResourceEnum = resource.ResourceEnum,
            RecourceAmount = Mathf.CeilToInt(resource.RecourceAmount * healthPercentage)
        }).ToArray();
    }

    private void OnDestroy()
    {
        CustomEvents.OnChangeGeneralRepairTileObject -= ChangeGeneralRepairTileObject;
        CustomEvents.OnBuildingDestroyed -= BuildingDestroyed;
    }
}
