using UnityEngine;

public class BaseDamage : MonoBehaviour
{
    [SerializeField] private KnockBackType _knockBackType;
    private BaseLevel _baseLevel;
    private BaseHealth _currentTargetBaseHealth;
    private float _damage;

    private void Awake()
    {
        _baseLevel = GetComponent<BaseLevel>();
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

    public void Attack()
    {
        if (_currentTargetBaseHealth == null) return;

        _currentTargetBaseHealth.CalculateDamage(_damage, _knockBackType);
    }
}
