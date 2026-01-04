using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HangarRobotItem : MonoBehaviour
{
    [SerializeField] private HangarSystem _hangarSystem;
    [SerializeField] private HangarRobotInfo _hangarRobotInfo;
    [SerializeField] private CityRobotInfo _cityRobotInfo;
    private bool _isOpen;
    private bool _isSelect;
    public bool IsOpen() => _isOpen;
    public bool IsSelect() => _isSelect;
    public HangarRobotInfo GetHangarRobotInfo() => _hangarRobotInfo;
    public CityRobotInfo GetCityRobotInfo() => _cityRobotInfo;

    [Header("View")]
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private Image _icon;
    [SerializeField] private GameObject _price;
    private Image _backImage;

    private void Awake()
    {
        _backImage = GetComponent<Image>();
    }

    public void UpdateView()
    {
        _price.SetActive(!_isOpen);
        _icon.enabled = _isOpen;
        _nameText.text = _isOpen ? Language.TextStatic[_hangarRobotInfo.Name] : "?";
        _icon.sprite = _hangarRobotInfo.RobotSprite;
        _priceText.text = _hangarRobotInfo.Price.ToString();

        _nameText.color = _isOpen ? _isSelect ? Colors.LightGreen : Colors.GreyEight : Colors.GreyFive;
        _icon.color = _isSelect ? Color.white : Colors.GreyEight;
        _backImage.color = _isSelect ? Color.white : Colors.GreyEight;
        _priceText.color = _hangarSystem.EnoughtShards(_hangarRobotInfo.Price) ? Color.white : Colors.WarningYellow;
    }

    public void SelectButton()
    {
        if (_isSelect) return;

        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        SelectToggleState(true);
        _hangarSystem.SelectRobot(_hangarRobotInfo.HangarRobotType, _isOpen);
    }

    public void SelectToggleState(bool state)
    {
        _isSelect = state;
        UpdateView();
    }

    public void SetIsOpen(bool state)
    {
        _isOpen = state;
        UpdateView();
    }
}
