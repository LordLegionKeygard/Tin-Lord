using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    [SerializeField] private int _enemyNumber;
    public int GetEnemyNumber() => _enemyNumber;

    public void SetEnemyInfo(int enemyNumber)
    {
        _enemyNumber = enemyNumber;
    }
}
