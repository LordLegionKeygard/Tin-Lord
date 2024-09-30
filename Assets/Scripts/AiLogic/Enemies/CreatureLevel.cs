using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureLevel : MonoBehaviour
{
    private int _level = 1;
    [SerializeField] private EnemiesInformation _enemyInformation;
    public int GetLevel() => _level;
    public EnemiesInformation GetEnemiesInformation() => _enemyInformation;

    public void SetLevel(int spawnerLevel)
    {
        _level = spawnerLevel;
    }

}
