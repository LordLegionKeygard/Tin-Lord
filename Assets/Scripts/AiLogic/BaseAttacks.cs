using System.Linq;
using UnityEngine;

public class BaseAttacks : MonoBehaviour
{
    [SerializeField] private AttackInfo[] _attacks;
    public AttackInfo[] GetCreatureAttacks() => _attacks;
    protected float _maxAttackRange;
    public virtual float MaxAtkRange() => _maxAttackRange;
    
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
