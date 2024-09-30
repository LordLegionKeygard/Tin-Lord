using UnityEngine;

public class EnemyStateChanger : BaseAiStateChanger
{
    [SerializeField] private EnemyState _currentState;
    private BaseHealth _baseHealth;
    private EnemyAttacks _enemyAttacks;

    public override void Awake()
    {
        base.Awake();
        _enemyAttacks = GetComponent<EnemyAttacks>();
        _baseHealth = GetComponent<EnemyHealth>();
    }

    public override void Update()
    {
        if (_baseHealth.IsDeath()) return;
        base.Update();
    }

    public override void HandleStateMachine()
    {
        if (_currentState != null)
        {
            EnemyState nextState = _currentState.Tick(this, _baseHealth, BaseAnimator, AiDestinationSetter, _enemyAttacks);

            if (nextState != null)
            {
                SwitchToNextState(nextState);
            }
        }
    }

    private void SwitchToNextState(EnemyState state)
    {
        _currentState = state;
    }
}
