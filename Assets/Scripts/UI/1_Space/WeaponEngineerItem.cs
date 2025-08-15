using UnityEngine;
using UnityEngine.UI;

public class WeaponEngineerItem : MonoBehaviour
{
    [SerializeField] private WeaponsEngineerPanel _weaponsEngineerPanel;
    [SerializeField] private bool _isLeft;
    [SerializeField] private Image _icon;
    [SerializeField] private Image _selectView;
    private Image _backImage;
    private bool _isSelect;

    private void Awake()
    {
        _backImage = GetComponent<Image>();
    }

    public void Select()
    {
        if (_isSelect) return;

        _weaponsEngineerPanel.SelectWeapon(_isLeft);
        _isSelect = true;
        UpdateView();
    }

    public void SelectToggle(bool state)
    {
        _isSelect = state;
        UpdateView();
    }

    public void UpdateView()
    {
        _selectView.enabled = _isSelect;
        _icon.sprite = _weaponsEngineerPanel.GetShipWeapon(_isLeft).Icon;
        _selectView.color = _isSelect ? Colors.LightGreen : Colors.GreyFive;

        _icon.color = _isSelect ? Color.white : Colors.GreyEight;
        _backImage.color = _isSelect ? Color.white : Colors.GreyEight;
    }
}
