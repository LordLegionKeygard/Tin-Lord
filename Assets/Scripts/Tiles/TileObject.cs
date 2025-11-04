using System.Linq;
using UnityEngine;
using Zenject;

public class TileObject : MonoBehaviour
{
    [Inject] MissionResources _missionResources;
    [Inject] SelectTilePanel _selectTilePanel;
    private TileObject[] _neighbourTiles = new TileObject[8];
    private bool _isGeneralRepairSelect = true;
    private GroundTile _groundTile;
    private BuildingTile _buildingTile;
    private TileEcology _tileEcology;
    private TileObjectEvents _tileObjectEvents;
    private BuildingProductionView _buildingProductionView;
    private BuildingHealth _buildingHealth;
    private int _id;
    private float _currentModifier;
    private Resource _currentResourceProduction;
    private Resource _currentResourceForWork;
    private ResourceRecept[] _currentResourceRecept;
    private float _currentResourceForWorkAmount;
    private bool _isBuildingWork;
    private bool _isHaveResourceRequired = true;
    private bool _isBuildingDestroyedNow;
    private bool _isGroundDestroyedNow;
    private int _riftViewTileId = -1;
    public bool IsGeneralRepairSelect() => _isGeneralRepairSelect;
    public int GetRiftViewTileId() => _riftViewTileId;
    public bool IsBuildingWork() => _isBuildingWork;
    public bool IsBuildingDestroyedNow() => _isBuildingDestroyedNow;
    public void ToggleIsBuildingDestroyedNow(bool state) => _isBuildingDestroyedNow = state;
    public bool IsGroundDestroyedNow() => _isGroundDestroyedNow;
    public void ToggleIsGroundDestroyedNow(bool state) => _isGroundDestroyedNow = state;
    public GroundTile GroundTileObject() => _groundTile;
    public BuildingTile BuildingTileObject() => _buildingTile;
    public BuildingHealth BuildingHealth() => _buildingHealth;
    public TileEcology TileEcology() => _tileEcology;
    public TileObjectEvents GetTileObjectEvents() => _tileObjectEvents;
    public int CurrentTileId() => _id;
    public int GetId() => _id;
    public float CurrentModifier() => _currentModifier;
    public Resource CurrentResourceProduction() => _currentResourceProduction;
    public Resource CurrentResourceForWork() => _currentResourceForWork;
    public float CurrentResourceForWorkAmount() => _currentResourceForWorkAmount;
    public ResourceRecept[] CurrentResourceRecept() => _currentResourceRecept;
    public void SetBuildingProductionView(BuildingProductionView buildingProductionView) => _buildingProductionView = buildingProductionView;
    public void SetGeneralRepairSelect(bool state) => _isGeneralRepairSelect = state;
    public void SetRiftViewTileId(int id) => _riftViewTileId = id;

    //Neighbours
    public TileObject GetNeighbourTileObject(int number) => _neighbourTiles[number] != null ? _neighbourTiles[number] : null;
    public GroundTile GetNeighbourGroundTile(int number) => _neighbourTiles[number] != null ? _neighbourTiles[number].GroundTileObject() : null;
    public GroundTile[] GetNeighbourGroundTilesArray() => _neighbourTiles.Select(tile => tile?.GroundTileObject()).ToArray();
    public BuildingTile GetNeighbourBuildingTile(int number) => _neighbourTiles[number] != null ? _neighbourTiles[number].BuildingTileObject() : null;
    public BuildingHealth[] GetNeighbourBulidingHealthArray() => _neighbourTiles.Where(tile => tile != null).Select(tile => tile.BuildingHealth()).ToArray();

    private void Awake()
    {
        _groundTile = GetComponent<GroundTile>();
        _buildingTile = GetComponent<BuildingTile>();
        _tileEcology = GetComponent<TileEcology>();
        _buildingHealth = GetComponent<BuildingHealth>();
        _tileObjectEvents = GetComponent<TileObjectEvents>();
    }

