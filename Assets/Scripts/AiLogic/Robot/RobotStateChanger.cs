using UnityEngine;

public class RobotStateChanger : BaseAiStateChanger
{
    [SerializeField] private float _rotationSpeed = 1;
    [SerializeField] private RobotState _currentState;
    private BaseHealth _baseHealth;
    private RobotAttacks _robotAttacks;
    private RobotSpeed _robotSpeed;

    [Header("Detection")]
    private float _defaultDetectionRadius;
    private float _currentDetectionRadius;
    private float _extraAimDetectionRadius = 7;
    public float DetectionRadius() => _currentDetectionRadius;
    [SerializeField] private LayerMask _detectionLayer;
    [SerializeField] private LayerMask _buildingDetectionLayer;
    public LayerMask DetectionLayer() => _detectionLayer;
    public LayerMask BuildingDetectionLayer() => _buildingDetectionLayer;


    public override void Awake()
    {
        base.Awake();
        _robotAttacks = GetComponent<RobotAttacks>();
        _baseHealth = GetComponent<RobotHealth>();
        _robotSpeed = GetComponent<RobotSpeed>();
    }
    private void Start()
    {
        _defaultDetectionRadius = MachinesDataWorld.Instance.GetDetectionRadius();
        _currentDetectionRadius = _defaultDetectionRadius;
    }

    public override void Update()
    {
        if (_baseHealth.IsDeath()) return;

        HandleAttackRecoveryTime();

        if (IsCanRotate() && AiDestinationSetter.CurrentTarget != null)
        {
            var t = AiDestinationSetter.CurrentTarget.transform.position;

            var targetDirection = new Vector3(t.x, transform.position.y, t.z) - transform.position;

            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, 3 * Time.deltaTime * _rotationSpeed, 0);

            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }

    public override void HandleStateMachine()
    {
        if (_currentState != null)
        {
            RobotState nextState = _currentState.Tick(this, _baseHealth, BaseAnimator, AiDestinationSetter, _robotAttacks, _robotSpeed);

            if (nextState != null)
            {
                SwitchToNextState(nextState);
            }
        }
    }

    private void SwitchToNextState(RobotState state)
    {
        _currentState = state;
    }

    public void AimDetectionRadius(string state)
    {
        _currentDetectionRadius = state == "true" ? _defaultDetectionRadius + _extraAimDetectionRadius : _defaultDetectionRadius;
    }
}
