using UnityEngine;

public class MinigunAttack : MonoBehaviour, ITurretAttack
{
    [SerializeField] private TurretGunRotation _turretGunRotation;
    [SerializeField] private TurretMinigunAttack _turretMinigun;

    public bool IsActive { get; private set; }

    public void StartAttack()
    {
        IsActive = true;
        _turretMinigun.AttackToggle(true);
        _turretGunRotation.SetRotateToggle(true);
    }

    public void StopAttack()
    {
        IsActive = false;
        _turretMinigun.AttackToggle(false);
        _turretGunRotation.SetRotateToggle(false);
    }
}
