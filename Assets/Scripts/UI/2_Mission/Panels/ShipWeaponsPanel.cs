using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ShipWeaponsPanel : MonoBehaviour
{
    [Inject] private readonly MissionModeSystem _missionModeSystem;
    [SerializeField] private PanelDoMoveY _panelDoMoveY;
    [SerializeField] private UIPanelsMission _uiPanelsMission;

    [Header("Left")]
    [SerializeField] private ShipWeaponInfo _leftShipWeaponInfo;
    [SerializeField] private ShipWeaponAimer _leftShipWeaponAimer;
    [SerializeField] private TextMeshProUGUI _leftWeaponNameText;
    private int _leftCannonBulletsCount;

    [Header("Right")]
    [SerializeField] private ShipWeaponInfo _rightShipWeaponInfo;
    [SerializeField] private ShipWeaponAimer _rightShipWeaponAimer;
    [SerializeField] private TextMeshProUGUI _rightWeaponNameText;
    private int _rightCannonBulletsCount;


    public int IsHaveShipCannonBulletsCount() => _leftCannonBulletsCount;
    public int IsHaveRightShipCannonBulletsCount() => _rightCannonBulletsCount;
    public bool IsHaveShipCannonBulletsCount(bool isLeft) => isLeft ? _leftCannonBulletsCount > 0 : _rightCannonBulletsCount > 0;
    public ShipWeaponInfo GetShipCannonInfo(bool isLeft) => isLeft ? _leftShipWeaponInfo : _rightShipWeaponInfo;

    [Header("View")]
    [SerializeField] private Slider _leftAmmunitionSlider;
    [SerializeField] private Slider _rightAmmunitionSlider;
    [SerializeField] private Slider _leftCooldownSlider;
    [SerializeField] private Slider _rightCooldownSlider;

    public void LoadWeaponsBullet(ShipCannonsData shipCannonsData, bool isStartMission)
    {
        if (isStartMission)
        {
            _leftCannonBulletsCount = _leftShipWeaponInfo.BulletsCount;
            _rightCannonBulletsCount = _rightShipWeaponInfo.BulletsCount;
        }
        else
        {
            _leftCannonBulletsCount = shipCannonsData.LeftShipCannonBulletsCount;
            _rightCannonBulletsCount = shipCannonsData.RightShipCannonBulletsCount;
        }

        SetupSliders();
        SetTexts();
    }

    private void Update()
    {
        if (_missionModeSystem.IsPlanetMode()) return;

        _leftCooldownSlider.value = 1f - _leftShipWeaponAimer.GetSliderCooldown();
        _rightCooldownSlider.value = 1f - _rightShipWeaponAimer.GetSliderCooldown();
    }

    private void SetTexts()
    {
        _leftWeaponNameText.text = Language.TextStatic[_leftShipWeaponInfo.NameNumber];
        _rightWeaponNameText.text = Language.TextStatic[_rightShipWeaponInfo.NameNumber];
    }

    private void SetupSliders()
    {
        _leftAmmunitionSlider.maxValue = _leftShipWeaponInfo.BulletsCount;
        _rightAmmunitionSlider.maxValue = _rightShipWeaponInfo.BulletsCount;
        UpdateSliders();
    }

    private void UpdateSliders()
    {
        _leftAmmunitionSlider.value = _leftCannonBulletsCount;
        _rightAmmunitionSlider.value = _rightCannonBulletsCount;
    }

    public void UseBullet(bool isLeft)
    {
        if (isLeft)
        {
            _leftCannonBulletsCount--;
        }
        else
        {
            _rightCannonBulletsCount--;
        }
        UpdateSliders();
    }

    public void SetupPanelsActive(bool isPlanetMode)
    {
        if (isPlanetMode)
        {
            _panelDoMoveY.PanelClose();
        }
        else
        {
            _panelDoMoveY.PanelMove(false);
            _uiPanelsMission.PreparePanelsToShipMode();
        }
    }

    public void PanelToggle()
    {
        _panelDoMoveY.PanelMove();
    }
}
