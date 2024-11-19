using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ResourcesView : MonoBehaviour
{
    [Inject] PlayerResources _playerResources;
    [SerializeField] private Sprite[] _allResourceSprites;
    [SerializeField] private GameObject[] _resourceCells;
    [SerializeField] private Image[] _icons;
    [SerializeField] private TextMeshProUGUI[] _amountText;

    public void SetResourcesView(ResourcesForBuildWrapper[] resources)
    {
        ResetCells();

        for (int i = 0; i < resources.Length; i++)
        {
            _resourceCells[i].SetActive(true);
            _icons[i].sprite = _allResourceSprites[(int)resources[i].ResourcesForBuild];

            if (_playerResources.ResourceEnough(resources[i].ResourcesForBuild, resources[i].RecourcesForBuildAmount))
            {
                _amountText[i].text = $"{resources[i].RecourcesForBuildAmount}";

            }
            else
            {
                _amountText[i].text = $"<color={Colors.HexColorWarningYellow}>{resources[i].RecourcesForBuildAmount}</color>";
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
