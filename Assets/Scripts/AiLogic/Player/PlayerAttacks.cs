using System.Linq;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    [SerializeField] private AttackInfo[] _attacks;
    public float MaxAtkRange() => _maxAttackRange;
    public AttackInfo[] GetCreatureAttacks() => _attacks;

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
}
