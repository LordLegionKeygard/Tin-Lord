using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShipCannonsPanel : MonoBehaviour
{
    [SerializeField] private PanelDoMoveY _panelDoMoveY;
    [SerializeField] private UIPanelsMission _uiPanelsMission;

    [Header("Left")]
    [SerializeField] private ShipCannonInfo _leftShipCannonInfo;
    [SerializeField] private ShipCannonAimer _leftShipCannonAimer;
    [SerializeField] private TextMeshProUGUI _leftCannonNameText;
    private int _leftCannonBulletsCount;

    [Header("Right")]
    [SerializeField] private ShipCannonInfo _rightShipCannonInfo;
    [SerializeField] private ShipCannonAimer _rightShipCannonAimer;
    [SerializeField] private TextMeshProUGUI _rightCannonNameText;
    private int _rightCannonBulletsCount;


    public int IsHaveShipCannonBulletsCount() => _leftCannonBulletsCount;
    public int IsHaveRightShipCannonBulletsCount() => _rightCannonBulletsCount;
    public bool IsHaveShipCannonBulletsCount(bool isLeft) => isLeft ? _leftCannonBulletsCount > 0 : _rightCannonBulletsCount > 0;
    public ShipCannonInfo GetShipCannonInfo(bool isLeft) => isLeft ? _leftShipCannonInfo : _rightShipCannonInfo;

    [Header("View")]
    [SerializeField] private Slider _leftAmmunitionSlider;
    [SerializeField] private Slider _rightAmmunitionSlider;

    public void LoadCannons(ShipCannonsData shipCannonsData, bool isStartMission)
    {
        if (isStartMission)
        {
            _leftCannonBulletsCount = _leftShipCannonInfo.BulletsCount;
            _rightCannonBulletsCount = _rightShipCannonInfo.BulletsCount;
        }
        else
        {
            _leftCannonBulletsCount = shipCannonsData.LeftShipCannonBulletsCount;
            _rightCannonBulletsCount = shipCannonsData.RightShipCannonBulletsCount;
        }

        SetupSliders();
        SetTexts();
    }

    private void SetTexts()
    {
        _leftCannonNameText.text = Language.TextStatic[_leftShipCannonInfo.NameNumber];
        _rightCannonNameText.text = Language.TextStatic[_rightShipCannonInfo.NameNumber];
    }

    private void SetupSliders()
    {
        _leftAmmunitionSlider.maxValue = _leftShipCannonInfo.BulletsCount;
        _rightAmmunitionSlider.maxValue = _rightShipCannonInfo.BulletsCount;
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
            _panelDoMoveY.PanelMove();
            _uiPanelsMission.PreparePanelsToShipMode();
        }
    }

    public void PanelToggle()
    {
        _panelDoMoveY.PanelMove();
    }
}
