using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class WeaponsEngineerPanel : MonoBehaviour
{
    [Inject] private readonly SpaceSaveGame _spaceSaveGame;
    [SerializeField] private ShipWeaponInfo[] _shipWeaponsInfos;
    [SerializeField] private WeaponEngineerItem _leftWeaponEngineerItem;
    [SerializeField] private WeaponEngineerItem _rightWeaponEngineerItem;
    private ShipWeaponInfo _leftShipWeaponInfo;
    private ShipWeaponInfo _rightShipWeaponInfo;
    private int _leftWeaponLevel;
    private int _rightWeaponLevel;
    [SerializeField] private QuantsSystem _quantsSystem;
    [SerializeField] private TextMeshProUGUI _quantsText;
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private Image _buttonImage;
    [SerializeField] private Sprite[] _buttonSprites;
    private ShipWeaponInfo _currentWeaponInfo;
    public ShipWeaponInfo GetShipWeapon(bool isLeft) => isLeft ? _leftShipWeaponInfo : _rightShipWeaponInfo;
    public float GetWeaponDamage(bool isLeft) => isLeft ? _leftShipWeaponInfo.Damage + _leftWeaponLevel * _leftShipWeaponInfo.DamageFactor : _rightShipWeaponInfo.Damage + _rightWeaponLevel * _rightShipWeaponInfo.DamageFactor;
    public int GetLevel(bool isLeft) => isLeft ? _leftWeaponLevel : _rightWeaponLevel;

    [SerializeField] private TextMeshProUGUI _shipWeaponNameText;
    [SerializeField] private TextMeshProUGUI _shipWeaponDamageText;
    [SerializeField] private TextMeshProUGUI _shipWeaponAmmoText;
    [SerializeField] private TextMeshProUGUI _shipWeaponLevelText;

    public void LoadWeapons(WeaponData weaponData)
    {
        _leftShipWeaponInfo = _shipWeaponsInfos[weaponData.LeftWeapon];
        _rightShipWeaponInfo = _shipWeaponsInfos[weaponData.RightWeapon];

        _leftWeaponLevel = weaponData.LeftWeaponLevel;
        _rightWeaponLevel = weaponData.RightWeaponLevel;
    }

    public void ResetTraderPanel()
    {
        _currentWeaponInfo = null;
        _quantsText.text = "0";
        _buttonImage.sprite = _buttonSprites[1];
        _upgradeButton.interactable = false;
        _quantsText.color = Colors.GreySeven;
        _shipWeaponNameText.text = $"{Language.TextStatic[299]}: -";
        _shipWeaponDamageText.text = $"{Language.TextStatic[98]}:  -";
        _shipWeaponAmmoText.text = $"{Language.TextStatic[230]}: -";
        _shipWeaponLevelText.text = $"{Language.TextStatic[231]}: -";
        _leftWeaponEngineerItem.UpdateView();
        _rightWeaponEngineerItem.UpdateView();
        ResetToggleItems();
    }

    public void ResetToggleItems()
    {
        _leftWeaponEngineerItem.SelectToggle(false);
        _rightWeaponEngineerItem.SelectToggle(false);
    }

    public void SelectWeapon(bool isLeft)
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        ResetToggleItems();

        _currentWeaponInfo = GetShipWeapon(isLeft);

        UpdateView();
    }

    private void UpdateView()
    {
        var currentWeaponLevel = _currentWeaponInfo.IsLeft ? _leftWeaponLevel : _rightWeaponLevel;
        var price = WorldGameInfo.StartWeaponEnigneerUpgradePrice + currentWeaponLevel * WorldGameInfo.FactorWeaponEnigneerUpgradePrice;
        _quantsText.text = price.ToString();

        var enoughtQuants = _quantsSystem.GetQuants() >= price;
        _buttonImage.sprite = enoughtQuants ? _buttonSprites[0] : _buttonSprites[1];
        _upgradeButton.interactable = enoughtQuants;
        _quantsText.color = enoughtQuants ? Colors.GreySeven : Colors.WarningYellow;


        _shipWeaponNameText.text = $"{Language.TextStatic[299]}: {Language.TextStatic[_currentWeaponInfo.NameNumber]}";
        _shipWeaponDamageText.text = $"{Language.TextStatic[98]}: {GetWeaponDamage(_currentWeaponInfo.IsLeft)}";
        _shipWeaponAmmoText.text = $"{Language.TextStatic[230]}: {_currentWeaponInfo.BulletsCount}";
        _shipWeaponLevelText.text = $"{Language.TextStatic[231]}: {GetLevel(_currentWeaponInfo.IsLeft)}";
    }

    public void UpgradeWeapon()
    {
        var currentWeaponLevel = _currentWeaponInfo.IsLeft ? _leftWeaponLevel : _rightWeaponLevel;
        var price = WorldGameInfo.StartWeaponEnigneerUpgradePrice + currentWeaponLevel * WorldGameInfo.FactorWeaponEnigneerUpgradePrice;

        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Buy], transform.position);
        _quantsSystem.ChangeQuants(-price);

        if (_currentWeaponInfo.IsLeft) _leftWeaponLevel++;
        else _rightWeaponLevel++;

        _spaceSaveGame.SpaceSaveData.HangarCommandCenterData.WeaponData.LeftWeaponLevel = _leftWeaponLevel;
        _spaceSaveGame.SpaceSaveData.HangarCommandCenterData.WeaponData.RightWeaponLevel = _rightWeaponLevel;
        _spaceSaveGame.SaveDataToJson();
        UpdateView();
    }
}
