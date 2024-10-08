using System.Linq;
using UnityEngine;
using Zenject;

public class TileObject : MonoBehaviour
{
    [Inject] PlayerResources _playerResources;
    [Inject] SelectTilePanel _selectTilePanel;
    [SerializeField] private TileObject[] _neighbourTiles;
    private GroundTile _groundTile;
    private BuildingTile _buildingTile;
    private TileEcology _tileEcology;
    private BuildingProductionView _buildingProductionView;
    private BuildingHealth _buildingHealth;
    private int _id;
    private float _currentModifier;
    private Resource _currentResourceProduction;
    private Resource _currentResourceRequired;
    private ResourceRecept[] _currentResourceRecept;
    private float _currentResourceRequiredAmount;
    public bool IsBuildingWork;
    private bool _isHaveResourceRequired = true;
    private bool _isBuildingDestroyedNow;
    public bool IsBuildingDestroyedNow() => _isBuildingDestroyedNow;
    public void ToggleIsBuildingDestroyedNow(bool state) => _isBuildingDestroyedNow = state;
    public GroundTile GroundTileObject() => _groundTile;
    public BuildingTile BuildingTileObject() => _buildingTile;
    public BuildingHealth BuildingHealth() => _buildingHealth;
    public TileEcology TileEcology() => _tileEcology;
    public int CurrentTileId() => _id;
    public int GetId() => _id;
    public float CurrentModifier() => _currentModifier;
    public Resource CurrentResourceProduction() => _currentResourceProduction;
    public Resource CurrentResourceRequired() => _currentResourceRequired;
    public float CurrentResourceRequiredAmount() => _currentResourceRequiredAmount;
    public ResourceRecept[] CurrentResourceRecept() => _currentResourceRecept;
    public void SetBuildingProductionView(BuildingProductionView buildingProductionView) => _buildingProductionView = buildingProductionView;

    //Neighbours
    public GroundTile GetNeighbourGroundTile(int number) => _neighbourTiles[number] != null ? _neighbourTiles[number].GroundTileObject() : null;
    public GroundTile[] GetNeighbourGroundTilesArray() => _neighbourTiles.Where(tile => tile != null).Select(tile => tile.GroundTileObject()).ToArray();
    public BuildingTile GetNeighbourBuildingTile(int number) => _neighbourTiles[number] != null ? _neighbourTiles[number].BuildingTileObject() : null;

    public BuildingTile[] GetNeighbourBuildingTilesArray() => _neighbourTiles.Where(tile => tile != null).Select(tile => tile.BuildingTileObject()).ToArray();

    private void Awake()
    {
        _groundTile = GetComponent<GroundTile>();
        _buildingTile = GetComponent<BuildingTile>();
        _tileEcology = GetComponent<TileEcology>();
        _buildingHealth = GetComponent<BuildingHealth>();
    }
    public bool IsHaveRequiredResource()
    {
        var haveResourcesForWork = _buildingTile.CurrentBuilding().ResourcesForWork.Length != 0 ? _playerResources.ResourceEnough(_currentResourceRequired.ResourceEnum, _currentResourceRequiredAmount) : true;
        if (!haveResourcesForWork) return false;
        if (_currentResourceRecept.Length == 0) return true;

        var haveResourceForRecept = _currentResourceRecept.All(recept =>
        _playerResources.ResourceEnough(recept.ResourceForRecept.ResourceEnum, recept.ResourcesForReceptAmount));

        return haveResourceForRecept;
    }

    public void ClearBuildingInfo()
    {
        _currentResourceProduction = null;
        _currentResourceRequired = null;
        _currentResourceRequiredAmount = 0;
        _buildingProductionView = null;
        _currentModifier = 0;
        _currentResourceRecept = null;
        CustomEvents.FireChangeResourceProduction(ResourceEnum.None, 0, _id, true);
        CustomEvents.FireChangeResourceRequired(this, null, 0, null);
    }

    public void SetId(int id) => _id = id;

    public void SetResourceModifier()
    {
        if (_buildingTile.CurrentBuildingTile() == null || !_buildingTile.CurrentBuildingTile().IsHaveProdictionResources()) return;

        _currentModifier = CalculateCurrentModifier();
        _buildingProductionView.RefreshModifierView();
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
            CheckBuildingView();
            ChangeResourceProduction();
        }
    }

    public void CheckBuildingView()
    {
        if (_buildingProductionView != null) _buildingProductionView.CheckMainBuildingView();
    }

    public void ChangeResourceProduction()
    {
        if (_buildingTile.CurrentBuildingTile() == null || GroundTileObject().IsWaterTile() || _buildingTile.CurrentBuildingTile().IsTurret) return;
        // Debug.Log("ChangeResourceProduction - CheckCount");

        var resourceWrapper = _buildingTile.CurrentBuilding();
        var resourcesProduction = IsBuildingWork
            ? _currentResourceRequired == null && _currentResourceRecept == null
            ? resourceWrapper.ResourceExtractedAmount * _currentModifier
            : (IsHaveRequiredResource() ? resourceWrapper.ResourceExtractedAmount * _currentModifier : 0) : 0;

        CustomEvents.FireChangeResourceProduction(_currentResourceProduction.ResourceEnum, resourcesProduction, _id, false);
    }

    public void SetResourceRequied(Resource resource, float amount, ResourceRecept[] resourceRecepts)
    {
        _currentResourceRequired = resource;
        _currentResourceRequiredAmount = amount;
        _currentResourceRecept = resourceRecepts;
        CustomEvents.FireChangeResourceRequired(this, _currentResourceRequired, _currentResourceRequiredAmount, _currentResourceRecept);
    }

    public void SetResourceProduction(Resource resource, ResourceRecept[] resourceRecept)
    {
        _currentResourceProduction = resource;
        _currentResourceRecept = resourceRecept;
        SetResourceModifier();
        ChangeResourceProduction();
        _selectTilePanel.SetInfo(this);
        CustomEvents.FireChangeResourceRequired(this, _currentResourceRequired, _currentResourceRequiredAmount, _currentResourceRecept);
    }
}
