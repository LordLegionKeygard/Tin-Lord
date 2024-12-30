
public class CommandCenterResourcesView : BaseResourceView
{
    public override void SetResourcesView(ResourceWrapper[] resources)
    {
        base.SetResourcesView(resources);

        for (int i = 0; i < resources.Length; i++)
        {
            _icons[i].sprite = _resourceSpritesInfo.Sprites[(int)resources[i].ResourceEnum];

            _amountText[i].text = $"{resources[i].RecourceAmount}";

            _resourceCells[i].SetActive(true);
        }
    }
}
