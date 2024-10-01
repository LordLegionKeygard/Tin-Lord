using UnityEngine;

public class BaseDamage : MonoBehaviour
{
    [SerializeField] private KnockBackType _knockBackType;
    private BaseLevel _baseLevel;
    private BaseHealth _currentTargetBaseHealth;
    private BaseAttackVFX _baseAttackVFX;
    private BaseAttacks _baseAttacks;
    private float _damage;

    private void Awake()
    {
        _baseLevel = GetComponent<BaseLevel>();
        _baseAttackVFX = GetComponent<BaseAttackVFX>();
        _baseAttacks = GetComponent<BaseAttacks>();
    }

    private void Start()
    {
        SetDamage();
    }

    private void SetDamage()
    {
        _damage = _baseLevel.GetAiLevelInformation().PhysAttack[_baseLevel.GetLevel()];
    }

    public void SetTargetHealth(BaseHealth baseHealth)
    {
        _currentTargetBaseHealth = baseHealth;
    }

    public void Attack(int attackNumber)
    {
        if (_baseAttacks.AttackOneByOne())
        {
            _baseAttacks.ChangeAttackIndex();
        }
        if (_baseAttackVFX != null) _baseAttackVFX.PlayeVFX(attackNumber);
        if (_currentTargetBaseHealth == null) return;
        _currentTargetBaseHealth.CalculateDamage(_damage, _knockBackType);
    }
}
