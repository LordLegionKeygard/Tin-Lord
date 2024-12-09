public interface ITurretAttack
{
    void StartAttack();
    void StopAttack();
    bool IsActive { get; }
}
