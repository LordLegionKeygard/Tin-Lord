using UnityEngine;

[CreateAssetMenu(menuName = "TinLord/Info/ShipWeaponInfo")]
public class ShipWeaponInfo : ScriptableObject
{
    [Header("Main")]
    public int NameNumber;
    public int ShardPrice;
    public Sprite Icon;
    public ShipWeaponEnum ShipWeaponEnum;
    public BulletEnum BulletType;
    public bool IsLeft;

    [Header("Fire")]
    public float FireRate; // выстрелов в секунду
    public float BulletSpeed; // скорость снаряда
    public float SpreadDeg; // разброс по конусу
    public float LifeTime; // страховка жизни
    public int BulletsPerShot = 1; // кол-во пуль за выстрел

    [Header("Damage")]
    public float Damage;
    public float DamageFactor; // доп урон которое получает оружие, за каждое улучшение
    public int BulletsCount;
    public GameObject ExplosionPrefab;
    public float ImpactYOffset = 0f;    // смещение вверх точки взрыва
}

[System.Serializable]
public enum ShipWeaponEnum
{
    Left_SteelRiffle_0 = 0,
    Left_ScatterShotgun_1 = 1,
    Left_2 = 2, // minigun
    Left_3 = 3, // vortex cannon
    Left_4 = 4, // plasmagun

    Right_TitatiumRocketLauncher_0 = 5,
    Right_LongshotRailgun_6 = 6,
    Right_2 = 7, // flamethrower
    Right_3 = 8, // lasergun
    Right_4 = 9, // singularity launcher
}
