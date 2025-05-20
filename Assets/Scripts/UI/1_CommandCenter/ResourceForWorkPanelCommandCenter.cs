using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceForWorkPanelCommandCenter : MonoBehaviour
{
    [SerializeField] private GameObject[] _select;
    [SerializeField] private Button[] _buttons;

    private Dictionary<ResourceEnum, (Button button, GameObject select)> _resourceMapping;

    private void Awake()
    {
        _resourceMapping = new Dictionary<ResourceEnum, (Button, GameObject)>
        {
            { ResourceEnum.Wood, (_buttons[0], _select[0]) },
            { ResourceEnum.Coal, (_buttons[1], _select[1]) },
            { ResourceEnum.Oil, (_buttons[2], _select[2]) },
            { ResourceEnum.Electricity, (_buttons[3], _select[3]) },
            { ResourceEnum.Steam, (_buttons[4], _select[4]) },
            { ResourceEnum.Water, (_buttons[5], _select[5]) },
            { ResourceEnum.Stone, (_buttons[6], _select[6]) },
        };
    }

    public void UpdateButtonsView(Building building, ResourceEnum currentResourceEnum)
    {
        ResetButtons();

        foreach (var resource in building.ResourcesForWork)
        {
            if (_resourceMapping.TryGetValue(resource.ResourceForWork.ResourceEnum, out var mapping))
            {
                mapping.button.gameObject.SetActive(true);
            }
        }

        if (_resourceMapping.TryGetValue(currentResourceEnum, out var selectedMapping))
        {
            selectedMapping.select.SetActive(true);
            selectedMapping.button.interactable = false;
        }
    }

    private void ResetButtons()
    {
        foreach (var mapping in _resourceMapping.Values)
        {
            mapping.button.gameObject.SetActive(false);
            mapping.button.interactable = true;
            mapping.select.SetActive(false);
        }
    }
}
