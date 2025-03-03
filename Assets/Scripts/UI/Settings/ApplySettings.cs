using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ApplySettings : MonoBehaviour
{
    [Inject] private WorldSaveSettings _worldSaveSettings;
    [SerializeField] private Button _applyBtn;
    [SerializeField] private TextMeshProUGUI _buttonText;
    private AudioPanel _audioPanel;
    private VideoPanel _videoPanel;
    private GamePlayPanel _gameplayPanel;

    private void Awake()
    {
        _audioPanel = GetComponent<AudioPanel>();
        _videoPanel = GetComponent<VideoPanel>();
        _gameplayPanel = GetComponent<GamePlayPanel>();
    }

    public void ApplyToggle(bool state)
    {
        if(!_applyBtn.gameObject.activeInHierarchy) return;

        _applyBtn.interactable = state;
        _buttonText.color = state ? Color.white : Colors.GreyFive;
    }

    public void Apply()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        ApplyToggle(false);
        _worldSaveSettings.SaveSettingsData();
        _audioPanel.SetGameSettings();
        _videoPanel.SetGameSettings();
        _gameplayPanel.SetGameSettings();
    }
}
