using UnityEngine;

public class MissionShipWeaponSystem : MonoBehaviour
{
    [SerializeField] private ShipWeaponsPanel _shipWeaponsPanel;
    [SerializeField] private ShipWeaponAimer _leftShipWeaponAimer;
    [SerializeField] private ShipWeaponAimer _rightShipWeaponAimer;
    [SerializeField] private WeaponSetter[] _weaponSetter;
    private int _leftWeaponBulletsCount;
    private int _rightWeaponBulletsCount;
    private ShipWeaponInfo _leftShipWeaponInfo;
    private ShipWeaponInfo _rightShipWeaponInfo;
    private int _leftWeaponLevel;
    private int _rightWeaponLevel;

    public int GetCurrentLeftShipWeaponBulletsCount() => _leftWeaponBulletsCount;
    public int GetCurrentRightShipWeaponBulletsCount() => _rightWeaponBulletsCount;
    public bool IsHaveShipCannonBulletsCount(bool isLeft) => isLeft ? _leftWeaponBulletsCount > 0 : _rightWeaponBulletsCount > 0;
    public ShipWeaponInfo GetShipCannonInfo(bool isLeft) => isLeft ? _leftShipWeaponInfo : _rightShipWeaponInfo;
    public float GetWeaponDamage(bool isLeft) => isLeft ? _leftShipWeaponInfo.Damage + _leftWeaponLevel * _leftShipWeaponInfo.DamageFactor : _rightShipWeaponInfo.Damage + _rightWeaponLevel * _rightShipWeaponInfo.DamageFactor;

    public void LoadWeapons(WeaponData weaponData, MissionShipWeaponsData shipCannonsData, bool isStartMission)
    {
        var leftWeaponSetter = _weaponSetter[weaponData.LeftWeapon];

        _leftShipWeaponInfo = leftWeaponSetter.ShipWeaponInfo;
        _leftWeaponLevel = weaponData.LeftWeaponLevel;
        leftWeaponSetter.WeaponModel.gameObject.SetActive(true);
        _leftShipWeaponAimer.SetupWeapon(leftWeaponSetter);

        var rightWeaponSetter = _weaponSetter[weaponData.RightWeapon];

        _rightShipWeaponInfo = rightWeaponSetter.ShipWeaponInfo;
        _rightWeaponLevel = weaponData.RightWeaponLevel;
        rightWeaponSetter.WeaponModel.gameObject.SetActive(true);
        _rightShipWeaponAimer.SetupWeapon(rightWeaponSetter);

        if (isStartMission)
        {
            _leftWeaponBulletsCount = _leftShipWeaponInfo.BulletsCount;
            _rightWeaponBulletsCount = _rightShipWeaponInfo.BulletsCount;
        }
        else
        {
            _leftWeaponBulletsCount = shipCannonsData.LeftWeaponBulletsCount;
            _rightWeaponBulletsCount = shipCannonsData.RightWeaponBulletsCount;
        }
    }

    public void UseBullet(bool isLeft)
    {
        if (isLeft)
        {
            _leftWeaponBulletsCount--;
        }
        else
        {
            _rightWeaponBulletsCount--;
        }
        _shipWeaponsPanel.UpdateSliders();
    }
}

[System.Serializable]
public class WeaponSetter
{
    public ShipWeaponEnum ShipWeaponEnum;
    public ShipWeaponInfo ShipWeaponInfo;
    public Transform WeaponModel;
    public Transform FirePoint;
    public ParticleSystem Muzzle;
}
