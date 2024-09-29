using UnityEngine;

public class BaseAiStateChanger : MonoBehaviour
{
    [Header("A.I Settings")]
    [SerializeField] private float _rotationSpeed = 1;
    [HideInInspector] public float CurrentAttackRecoveryTime;
    protected CreatureAttacks CreatureAttacks;
    protected CreatureHealth CreatureHealth;
    protected AIDestinationSetter AiDestinationSetter;
    public CreatureAnimator CreatureAnimator { get; private set; }

    [Header("Bool")]
    private bool _isCanAttack = true;
    public bool CanAttack() => _isCanAttack;
    private bool _isCanRotate;

    [Header("Detection")]
    public float CurrentDetectionRadius = 50;
    public LayerMask DetectionLayer;

    [Header("Distance")]
    [SerializeField] private float _distanceToTarget;
    public float DistanceToTarget => _distanceToTarget;

    public virtual void Awake()
    {
        CreatureAttacks = GetComponent<CreatureAttacks>();
        CreatureHealth = GetComponent<CreatureHealth>();
        AiDestinationSetter = GetComponent<AIDestinationSetter>();
        CreatureAnimator = GetComponent<CreatureAnimator>();
    }

    public virtual void Update()
    {
        HandleAttackRecoveryTime();

        if (_isCanRotate && AiDestinationSetter.CurrentTarget != null && !CreatureHealth.IsDeath())
        {
            var t = AiDestinationSetter.CurrentTarget.transform.position;

            var targetDirection = new Vector3(t.x, transform.position.y, t.z) - transform.position;

            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, 3 * Time.deltaTime * _rotationSpeed, 0);

            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }

    public virtual void FixedUpdate()
    {
        HandleStateMachine();

        if (AiDestinationSetter.CurrentTarget == null) return;

        var targetPos = AiDestinationSetter.CurrentTarget.transform.position;

        _distanceToTarget = Vector3.Distance(new Vector3(targetPos.x, transform.position.y, targetPos.z), transform.position);
    }

    public void CanRotateForwardToggle(bool state) => _isCanRotate = state;
    public void AttackToggle(bool state) => _isCanAttack = state;

    public virtual void HandleStateMachine()
    {

    }

    private void HandleAttackRecoveryTime()
    {
        if (CurrentAttackRecoveryTime > 0) CurrentAttackRecoveryTime -= Time.deltaTime;

        if (!_isCanAttack && CurrentAttackRecoveryTime <= 0) _isCanAttack = true;
    }
}
