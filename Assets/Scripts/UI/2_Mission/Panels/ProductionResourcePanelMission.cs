public class ProductionResourcePanelMission : BaseProductionResourcePanel
{
    private SelectTilePanel _selectTilePanel;

    private void Awake()
    {
        _selectTilePanel = GetComponent<SelectTilePanel>();
    }

    public override void ChangeResourceProductionButton(int number)
    {
        base.ChangeResourceProductionButton(number);
        _selectTilePanel.ChangeResourceProduction(_productionResources[number], _lastBuilding.ResourcesProduction[number].ResourceRecept);
    }
}
