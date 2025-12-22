using UnityEngine;

public class HangarSkillsResourcesView : BaseResourceView
{
    [SerializeField] private GameObject _unknown;
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

    public void SetResources(SkillResource skillResource, bool isOpen)
    {
        if (isOpen)
        {
            var resourcesWraper = new[]
            {
                new ResourceWrapper
                {
                    ResourceEnum = skillResource.Resource.ResourceEnum,
                    RecourceAmount = skillResource.RecourceAmount
                }
            };

            SetResourcesView(resourcesWraper);
        }
        else
        {
            ResetCells();
            _unknown.SetActive(true);
        }
    }

    public override void ResetCells()
    {
        for (int i = 0; i < _resourceCells.Length; i++)
        {
            _icons[i].gameObject.SetActive(false);
            _amountText[i].gameObject.SetActive(false);
            _unknown.SetActive(false);
        }
    }
}
