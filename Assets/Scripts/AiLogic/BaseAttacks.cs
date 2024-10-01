using System.Linq;
using UnityEngine;

public class BaseAttacks : MonoBehaviour
{
    [SerializeField] private AttackInfo[] _attacks;
    [SerializeField] private bool _attackOneByOne;
    public bool AttackOneByOne() => _attackOneByOne;
    [SerializeField] private int _currentAttackIndex;
    public int CurrentAttackIndex() => _currentAttackIndex;
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

    public void ChangeAttackIndex()
    {     
        if(_currentAttackIndex >= _attacks.Length - 1)
        {
            _currentAttackIndex = 0;
        }
        else
        {
            _currentAttackIndex++;
        }
    }
}
