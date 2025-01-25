using UnityEngine;

public class LaserAttack : MonoBehaviour, ITurretAttack
{
    [SerializeField] private TurretLaserAttack _turretLaser;
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
        _turretLaser.AttackToggle(true);
    }

    public void StopAttack()
    {
        IsActive = false;
        _sound.SetActive(false);
        _turretLaser.AttackToggle(false);
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
