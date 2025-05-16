using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionItem : MonoBehaviour
{
    [Header("View")]
    [SerializeField] private TextMeshProUGUI _missionNameText;
    [SerializeField] private Image _missionIcon;
    [SerializeField] private Image _backImage;
    [SerializeField] private Button _button;

    [Header("Other")]
    [SerializeField] private Mission _mission;
    [SerializeField] private MissionPanel _missionPanel;
    private bool _isSelect;
    private bool _missionOpened;
    public bool IsSelect() => _isSelect;


    public void SetMissionOpened(int lastOpenedMissionId)
    {
        _missionOpened = WorldGameInfo.IsDemo ?
        _mission.MissionId <= lastOpenedMissionId && _mission.MissionId <= WorldGameInfo.LastAvailableDemoMissionId :
        _mission.MissionId <= lastOpenedMissionId;
        SetMissionView();
    }

    private void SetMissionView()
    {
        _button.interactable = _missionOpened;
        _missionIcon.enabled = _missionOpened;
        _missionIcon.sprite = _mission.MissionSprite;

        bool isLockedDemoMission = WorldGameInfo.IsDemo && _mission.MissionId > WorldGameInfo.LastAvailableDemoMissionId;
        
        _missionNameText.text = isLockedDemoMission ? Language.TextStatic[236] : _missionOpened ? _mission.Name[Language.LanguageNumber] : "?";

    }

    public void SelectMissionItem()
    {
        if (_isSelect) return;

        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.SelectMission, transform.position);
        _missionPanel.RefreshInfo(_mission);
        SelectToggleView(true);
    }

    private void RefreshView()
    {
        _missionNameText.color = _isSelect ? Color.white : Colors.GreyEight;
        _missionIcon.color = _isSelect ? Color.white : Colors.GreyEight;
        _backImage.color = _isSelect ? Color.white : Colors.GreyEight;
    }

    public void SelectToggleView(bool state)
    {
        _isSelect = state;
        RefreshView();
    }
}
