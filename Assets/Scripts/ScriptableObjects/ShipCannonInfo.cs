using UnityEngine;

[CreateAssetMenu(menuName = "TinLord/Ship/CannonInfo")]
public class ShipCannonInfo : ScriptableObject
{
    [Header("Main")]
    public int NameNumber;
    public BulletEnum BulletType;

    [Header("Fire")]
    public float FireRate; // выстрелов в секунду
    public float BulletSpeed; // скорость снаряда
    public float SpreadDeg; // разброс по конусу
    public float LifeTime; // страховка жизни

    [Header("Damage")]
    public int BulletsCount;
    public float ExplosionDamage;
    public GameObject ExplosionPrefab;
    public float ImpactYOffset = 0f;    // смещение вверх точки взрыва
}
