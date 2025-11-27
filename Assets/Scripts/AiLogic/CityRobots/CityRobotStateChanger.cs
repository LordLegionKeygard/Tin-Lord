using UnityEngine;

public class CityRobotStateChanger : BaseAiStateChanger
{
    [SerializeField] private CityRobotState _currentState;
    [SerializeField] private LayerMask _detectionLayer;
    private CityRobotInformation _cityRobotInformation;
    public float GetAttackRadius() => _cityRobotInformation.GetCityRobotInfo().AttackRadius;
    public float GetRotationSpeed() => _cityRobotInformation.GetCityRobotInfo().RotationSpeed;
    public float GetAttackSpeed() => _cityRobotInformation.GetCityRobotInfo().AttackSpeed;
    public LayerMask GetDetectionLayer() => _detectionLayer;

    public override void Awake()
    {
        base.Awake();
        _cityRobotInformation = GetComponent<CityRobotInformation>();
    }

    public override void Update()
    {
        HandleAttackRecoveryTime();

        if (IsCanRotate() && AiDestinationSetter.CurrentTarget != null)
        {
            var t = AiDestinationSetter.CurrentTarget.transform.position;

            var targetDirection = new Vector3(t.x, transform.position.y, t.z) - transform.position;

            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, 3 * Time.deltaTime * _cityRobotInformation.GetCityRobotInfo().RotationSpeed, 0);

            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }

    public override void HandleStateMachine()
    {
        if (_currentState != null)
        {
            CityRobotState nextState = _currentState.Tick(this, BaseAnimator, AiDestinationSetter);

            if (nextState != null)
            {
                SwitchToNextState(nextState);
            }
        }
    }

    private void SwitchToNextState(CityRobotState state)
    {
        _currentState = state;
    }
}

