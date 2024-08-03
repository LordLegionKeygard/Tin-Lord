using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RequiredResourcePanel : MonoBehaviour
{
    [SerializeField] private GameObject[] _select;
    [SerializeField] private Button[] _buttons;

    public void UpdateButtonsView(ResourceEnum resourceEnum, ResourceRequiredEnum resourceRequiredEnum)
    {
        if (resourceRequiredEnum == ResourceRequiredEnum.Fuel)
        {
            _buttons[0].gameObject.SetActive(true);
            _buttons[1].gameObject.SetActive(true);
            _buttons[2].gameObject.SetActive(false);
        }
        else if (resourceRequiredEnum == ResourceRequiredEnum.Electricity)
        {
            _buttons[0].gameObject.SetActive(false);
            _buttons[1].gameObject.SetActive(false);
            _buttons[2].gameObject.SetActive(true);
        }


        foreach (var select in _select)
        {
            select.SetActive(false);
        }

        foreach (var button in _buttons)
        {
            button.interactable = false;
        }

        switch (resourceEnum)
        {
            case ResourceEnum.Wood:
                SetActiveResource(0, 1, 2);
                break;
            case ResourceEnum.Coal:
                SetActiveResource(1, 0, 2);
                break;
            case ResourceEnum.Electricity:
                SetActiveResource(2, 0, 1);
                break;
        }
    }

    private void SetActiveResource(int selectIndex, int buttonIndex1, int buttonIndex2)
    {
        _select[selectIndex].SetActive(true);
        _buttons[buttonIndex1].interactable = true;
        _buttons[buttonIndex2].interactable = true;
    }
}
