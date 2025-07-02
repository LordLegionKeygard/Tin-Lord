using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HangarDroneItem : MonoBehaviour
{
    [SerializeField] private HangarSystem _hangarSystem;
    [SerializeField] private HangarDroneInfo _hangarDroneInfo;
    private bool _isOpen;
    private bool _isSelect;
    public bool IsOpen() => _isOpen;
    public bool IsSelect() => _isSelect;
    public HangarDroneInfo GetInfo() => _hangarDroneInfo;

    [Header("View")]
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private Image _icon;
    [SerializeField] private GameObject _shardsIcon;
    [SerializeField] private GameObject _openedIcon;
    private Image _backImage;

    private void Awake()
    {
        _backImage = GetComponent<Image>();
    }

    public void UpdateView()
    {
        _shardsIcon.SetActive(!_isOpen);
        _priceText.gameObject.SetActive(!_isOpen);
        _openedIcon.gameObject.SetActive(_isOpen);
        _icon.enabled = _isOpen;
        _nameText.text = _isOpen ? Language.TextStatic[_hangarDroneInfo.Name] : "?";
        _icon.sprite = _hangarDroneInfo.DroneSprite;
        _priceText.text = _hangarDroneInfo.Price.ToString();

        _nameText.color = _isOpen ? _isSelect ? Colors.LightGreen : Colors.GreyEight : Colors.GreyFive;
        _icon.color = _isSelect ? Color.white : Colors.GreyEight;
        _backImage.color = _isSelect ? Color.white : Colors.GreyEight;
        _priceText.color = _hangarSystem.EnoughtShards(_hangarDroneInfo.Price) ? Color.white : Colors.WarningYellow;
    }

    public void SelectButton()
    {
        if (_isSelect) return;

        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        SelectToggleState(true);
        _hangarSystem.SelectDrone(_hangarDroneInfo.HangarDroneType, _isOpen);
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
