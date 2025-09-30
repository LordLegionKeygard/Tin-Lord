using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    [SerializeField] private EnemyEnum _enemyEnum;
    private int _healthFactor;
    private int _damageFactor;
    private int _enemyNumber;
    private bool _isMiniBoss;
    public EnemyEnum GetEnemyEnum() => _enemyEnum;
    public int GetEnemyNumber() => _enemyNumber;
    public int GetHealthFactor() => _healthFactor;
    public int GetDamageFactor() => _damageFactor;
    public bool IsMiniBoss() => _isMiniBoss;

    public void SetEnemyInfo(int enemyNumber, int healthFactor, int damageFactor, bool isMiniBoss)
    {
        _enemyNumber = enemyNumber;
        _healthFactor = healthFactor;
        _damageFactor = damageFactor;
        _isMiniBoss = isMiniBoss;
    }
}
