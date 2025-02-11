using System.Linq;
using UnityEngine;

public class EnemyAttacks : MonoBehaviour
{
    [SerializeField] private AttackInfo[] _attacks;
    [SerializeField] private float _defaultMeleeMaxAttackRange;
    [SerializeField] private float _defaultMaxAttackRange;
    [SerializeField] private float _maxMeleeAttackRange;
    [SerializeField] private float _maxAttackRange;
    private float _tileDistance;
    public float GetTileDistance() => _tileDistance;
    public float MaxAttackRange() => _maxAttackRange;
    public float MaxMeleeAtkRange() => _maxMeleeAttackRange; //нужно только чтобы задать _aiPath.endReachedDistance
    public AttackInfo[] GetCreatureAttacks() => _attacks;

    private void Awake()
    {
        CalculateDefaultMaxAttack();
    }

    private void CalculateDefaultMaxAttack()
    {
        if (_attacks.Length == 0) return;
        _defaultMeleeMaxAttackRange = _attacks
            .Where(attack => attack.AttackType == AttackType.Melee)
            .Max(attack => attack.MaximumDistanceNeededToAttack);

        _defaultMaxAttackRange = _attacks.Max(attack => attack.MaximumDistanceNeededToAttack);
    }



    public void UpdateCreatureAttackDistance(Tile tile)
    {
        _tileDistance = tile != null ? WorldGameInfo.EnemyReachedFourTileDistance : WorldGameInfo.EnemyReachedRobotDistance;
        _maxMeleeAttackRange = tile.IsFourTile ? _tileDistance + _defaultMeleeMaxAttackRange : _tileDistance + _defaultMeleeMaxAttackRange;
        _maxAttackRange = tile.IsFourTile ? _tileDistance + _defaultMaxAttackRange : _tileDistance + _defaultMaxAttackRange;
    }
}
