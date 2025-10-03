using System.Linq;
using UnityEngine;

public class EnemyAttacks : MonoBehaviour
{
    [SerializeField] private AttackInfo[] _attacks;
    [SerializeField] private float _reachDistanceOffset = -2;
    private float _defaultMeleeMaxAttackRange;
    private float _defaultMaxAttackRange;
    private float _maxMeleeAttackRange;
    private float _maxAttackRange;
    private float _tileDistance;
    public float GetTileDistance() => _tileDistance;
    public float MaxAttackRange() => _maxAttackRange;
    public float MaxMeleeAtkRange() => _maxMeleeAttackRange + _reachDistanceOffset; //нужно только чтобы задать _aiPath.endReachedDistance
    public AttackInfo[] GetCreatureAttacks() => _attacks;

    private void Awake()
    {
        CalculateDefaultMaxAttack();
    }

    private void CalculateDefaultMaxAttack()
    {
        if (_attacks == null || _attacks.Length == 0) return;

        var melee = _attacks.Where(a => a.AttackType == AttackType.Melee);
        var ranged = _attacks.Where(a => a.AttackType == AttackType.Range);

        _defaultMeleeMaxAttackRange = melee.Any() ? melee.Max(a => a.MaximumDistanceNeededToAttack) : ranged.Max(a => a.MaximumDistanceNeededToAttack);

        _defaultMaxAttackRange = _attacks.Max(a => a.MaximumDistanceNeededToAttack);
    }




    public void UpdateCreatureAttackDistance(Tile tile)
    {
        _tileDistance = tile != null ? tile.IsFourTile ? WorldGameInfo.EnemyReachedFourTileDistance : WorldGameInfo.EnemyReachedTileDistance : WorldGameInfo.EnemyReachedMachineDistance;
        _maxMeleeAttackRange = _tileDistance + _defaultMeleeMaxAttackRange;
        _maxAttackRange = _tileDistance + _defaultMaxAttackRange;
    }
}