    public void SetBuildingWork(bool state)
    {
        _isBuildingWork = state;
        _buildingHealth.GetBuildingSliderWorkView().SetIsBuildingWorkView(_isBuildingWork);
    }

    public TileObject GetNearNeighbourCrossRoad()
    {
        if (!_groundTile.CurrentGroundTile().CanBuildMachineProduction) return null;
        for (int i = 0; i < _neighbourTiles.Length; i++)
        {
            if (i is (int)TileDirectionEnum.NorthEast or (int)TileDirectionEnum.NorthWest or (int)TileDirectionEnum.SouthEast or (int)TileDirectionEnum.SouthWest) continue;
            if (_neighbourTiles[i] != null && _neighbourTiles[i].GroundTileObject().CheckTileView(GroundTileViewEnum.Road)) return _neighbourTiles[i];
        }
        return null;
    }

    public bool IsHaveRequiredResource()
    {
        var haveResourcesForWork = _buildingTile.GetCurrentBuilding().ResourcesForWork.Length == 0 || _missionResources.ResourceEnough(_currentResourceForWork.ResourceEnum, _currentResourceForWorkAmount);
        if (!haveResourcesForWork)
        {
            if (_buildingTile.IsEcologyBuilding())
            {
                SetBuildingWork(false); // отключаем полностью только здания по очистке экологии, если закончился ресурс
            }
            return false;
        }
        if (_currentResourceRecept == null || _currentResourceRecept.Length == 0) return true;

        var haveResourceForRecept = _currentResourceRecept.All(recept =>
        _missionResources.ResourceEnough(recept.ResourceForRecept.ResourceEnum, recept.ResourcesForReceptAmount));

        return haveResourceForRecept;
    }

    public void ClearBuildingInfoAndProduction()
    {
        _currentResourceForWork = null;
        _currentResourceForWorkAmount = 0;
        _currentResourceRecept = null;
        _currentResourceProduction = null;
        _buildingProductionView = null;
        _currentModifier = 0;
        _isGeneralRepairSelect = true;
        CustomEvents.FireChangeResourceProduction(ResourceEnum.None, 0, _id, true);
        CustomEvents.FireChangeResourceForWork(this, null, 0, null);
    }

    public void ClearResourceProductionAndRequiredWhenBuildingConstruct()
    {
        CustomEvents.FireChangeResourceProduction(ResourceEnum.None, 0, _id, true);
        CustomEvents.FireChangeResourceForWork(this, null, 0, null);
    }

    public void SetId(int id) => _id = id;

    public void SetResourceModifier()
    {
        _currentModifier = CalculateCurrentModifier();
        if (_buildingProductionView != null) _buildingProductionView.RefreshModifierView();
        ChangeResourceProduction();
    }

    public void SetNeighbourTiles(TileObject[] array)
    {
        for (int i = 0; i < _neighbourTiles.Length; i++)
        {
            if (array[i] == null) continue;
            _neighbourTiles[i] = array[i];
        }
    }

    private float CalculateCurrentModifier()
    {
        foreach (var productionFromGroundResources in GroundTileObject().CurrentGroundTile().ProductionOnGroundResources)
        {
            if (productionFromGroundResources.ProductionOnGroundResource == _currentResourceProduction)
            {
                return productionFromGroundResources.ProductionOnGroundResourceModifier;
            }
        }
        return 0;
    }

    public void CheckResourceRequired(bool needCheck)
    {
        var state = IsHaveRequiredResource();

        if (state != _isHaveResourceRequired || needCheck)
        {
            _isHaveResourceRequired = state;

            CustomEvents.FireChangeEcology(TileEcology().GetEcology(GetEcologyEnum.Total), GetId(), false); //обновляем экологию здания, после изменения состояния его работы

            CheckBuildingView(state);
            ChangeResourceProduction();
        }
    }

    // Обновляем визуал хватает ли нам ресурса для работы на слайдере и у самого здания
    public void CheckBuildingView(bool isHaveRequiredResource)
    {
        _buildingHealth.GetBuildingSliderWorkView()?.SetIsHaveRequiredResourceView(isHaveRequiredResource);
        _buildingProductionView?.CheckMainBuildingView();
    }

