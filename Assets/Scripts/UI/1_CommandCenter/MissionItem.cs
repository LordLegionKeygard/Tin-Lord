using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionItem : MonoBehaviour
{
    [Header("View")]
    [SerializeField] private TextMeshProUGUI _missionNameText;
    [SerializeField] private Image _missionIcon;
    [SerializeField] private Image _backImage;
    [SerializeField] private GameObject _targetPosition;

    [Header("Other")]
    [SerializeField] private Mission _mission;
    [SerializeField] private MissionPanel _missionPanel;
    private bool _isSelect;
    public bool IsSelect() => _isSelect;

    private void Start()
    {
        SetMissionInfo();
    }

    private void SetMissionInfo()
    {
        _missionIcon.sprite = _mission.MissionSprite;
        _missionNameText.text = _mission.Name[Language.LanguageNumber];
    }

    public void SelectMissionItem()
    {
        _missionPanel.RefreshInfo(_mission);
        SelectToggleView(true);
    }

    private void RefreshView()
    {
        _missionNameText.color = _isSelect ? Color.white : Colors.GreyEight;
        _missionIcon.color = _isSelect ? Color.white : Colors.GreyEight;
        _backImage.color = _isSelect ? Color.white : Colors.GreyEight;
        _targetPosition.SetActive(_isSelect);
    }

    public void SelectToggleView(bool state)
    {
        _isSelect = state;
        RefreshView();
    }
}
