using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevel : BaseLevel
{
    [SerializeField] private int _level;
    public override int GetLevel() => _level;

    public void SetLevel(int spawnerLevel)
    {
        _level = spawnerLevel;
    }
}
