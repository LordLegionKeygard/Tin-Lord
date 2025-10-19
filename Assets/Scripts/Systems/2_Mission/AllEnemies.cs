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
    Desert_4_Serpmare = 3,
    Desert_5_Funglicane = 4,
    Desert_Boss_Kupolobrach = 5,

    Winter_1_Entomochelon = 6,
    Winter_2_Onyscidus = 7,
    Winter_3_Karcicodus = 8,
    Winter_4_Fulgurodonte = 9,
    Winter_5_Pistripod = 10,
    Winter_Boss_Trunckarce = 11,

    Robot_1_Orb_01 = 12,
    Robot_2_AdvancedCombatDroid = 13,
    Robot_3_Quadroid = 14,
    Robot_4_DroidBipedSentinel = 15,
    Robot_5_Etasphera10 = 16,
    Robot_Boss_ScoutDroid = 17,
}
