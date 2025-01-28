using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    [SerializeField] private EnemyEnum _enemyEnum;
    private int _enemyNumber;
    public int GetEnemyNumber() => _enemyNumber;
    public EnemyEnum GetEnemyEnum() => _enemyEnum;

    public void SetEnemyInfo(int enemyNumber)
    {
        _enemyNumber = enemyNumber;
    }
}
