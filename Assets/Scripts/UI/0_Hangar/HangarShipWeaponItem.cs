using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HangarShipWeaponItem : MonoBehaviour
{
    [SerializeField] private HangarSystem _hangarSystem;
    [SerializeField] private ShipWeaponInfo _shipWeaponInfo;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private Image _icon;
    [SerializeField] private Image _selectView;
    private Image _backImage;
    private int _selectShipWeaponIndex = -1;
    private bool _isOpen;
    private bool _isSelect;
    public bool IsOpen() => _isOpen;
    public bool IsSelect() => _isSelect;
    public ShipWeaponInfo GetInfo() => _shipWeaponInfo;

    private void Awake()
    {
        _backImage = GetComponent<Image>();
    }

    public void UpdateView()
    {
        _selectView.enabled = _isSelect;
        _priceText.gameObject.SetActive(!_isOpen);
        _icon.enabled = _isOpen;
        _icon.sprite = _shipWeaponInfo.Icon;
        _priceText.text = _shipWeaponInfo.ShardPrice.ToString();
        _selectView.color = _isOpen && _isSelect ? Colors.LightGreen : Colors.GreyFive;

        _icon.color = _isSelect ? Color.white : Colors.GreyEight;
        _backImage.color = _isSelect ? Color.white : Colors.GreyEight;
        _priceText.color = _hangarSystem.EnoughtShards(_shipWeaponInfo.ShardPrice) ? Color.white : Colors.WarningYellow;
    }

    public void SelectButton()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);

        if (_isSelect)
        {
            _hangarSystem.UnselectShipWeapon(_selectShipWeaponIndex);
        }
        else
        {
            _hangarSystem.SelectShipWeapon(_shipWeaponInfo.ShipWeaponEnum, _isOpen, _shipWeaponInfo.IsLeft);
        }
    }

    public void SelectToggleState(bool state, int selectShipWeaponIndex)
    {
        _selectShipWeaponIndex = selectShipWeaponIndex;
        _isSelect = state;
        UpdateView();
    }

    public void SetIsOpen(bool state)
    {
        _isOpen = state;
        UpdateView();
    }
}
