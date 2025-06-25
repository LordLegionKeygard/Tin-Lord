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
    Desert_1_Arathrox = 0,
    Desert_2_Serparmat = 1,
    Desert_3_Ceratoferox = 2,
    Winter_1_Pistripod = 3,
    Winter_Boss_Trunckarce = 4,
    Winter_MiniBoss_Karcicodus = 5,
    Winter_2_Entomochelon = 6,
    Winter_3_Onyscidus = 7,
    Winter_4_Fulgurodonte = 8,
    Desert_Boss_Kupolobrach = 9,
}
