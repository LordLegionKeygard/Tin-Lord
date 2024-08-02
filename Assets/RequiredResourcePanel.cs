using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RequiredResourcePanel : MonoBehaviour
{
    [SerializeField] private GameObject[] _select;
    [SerializeField] private Button[] _buttons;
    public void UpdateButtonsView(ResourceEnum resourceEnum)
    {
        _select[0].SetActive(false);
        _select[1].SetActive(false);
        _select[2].SetActive(false);

        _buttons[0].interactable = false;
        _buttons[1].interactable = false;
        _buttons[2].interactable = false;

        switch (resourceEnum)
        {
            case ResourceEnum.Wood:
                _select[0].SetActive(true);
                _buttons[1].interactable = true;
                _buttons[2].interactable = true;
                break;
            case ResourceEnum.Coal:
                _select[1].SetActive(true);
                _buttons[0].interactable = true;
                _buttons[2].interactable = true;

                break;
            case ResourceEnum.Electricity:
                _select[2].SetActive(true);
                _buttons[0].interactable = true;
                _buttons[1].interactable = true;
                break;
        }
    }
}
