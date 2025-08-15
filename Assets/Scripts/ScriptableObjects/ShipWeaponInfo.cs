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

    [Header("Damage")]
    public float Damage;
    public float DamageFactor;
    public int BulletsCount;
    public GameObject ExplosionPrefab;
    public float ImpactYOffset = 0f;    // смещение вверх точки взрыва
}

[System.Serializable]
public enum ShipWeaponEnum
{
    Left_SteelRiffle_0 = 0,
    Left_1 = 1,
    Left_2 = 2,
    Left_3 = 3,
    Left_4 = 4,

    Right_TitatiumRocketLauncher_0 = 5,
    Right_1 = 6,
    Right_2 = 7,
    Right_3 = 8,
    Right_4 = 9,
}
