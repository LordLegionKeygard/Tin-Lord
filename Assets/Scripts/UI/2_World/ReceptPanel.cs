using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ReceptPanel : MonoBehaviour
{
    [Inject] PlayerResources _playerResources;
    [SerializeField] private GameObject[] _receptCells;
    [SerializeField] private Image[] _receptCellResourceIcon;
    [SerializeField] private TextMeshProUGUI[] _receptCellResourceText;
    public void UpdateReceptView(ResourceRecept[] resourceRecept)
    {
        if (resourceRecept.Length == 0)
        {
            return;
        }

        ResetCells();

        for (int i = 0; i < resourceRecept.Length; i++)
        {
            _receptCells[i].SetActive(true);
            _receptCellResourceIcon[i].sprite = resourceRecept[i].ResourceForRecept.Icon;

            if (_playerResources.ResourceEnough(resourceRecept[i].ResourceForRecept.ResourceEnum, resourceRecept[i].ResourcesForReceptAmount))
            {
                _receptCellResourceText[i].text = $"{resourceRecept[i].ResourcesForReceptAmount}";

            }
            else
            {
                _receptCellResourceText[i].text = $"<color={Colors.HexColorWarningYellow}>{resourceRecept[i].ResourcesForReceptAmount}</color>";
            }
        }
    }

    private void ResetCells()
    {
        foreach (var item in _receptCells)
        {
            item.SetActive(false);
        }
    }
}
