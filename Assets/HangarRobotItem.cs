using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HangarRobotItem : MonoBehaviour
{
    [SerializeField] private HangarSystem _hangarSystem;
    [SerializeField] private HangarRobotInformation _hangarRobotInformation;
    private bool _isSelect;
    private bool _isOpen = true;
    public bool IsSelect() => _isSelect;

    [Header("View")]
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _button;
    [SerializeField] private Image _backImage;

    private void Start()
    {
        UpdateView();
    }

    public void UpdateView()
    {
        _nameText.text = Language.TextStatic[_hangarRobotInformation.Name];
        _icon.sprite = _hangarRobotInformation.RobotSprite;
        _priceText.text = _hangarRobotInformation.Price.ToString();
    }

    public void SetButtonAndTextColor()
    {
        _button.enabled = _isOpen;
        _nameText.color = _isOpen ? _isSelect ? Colors.LightGreen : Colors.GreyEight : Colors.GreyFive;
        _icon.color = _isOpen ? _isSelect ? Color.white : Colors.GreyEight : Colors.GreyFive;
        _backImage.color = _isSelect ? Color.white : Colors.GreyEight;
    }

    public void SelectView()
    {
        if (_isSelect) return;

        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        _hangarSystem.SelectRobot(_hangarRobotInformation.HangarRobotType);
        SelectToggleState(true);
    }

    public void SelectToggleState(bool state)
    {
        _isSelect = state;
        SetButtonAndTextColor();
    }
}
