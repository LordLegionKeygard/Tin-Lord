using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacks : BaseAttacks
{
    public override float MaxAtkRange() => _maxAttackRange + _bonusDistance;
    private float _bonusDistance;
    public float GetBonusAttackDistance() => _bonusDistance;
    public void UpdateCreatureAttackDistance(Tile tile)
    {
        if (tile != null)
        {
            _bonusDistance = tile.IsFourTile ? WorldGameInfo.FourTileDistance - WorldGameInfo.TileDistance : 0;
        }
        else
        {
            _bonusDistance = 0;
        }
    }
}
