using UnityEngine;

public class TurretStateChanger : BaseAiStateChanger
{
    [SerializeField] private TurretState _currentState;
    private TurretBuilding _turretBuilding;

    [Header("Detection")]
    [SerializeField] private LayerMask _detectionLayer;
    public float AttackRadius() => _turretBuilding.Building().AttackRadius;
    public LayerMask DetectionLayer() => _detectionLayer;

    public override void Awake()
    {
        base.Awake();
        _turretBuilding = GetComponent<TurretBuilding>();
    }

    public override void Update()
    {       
        HandleAttackRecoveryTime();

        if (IsCanRotate() && AiDestinationSetter.CurrentTarget != null)
        {
            var t = AiDestinationSetter.CurrentTarget.transform.position;

            var targetDirection = new Vector3(t.x, transform.position.y, t.z) - transform.position;

            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, 3 * Time.deltaTime * _turretBuilding.Building().RotationSpeed, 0);

            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }

    public override void HandleStateMachine()
    {
        if (_currentState != null)
        {
            TurretState nextState = _currentState.Tick(this, BaseAnimator, AiDestinationSetter);

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
