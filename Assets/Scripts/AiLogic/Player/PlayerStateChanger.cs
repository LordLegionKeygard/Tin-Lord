using UnityEngine;

public class PlayerStateChanger : BaseAiStateChanger
{
    [SerializeField] private float _rotationSpeed = 1;
    [SerializeField] private PlayerState _currentState;
    private BaseHealth _baseHealth;
    private PlayerAttacks _playerAttacks;
    private PlayerSpeed _playerSpeed;

    [Header("Detection")]
    private float _defaultDetectionRadius = 35;
    private float _currentDetectionRadius;
    private float _extraAimDetectionRadius = 7;
    public float DetectionRadius() => _currentDetectionRadius;
    [SerializeField] private LayerMask _detectionLayer;
    public LayerMask DetectionLayer() => _detectionLayer;

    public override void Awake()
    {
        base.Awake();
        _playerAttacks = GetComponent<PlayerAttacks>();
        _baseHealth = GetComponent<PlayerHealth>();
        _playerSpeed = GetComponent<PlayerSpeed>();
    }
    private void Start()
    {
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
            PlayerState nextState = _currentState.Tick(this, _baseHealth, BaseAnimator, AiDestinationSetter, _playerAttacks, _playerSpeed);

            if (nextState != null)
            {
                SwitchToNextState(nextState);
            }
        }
    }

    private void SwitchToNextState(PlayerState state)
    {
        _currentState = state;
    }

    public void AimDetectionRadius(string state)
    {
        _currentDetectionRadius = state == "true" ? _defaultDetectionRadius + _extraAimDetectionRadius : _defaultDetectionRadius;
    }
}
