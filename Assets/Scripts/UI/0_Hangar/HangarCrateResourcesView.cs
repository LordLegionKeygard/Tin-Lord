using UnityEngine;

public class HangarCrateResourcesView : BaseResourceView
{
    [SerializeField] private GameObject[] _unknown;
    public override void SetResourcesView(ResourceWrapper[] resources)
    {
        base.SetResourcesView(resources);

        for (int i = 0; i < resources.Length; i++)
        {
            _icons[i].sprite = _resourceSpritesInfo.Sprites[(int)resources[i].ResourceEnum];
            _amountText[i].text = $"{resources[i].RecourceAmount}";
            _icons[i].gameObject.SetActive(true);
            _amountText[i].gameObject.SetActive(true);
        }
    }

    public void SetResources(ResourceWrapper[] resources, bool isOpen)
    {
        if (isOpen)
        {
            SetResourcesView(resources);
        }
        else
        {
            ResetCells();
            for (int i = 0; i < resources.Length; i++)
            {
                _unknown[i].SetActive(true);
            }
        }
    }

    public override void ResetCells()
    {
        for (int i = 0; i < _resourceCells.Length; i++)
        {
            _icons[i].gameObject.SetActive(false);
            _amountText[i].gameObject.SetActive(false);
            _unknown[i].SetActive(false);
        }
    }
}
