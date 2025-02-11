using UnityEngine;

public class AllEnemies : MonoBehaviour
{
    [SerializeField] private GameObject[] _allEnemies;
    public GameObject GetEnemyForEnum(EnemyEnum enemiesEnum) => _allEnemies[(int)enemiesEnum];
    public GameObject GetEnemyForNumber(int number) => _allEnemies[number];
}

[System.Serializable]
public enum EnemyEnum
{
    None = -1,
    Arathrox = 0,
    Serparmat = 1,
    Ceratoferox = 2,
    Pistripod = 3,
    Trunckarce = 4,
    Karcicodus = 5,
}
