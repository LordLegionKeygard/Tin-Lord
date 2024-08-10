using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RequiredResourcePanel : MonoBehaviour
{
    [SerializeField] private GameObject[] _select;
    [SerializeField] private Button[] _buttons;

    public void UpdateButtonsView(ResourceEnum resourceEnum, ResourcesForWorkWrapper[] resourcesForWork)
    {
        ResetButtons();

        for (int i = 0; i < resourcesForWork.Length; i++)
        {
            switch (resourcesForWork[i].ResourceForWork.ResourceEnum)
            {
                case ResourceEnum.Wood:
                    _buttons[0].gameObject.SetActive(true);
                    break;
                case ResourceEnum.Coal:
                    _buttons[1].gameObject.SetActive(true);
                    break;
                case ResourceEnum.Oil:
                    _buttons[2].gameObject.SetActive(true);
                    break;
                case ResourceEnum.Electricity:
                    _buttons[3].gameObject.SetActive(true);
                    break;
            }
        }

        switch (resourceEnum)
        {
            case ResourceEnum.Wood:
                _select[0].SetActive(true);
                _buttons[0].interactable = false;
                break;
            case ResourceEnum.Coal:
                _select[1].SetActive(true);
                _buttons[1].interactable = false;
                break;
            case ResourceEnum.Oil:
                _select[2].SetActive(true);
                _buttons[2].interactable = false;
                break;
            case ResourceEnum.Electricity:
                _select[3].SetActive(true);
                _buttons[3].interactable = false;
                break;
        }
    }

    private void ResetButtons()
    {
        foreach (var button in _buttons)
        {
            button.gameObject.SetActive(false);
            button.interactable = true;
        }

        foreach (var select in _select)
        {
            select.SetActive(false);
        }
    }
}
