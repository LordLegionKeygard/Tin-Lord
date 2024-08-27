using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BuildingResourcesView : MonoBehaviour
{
    [Inject] PlayerResources _playerResources;
    [SerializeField] private Sprite[] _allResourceSprites;
    [SerializeField] private GameObject[] _resourceCells;
    [SerializeField] private Image[] _icons;
    [SerializeField] private TextMeshProUGUI[] _amountText;

    public void SetBuildingResourcesView(Building building)
    {
        ResetCells();

        for (int i = 0; i < building.ResourcesForBuild.Length; i++)
        {
            _resourceCells[i].SetActive(true);
            _icons[i].sprite = _allResourceSprites[(int)building.ResourcesForBuild[i].ResourcesForBuild];

            if (_playerResources.ResourceEnough(building.ResourcesForBuild[i].ResourcesForBuild, building.ResourcesForBuild[i].RecourcesForBuildAmount))
            {
                _amountText[i].text = $"{building.ResourcesForBuild[i].RecourcesForBuildAmount}";

            }
            else
            {
                _amountText[i].text = $"<color={Colors.HexColorYellow}>{building.ResourcesForBuild[i].RecourcesForBuildAmount}</color>";
            } 
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
