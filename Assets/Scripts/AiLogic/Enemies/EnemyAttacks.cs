using System.Linq;
using UnityEngine;

public class EnemyAttacks : MonoBehaviour
{
    [SerializeField] private EnemyAttackInfo[] _attacks;
    public float MaxAtkRange() => _maxAttackRange + _bonusDistance;
    private float _bonusDistance;
    public float GetBonusAttackDistance() => _bonusDistance;
    public EnemyAttackInfo[] GetCreatureAttacks() => _attacks;

    protected float _maxAttackRange;

    private void Awake()
    {
        CalculateMaxAttack();
    }

    private void CalculateMaxAttack()
    {
        if (_attacks.Length == 0) return;
        _maxAttackRange = _attacks.Max(attack => attack.MaximumDistanceNeededToAttack);
    }
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
