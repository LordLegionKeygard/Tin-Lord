using UnityEngine;

public class MinigunAttack : MonoBehaviour, ITurretAttack
{
    [SerializeField] private TurretGunRotation _turretGunRotation;
    [SerializeField] private TurretMinigunAttack _turretMinigun;
    [SerializeField] private GameObject _sound;
    public bool IsActive { get; private set; }

    private void Start()
    {
        CustomEvents.OnPauseChanged += UpdateSound;
    }

    public void StartAttack()
    {
        IsActive = true;
        _sound.SetActive(true);
        _turretMinigun.AttackToggle(true);
        _turretGunRotation.SetRotateToggle(true);
    }

    public void StopAttack()
    {
        IsActive = false;
        _sound.SetActive(false);
        _turretMinigun.AttackToggle(false);
        _turretGunRotation.SetRotateToggle(false);
    }

    private void UpdateSound(bool isPaused)
    {
        _sound.SetActive(!isPaused && IsActive);
    }

    private void OnDestroy()
    {
        CustomEvents.OnPauseChanged -= UpdateSound;
    }
}
