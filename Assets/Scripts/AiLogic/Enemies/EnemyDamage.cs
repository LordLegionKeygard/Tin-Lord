using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : BaseDamage
{
    private EnemyLevel _enemyLevel;

    public override void Awake()
    {
        base.Awake();
        _enemyLevel = GetComponent<EnemyLevel>();
    }

    public override void SetDamage()
    {
        Damage = _enemyLevel.GetAiLevelInformation().PhysAttack[_enemyLevel.GetLevel()];
    }
}
