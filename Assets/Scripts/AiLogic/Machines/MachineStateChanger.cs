using UnityEngine;

public class MachineStateChanger : BaseAiStateChanger
{
    [SerializeField] private float _rotationSpeed = 0.1f;
    [SerializeField] private MachineState _currentState;
    private BaseHealth _baseHealth;
    private MachineAttacks _machineAttacks;
    private MachineSpeed _machineSpeed;

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
        _machineAttacks = GetComponent<MachineAttacks>();
        _baseHealth = GetComponent<MachineHealth>();
        _machineSpeed = GetComponent<MachineSpeed>();
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
            MachineState nextState = _currentState.Tick(this, _baseHealth, BaseAnimator, AiDestinationSetter, _machineAttacks, _machineSpeed);

            if (nextState != null)
            {
                SwitchToNextState(nextState);
            }
        }
    }

    private void SwitchToNextState(MachineState state)
    {
        _currentState = state;
    }

    public void AimDetectionRadius(string state)
    {
        _currentDetectionRadius = state == "true" ? _defaultDetectionRadius + _extraAimDetectionRadius : _defaultDetectionRadius;
    }
}
