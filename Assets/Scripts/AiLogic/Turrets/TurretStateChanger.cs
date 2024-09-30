using UnityEngine;

public class TurretStateChanger : BaseAiStateChanger
{
    [SerializeField] private TurretState _currentState;
    private BaseAttacks _baseAttacks;

    public override void Awake()
    {
        base.Awake();
        _baseAttacks = GetComponent<BaseAttacks>();
    }

    public override void HandleStateMachine()
    {
        if (_currentState != null)
        {
            TurretState nextState = _currentState.Tick(this, BaseAnimator, AiDestinationSetter, _baseAttacks);

            if (nextState != null)
            {
                SwitchToNextState(nextState);
            }
        }
    }

    private void SwitchToNextState(TurretState state)
    {
        _currentState = state;
    }
}
