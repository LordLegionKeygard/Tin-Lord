using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureLevel : MonoBehaviour
{
    private int _level = 1;
    public EnemiesInformation EnemyInformation;

    public void SetLevel(int spawnerLevel)
    {
        _level = spawnerLevel;
    }

    public virtual int Level()
    {
        return _level;
    }
}
