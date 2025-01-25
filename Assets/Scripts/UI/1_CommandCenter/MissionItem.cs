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
        _missionOpened = _mission.MissionId <= lastOpenedMissionId;
        SetMissionView();
    }

    private void SetMissionView()
    {
        _button.interactable = _missionOpened;
        _missionIcon.enabled = _missionOpened;
        _missionIcon.sprite = _mission.MissionSprite;
        _missionNameText.text = _missionOpened ? _mission.Name[Language.LanguageNumber] : "?";
    }

    public void SelectMissionItem()
    {
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
