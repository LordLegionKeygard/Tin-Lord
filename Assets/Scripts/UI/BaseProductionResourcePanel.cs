using UnityEngine;
using UnityEngine.UI;

public class BaseProductionResourcePanel : MonoBehaviour
{
    [SerializeField] private GameObject[] _select;
    [SerializeField] private Button[] _buttons;
    [SerializeField] private Image[] _images;
    protected Resource[] _productionResources;
    private Resource _lastResource;
    protected Building _lastBuilding;


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
            if (isNeedFalseObjects) button.gameObject.SetActive(false);
            button.interactable = true;
        }

        foreach (var select in _select)
        {
            select.SetActive(false);
        }
    }

    public virtual void ChangeResourceProductionButton(int number)
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        ResetButtons(false);
        _select[number].SetActive(true);
        _buttons[number].interactable = false;      
    }
}
