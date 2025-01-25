using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductionResourcePanel : MonoBehaviour
{
    [SerializeField] private GameObject[] _select;
    [SerializeField] private Button[] _buttons;
    [SerializeField] private Image[] _images;
    [SerializeField] private Resource[] _productionResources;
    private SelectTilePanel _selectTilePanel;
    private Resource _lastResource;
    private Building _lastBuilding;

    private void Awake()
    {
        _selectTilePanel = GetComponent<SelectTilePanel>();
    }

    public void SetButtonView(Building building, Resource currentResource)
    {

        if (_lastResource != null && _lastResource == currentResource && _lastBuilding == building) return;

        _lastResource = currentResource;
        _lastBuilding = building;

        ResetButtons(true);

        _productionResources = new Resource[building.ResourcesProduction.Length];

        for (int i = 0; i < building.ResourcesProduction.Length; i++)
        {
            _productionResources[i] = building.ResourcesProduction[i].ProductionResource;
            _images[i].sprite = building.ResourcesProduction[i].ProductionResource.Icon;
            _buttons[i].gameObject.SetActive(true);

            if (currentResource == building.ResourcesProduction[i].ProductionResource)
            {
                _select[i].SetActive(true);
                _buttons[i].interactable = false;
            }
        }
    }

    private void ResetButtons(bool isNeedFalseObjects)
    {
        foreach (var button in _buttons)
        {
            if(isNeedFalseObjects) button.gameObject.SetActive(false);
            button.interactable = true;
        }

        foreach (var select in _select)
        {
            select.SetActive(false);
        }
    }

    public void ChangeResourceProductionButton(int number)
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        _selectTilePanel.ChangeResourceProduction(_productionResources[number], _lastBuilding.ResourcesProduction[number].ResourceRecept);
        ResetButtons(false);
        _select[number].SetActive(true);
        _buttons[number].interactable = false;
    }
}
