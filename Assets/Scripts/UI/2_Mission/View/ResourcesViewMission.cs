using System;
using UnityEngine;
using Zenject;

public class ResourcesViewMission : BaseResourceView
{
    [Inject] MissionResources _missionResources;
    
    public override void SetResourcesView(ResourceWrapper[] resources)
    {
        base.SetResourcesView(resources);

        for (int i = 0; i < resources.Length; i++)
        {
            _icons[i].sprite = _resourceSpritesInfo.Sprites[(int)resources[i].ResourceEnum];

            if (_missionResources.ResourceEnough(resources[i].ResourceEnum, resources[i].RecourceAmount))
            {
                _amountText[i].text = $"{resources[i].RecourceAmount}";

            }
            else
            {
                _amountText[i].text = $"<color={Colors.HexColorWarningYellow}>{resources[i].RecourceAmount}</color>";
            }

            _resourceCells[i].SetActive(true);
        }
    }

    public void SetReturnedResources(ResourceWrapper[] resources, float buildingHealthPercent)
    {
        ResetCells();

        for (int i = 0; i < resources.Length; i++)
        {
            float healthFactor = Mathf.Clamp01(buildingHealthPercent); // Убеждаемся, что значение в пределах [0, 1]
            float returnedAmount = resources[i].RecourceAmount * WorldGameInfo.DestroyConstructionBuildingResourcePercent * healthFactor;

            // Преобразуем в целое число
            int roundedAmount = (int)Math.Floor(returnedAmount);

            // Пропускаем, если значение меньше 1
            if (roundedAmount <= 0)
            {
                continue;
            }

            _icons[i].sprite = _resourceSpritesInfo.Sprites[(int)resources[i].ResourceEnum];
            _amountText[i].text = roundedAmount.ToString();
            _resourceCells[i].SetActive(true);
        }
    }
}
