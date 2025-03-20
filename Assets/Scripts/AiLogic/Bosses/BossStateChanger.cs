using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateChanger : EnemyStateChanger
{
    public override void Awake()
    {
        base.Awake();
        _enemyAttacks = GetComponent<EnemyAttacks>();
        _baseHealth = GetComponent<BossHealth>();
    }
}
