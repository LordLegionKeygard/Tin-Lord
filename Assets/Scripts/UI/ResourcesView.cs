using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ResourcesView : MonoBehaviour
{
    [Inject] PlayerResources _playerResources;
    [SerializeField] private ResourceSpritesInfo _resourceSpritesInfo;
    [SerializeField] private GameObject[] _resourceCells;
    [SerializeField] private Image[] _icons;
    [SerializeField] private TextMeshProUGUI[] _amountText;

    public void SetResourcesView(ResourcesForBuildWrapper[] resources)
    {
        ResetCells();

        for (int i = 0; i < resources.Length; i++)
        {
            _icons[i].sprite = _resourceSpritesInfo.Sprites[(int)resources[i].ResourcesForBuild];

            if (_playerResources.ResourceEnough(resources[i].ResourcesForBuild, resources[i].RecourcesForBuildAmount))
            {
                _amountText[i].text = $"{resources[i].RecourcesForBuildAmount}";

            }
            else
            {
                _amountText[i].text = $"<color={Colors.HexColorWarningYellow}>{resources[i].RecourcesForBuildAmount}</color>";
            }

            _resourceCells[i].SetActive(true);
        }
    }

    public void SetReturnedResources(ResourcesForBuildWrapper[] resources, float buildingHealthPercent)
    {
        ResetCells();

        for (int i = 0; i < resources.Length; i++)
        {
            float healthFactor = Mathf.Clamp01(buildingHealthPercent); // Убеждаемся, что значение в пределах [0, 1]
            float returnedAmount = resources[i].RecourcesForBuildAmount / 2 * healthFactor;

            // Преобразуем в целое число
            int roundedAmount = (int)Math.Floor(returnedAmount);

            // Пропускаем, если значение меньше 1
            if (roundedAmount <= 0)
            {
                continue;
            }

            _icons[i].sprite = _resourceSpritesInfo.Sprites[(int)resources[i].ResourcesForBuild];
            _amountText[i].text = roundedAmount.ToString();
            _resourceCells[i].SetActive(true);
        }
    }

    public void ResetCells()
    {
        foreach (var item in _resourceCells)
        {
            item.SetActive(false);
        }
    }
}
