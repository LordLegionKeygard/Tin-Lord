using UnityEngine;

public class EnemyStateChanger : BaseAiStateChanger
{
    [Header("State")]
    [SerializeField] private EnemyState _currentState;

    public override void HandleStateMachine()
    {
        if (_currentState != null)
        {
            EnemyState nextState = _currentState.Tick(this, CreatureHealth, CreatureAnimator, AiDestinationSetter, base.CreatureHealth, CreatureAttacks);

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
