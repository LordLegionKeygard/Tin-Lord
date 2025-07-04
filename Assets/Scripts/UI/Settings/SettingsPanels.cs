using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SettingsPanels : MonoBehaviour
{
    [SerializeField] private EscapePanelMission _escapePanelMission;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject[] _panels;
    [SerializeField] private Image[] _panelsBtnImages;
    [SerializeField] private TextMeshProUGUI[] _buttonsText;
    [SerializeField] private Sprite[] _sprites;
    private SettingsSaveLoad _settingsSaveLoad;
    private ApplySettings _applySettings;

    private void Awake()
    {
        _applySettings = GetComponent<ApplySettings>();
        _settingsSaveLoad = GetComponent<SettingsSaveLoad>();
    }

    public void ChangePanel(int number)
    {
        if (_panels[number].activeInHierarchy) return;
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);

        for (int i = 0; i < _panels.Length; i++)
        {
            _panels[i].SetActive(false);
            _panelsBtnImages[i].sprite = _sprites[1];
            _buttonsText[i].color = Color.white;
        }
        _panels[number].SetActive(true);
        _panelsBtnImages[number].sprite = _sprites[0];
        _buttonsText[number].color = Color.black;
    }

    public void CloseButton()
    {
        _settingsSaveLoad.SetAllSettingsFromData();
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        _applySettings.ApplyToggle(false);
        _settingsPanel.SetActive(false);
        if (_escapePanelMission != null) _escapePanelMission.PanelViewToggle(false);
    }
}
