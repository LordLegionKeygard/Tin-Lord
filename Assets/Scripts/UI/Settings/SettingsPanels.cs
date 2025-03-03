using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using Zenject;

public class SettingsPanels : MonoBehaviour
{
    [Inject] private WorldSaveSettings _worldSaveSettings;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject[] _panels;
    [SerializeField] private Image[] _panelsBtnImages;
    [SerializeField] private TextMeshProUGUI[] _buttonsText;
    [SerializeField] private Sprite[] _sprites;
    private SaveLoadSettings _saveLoadSettings;
    private ApplySettings _applySettings;

    private void Awake()
    {
        _applySettings = GetComponent<ApplySettings>();
        _saveLoadSettings = GetComponent<SaveLoadSettings>();
    }

    public void ChangePanel(int number)
    {
        if (_panels[number].activeInHierarchy) return;
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);

        for (int i = 0; i < _panels.Length; i++)
        {
            _panels[i].SetActive(false);
            _panelsBtnImages[i].sprite = _sprites[1];
            _buttonsText[i].color = Colors.GreyFive;
        }
        _panels[number].SetActive(true);
        _panelsBtnImages[number].sprite = _sprites[0];
        _buttonsText[number].color = Color.white;
    }

    public void CloseButton()
    {
        _saveLoadSettings.SetAllSettingsFromData();
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        _applySettings.ApplyToggle(false);
        _settingsPanel.SetActive(false);
    }
}
