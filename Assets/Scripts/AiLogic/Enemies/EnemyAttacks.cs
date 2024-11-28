using System.Linq;
using UnityEngine;

public class EnemyAttacks : MonoBehaviour
{
    [SerializeField] private AttackInfo[] _attacks;
    private float _defaultMaxAttackRange;
    private float _maxAttackRange;
    public float MaxAtkRange() => _maxAttackRange;
    public AttackInfo[] GetCreatureAttacks() => _attacks;

    private void Awake()
    {
        CalculateDefaultMaxAttack();
    }

    private void CalculateDefaultMaxAttack()
    {
        if (_attacks.Length == 0) return;
        _defaultMaxAttackRange = _attacks.Max(attack => attack.MaximumDistanceNeededToAttack);
        
    }
    public void UpdateCreatureAttackDistance(Tile tile)
    {
        if (tile != null)
        {
            _maxAttackRange = tile.IsFourTile ? WorldGameInfo.EnemyReachedFourTileDistance + _defaultMaxAttackRange : WorldGameInfo.EnemyReachedTileDistance + _defaultMaxAttackRange;
        }
        else
        {
            _maxAttackRange = WorldGameInfo.EnemyReachedRobotDistance + _defaultMaxAttackRange;
        }
    }
}
