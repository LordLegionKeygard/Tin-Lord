using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ShipWeaponsPanel : MonoBehaviour
{
    [Inject] private readonly MissionModeSystem _missionModeSystem;
    [SerializeField] private MissionShipWeaponSystem _shipWeaponSystem;
    [SerializeField] private PanelDoMoveY _panelDoMoveY;
    [SerializeField] private UIPanelsMission _uiPanelsMission;

    [Header("Left")]
    [SerializeField] private ShipWeaponAimer _leftShipWeaponAimer;
    [SerializeField] private TextMeshProUGUI _leftWeaponNameText;

    [Header("Right")]
    [SerializeField] private ShipWeaponAimer _rightShipWeaponAimer;
    [SerializeField] private TextMeshProUGUI _rightWeaponNameText;

    [Header("View")]
    [SerializeField] private Slider _leftAmmunitionSlider;
    [SerializeField] private Slider _rightAmmunitionSlider;
    [SerializeField] private Slider _leftCooldownSlider;
    [SerializeField] private Slider _rightCooldownSlider;

    private void Awake()
    {
        CustomEvents.OnDataLoad += SetupSlidersAndText;
    }

    public void SetupSlidersAndText()
    {
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
        _leftWeaponNameText.text = Language.TextStatic[_shipWeaponSystem.GetShipCannonInfo(true).NameNumber];
        _rightWeaponNameText.text = Language.TextStatic[_shipWeaponSystem.GetShipCannonInfo(false).NameNumber];
    }

    private void SetupSliders()
    {
        _leftAmmunitionSlider.maxValue = _shipWeaponSystem.GetShipCannonInfo(true).BulletsCount;
        _rightAmmunitionSlider.maxValue = _shipWeaponSystem.GetShipCannonInfo(false).BulletsCount;
        UpdateSliders();
    }

    public void UpdateSliders()
    {
        _leftAmmunitionSlider.value = _shipWeaponSystem.GetCurrentLeftShipWeaponBulletsCount();
        _rightAmmunitionSlider.value = _shipWeaponSystem.GetCurrentRightShipWeaponBulletsCount();
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

    private void OnDestroy()
    {
        CustomEvents.OnDataLoad -= SetupSlidersAndText;
    }
}
