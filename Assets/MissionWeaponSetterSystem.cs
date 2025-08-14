using UnityEngine;

public class MissionWeaponSetterSystem : MonoBehaviour
{
    [SerializeField] private ShipWeaponAimer _leftShipWeaponAimer;
    [SerializeField] private ShipWeaponAimer _rightShipWeaponAimer;
    [SerializeField] private WeaponSetter[] _weaponSetter;

    public void LoadWeapons(WeaponData weaponData)
    {
        var leftWeaponSetter = _weaponSetter[weaponData.LeftWeapon];
        leftWeaponSetter.WeaponModel.gameObject.SetActive(true);
        _leftShipWeaponAimer.SetupWeapon(leftWeaponSetter);

        var rightWeaponSetter = _weaponSetter[weaponData.RightWeapon];
        rightWeaponSetter.WeaponModel.gameObject.SetActive(true);
        _rightShipWeaponAimer.SetupWeapon(rightWeaponSetter);
    }
}

[System.Serializable]
public class WeaponSetter
{
    public ShipWeaponEnum ShipWeaponEnum;
    public Transform WeaponModel;
    public Transform FirePoint;
    public ParticleSystem Muzzle;
}
