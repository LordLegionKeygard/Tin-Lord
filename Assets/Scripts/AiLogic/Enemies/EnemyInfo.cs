using Pathfinding;
using Pathfinding.RVO;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    [SerializeField] private EnemyEnum _enemyEnum;
    private int _healthFactor;
    private int _damageFactor;
    private int _enemyNumber;
    private bool _isMiniBoss;
    private RVOController _rVOController;
    private AlternativePath _alternativePath;
    public EnemyEnum GetEnemyEnum() => _enemyEnum;
    public int GetEnemyNumber() => _enemyNumber;
    public int GetHealthFactor() => _healthFactor;
    public int GetDamageFactor() => _damageFactor;
    public bool IsMiniBoss() => _isMiniBoss;

    private void Awake()
    {
        _rVOController = GetComponent<RVOController>();
        _alternativePath = GetComponent<AlternativePath>();
    }

    public void SetEnemyInfo(int enemyNumber, int healthFactor, int damageFactor, bool isMiniBoss)
    {
        _enemyNumber = enemyNumber;
        _healthFactor = healthFactor;
        _damageFactor = damageFactor;
        _isMiniBoss = isMiniBoss;

        if (_isMiniBoss) DisableAlternativePath();
    }

    private void DisableAlternativePath()
    {
        if (_rVOController != null) _rVOController.enabled = false;
        if (_alternativePath != null) _alternativePath.enabled = false;
    }
}
