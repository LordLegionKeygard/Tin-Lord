using UnityEngine;

public class LaserAttack : MonoBehaviour, ITurretAttack
{
    [SerializeField] private TurretLaserAttack _turretLaser;

    public bool IsActive { get; private set; }

    public void StartAttack()
    {
        IsActive = true;
        _turretLaser.AttackToggle(true);
    }

    public void StopAttack()
    {
        IsActive = false;
        _turretLaser.AttackToggle(false);
    }
}