    public void ChangeResourceProduction()
    {
        if (_currentResourceProduction == null || _buildingTile.GetCurrentBuildingTile() == null || !GroundTileObject().IsHaveBuildingTypes() || _buildingTile.IsConstructionNow() ||
           (_buildingTile.GetCurrentBuildingTile().BuildingTileView is BuildingTileViewEnum.AttackingStructures or
            BuildingTileViewEnum.Walls or BuildingTileViewEnum.EcologyPurifier or BuildingTileViewEnum.RadioCommunication or BuildingTileViewEnum.Bridge or BuildingTileViewEnum.MachineProduction or BuildingTileViewEnum.Traps or BuildingTileViewEnum.Gates)) return;

        var resourceWrapper = _buildingTile.GetCurrentBuilding();
        var resourcesProduction = _isBuildingWork
            ? _currentResourceForWork == null && _currentResourceRecept == null
            ? resourceWrapper.ResourceExtractedAmount * _currentModifier
            : (IsHaveRequiredResource() ? resourceWrapper.ResourceExtractedAmount * _currentModifier : 0) : 0;

        CustomEvents.FireChangeResourceProduction(_currentResourceProduction.ResourceEnum, resourcesProduction, _id, false);
    }

    public void SetResourceForWork(Resource resource, float amount, ResourceRecept[] resourceRecepts)
    {
        _currentResourceForWork = resource;
        _currentResourceForWorkAmount = amount;
        _currentResourceRecept = resourceRecepts;
        CustomEvents.FireChangeResourceForWork(this, _currentResourceForWork, _currentResourceForWorkAmount, _currentResourceRecept);
    }

    public void SetNewResourceRequiredAfterUpgradeBuilding(ResourcesForWorkWrapper[] allResourcesForWorkWrapper, ResourceRecept[] resourceRecepts)
    {
        for (int i = 0; i < allResourcesForWorkWrapper.Length; i++)
        {
            if (_currentResourceForWork == allResourcesForWorkWrapper[i].ResourceForWork && _currentResourceForWorkAmount == allResourcesForWorkWrapper[i].ResourcesForWorkAmount) //если находим текущий ресурс для работы у нового здания, значит ничего менять не нужно
            {
                //здесь не требуется обновлять требуемые ресурсы как у производимых, потому что метод SetResourceProduction() вызывает FireChangeResourceRequired()
                return;
            }
        }

        //если текущий для работы ресурс не совпадает с ресурсами для работы нового здания, значит текущий ресурс больше нельзя использовать, обновляем на [0]
        var wrapper = allResourcesForWorkWrapper[0];
        SetResourceForWork(wrapper.ResourceForWork, wrapper.ResourcesForWorkAmount, resourceRecepts);
    }

    public void SetResourceProduction(Resource resource, ResourceRecept[] resourceRecept)
    {
        _currentResourceProduction = resource;
        _currentResourceRecept = resourceRecept;
        SetResourceModifier();
        ChangeResourceProduction();
        _selectTilePanel.RefreshInfo();
        CustomEvents.FireChangeResourceForWork(this, _currentResourceForWork, _currentResourceForWorkAmount, _currentResourceRecept);
    }

    public void SetNewResourceProductionAfterUpgradeBuilding(ResourcesProductionWrapper[] allResourcesProductionWrapper)
    {
        for (int i = 0; i < allResourcesProductionWrapper.Length; i++)
        {
            if (_currentResourceProduction == allResourcesProductionWrapper[i].ProductionResource)
            {
                SetResourceProduction(allResourcesProductionWrapper[i].ProductionResource, allResourcesProductionWrapper[i].ResourceRecept);
                return;
            }
        }

        SetResourceProduction(allResourcesProductionWrapper[0].ProductionResource, allResourcesProductionWrapper[0].ResourceRecept);
    }
}
