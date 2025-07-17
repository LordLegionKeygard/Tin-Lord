using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    [SerializeField] private EnemyEnum _enemyEnum;
    private int _healthFactor;
    private int _damageFactor;
    private int _enemyNumber;
    public EnemyEnum GetEnemyEnum() => _enemyEnum;
    public int GetEnemyNumber() => _enemyNumber;
    public int GetHealthFactor() => _healthFactor;
    public int GetDamageFactor() => _damageFactor;
    public bool IsMiniBoss() => _healthFactor != 1 || _damageFactor != 1;

    public void SetEnemyInfo(int enemyNumber, int healthFactor, int damageFactor)
    {
        _enemyNumber = enemyNumber;
        _healthFactor = healthFactor;
        _damageFactor = damageFactor;
    }
}
