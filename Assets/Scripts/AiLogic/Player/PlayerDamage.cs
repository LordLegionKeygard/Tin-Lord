using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : BaseDamage
{
    private PlayerLevel _playerLevel;

    public override void Awake()
    {
        base.Awake();
        _playerLevel = GetComponent<PlayerLevel>();
    }

    public override void SetDamage()
    {
        Damage = _playerLevel.GetPlayerLevelInformation().PhysAttack[_playerLevel.GetLevel()];
    }
}
