using System.Linq;
using UnityEngine;

public class CreatureAttacks : MonoBehaviour
{
    [SerializeField] private CreatureAttack[] _creatureAttacks;
    public CreatureAttack[] GetCreatureAttacks() => _creatureAttacks;
    private float _maxAttackRange;
    public float MaxAtkRange;
    private float _bonusDistance;
    public float GetBonusAttackDistance() => _bonusDistance;
    
    private void Awake()
    {
        CalculateMaxAttack();
    }

    private void CalculateMaxAttack()
    {
        if (_creatureAttacks.Length == 0) return;
        _maxAttackRange = _creatureAttacks.Max(attack => attack.MaximumDistanceNeededToAttack);
        MaxAtkRange = _maxAttackRange;
    }

    public void UpdateCreatureAttackDistance(Tile tile)
    {
        if (tile != null)
        {
            _bonusDistance = tile.IsFourTile ? WorldGameInfo.FourTileDistance - WorldGameInfo.TileDistance : 0;
            MaxAtkRange = _maxAttackRange + _bonusDistance;
        }
        else
        {
            _bonusDistance = 0;
            MaxAtkRange = _maxAttackRange + _bonusDistance;
        }
    }
}
