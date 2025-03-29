using Pathfinding;
using UnityEngine;

public class EnemyStateChanger : BaseAiStateChanger
{
    [SerializeField] private float _rotationSpeed = 1;
    [SerializeField] private EnemyState _currentState;
    protected BaseHealth _baseHealth;
    protected EnemyAttacks _enemyAttacks;
    private AIPath _aiPath;

    [Header("Detection")]
    private float _detectionRadius = 50000;
    public float DetectionRadius() => _detectionRadius;
    [SerializeField] private LayerMask _detectionLayer;
    public LayerMask DetectionLayer() => _detectionLayer;

    public override void Awake()
    {
        base.Awake();
        _aiPath = GetComponent<AIPath>();
        _enemyAttacks = GetComponent<EnemyAttacks>();
        _baseHealth = GetComponent<EnemyHealth>();
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
            EnemyState nextState = _currentState.Tick(this, _baseHealth, BaseAnimator, AiDestinationSetter, _enemyAttacks, _aiPath);

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
