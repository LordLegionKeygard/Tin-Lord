public class ProductionResourcePanelSpace : BaseProductionResourcePanel
{
    private LearnBuildingInfoPanel _learnBuildingInfoPanel;

    private void Awake()
    {
        _learnBuildingInfoPanel = GetComponent<LearnBuildingInfoPanel>();
    }

    public override void ChangeResourceProductionButton(int number)
    {
        base.ChangeResourceProductionButton(number);
        _learnBuildingInfoPanel.ChangeResourceProduction(number);
    }
}
