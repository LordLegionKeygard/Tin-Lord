using UnityEngine;

public class BaseAiStateChanger : MonoBehaviour
{
    [Header("A.I Settings")]
    [HideInInspector] public float CurrentAttackRecoveryTime;
    protected AIDestinationSetter AiDestinationSetter;
    protected BaseAnimator BaseAnimator;

    [Header("Bool")]
    private bool _isCanAttack = true;
    public bool CanAttack() => _isCanAttack;
    private bool _isCanRotate;
    public bool IsCanRotate() => _isCanRotate;

    [Header("Distance")]
    [SerializeField] private float _distanceToTarget;
    public float DistanceToTarget() => _distanceToTarget;

    public virtual void Awake()
    {    
        AiDestinationSetter = GetComponent<AIDestinationSetter>();
        BaseAnimator = GetComponent<BaseAnimator>();
    }

    public virtual void Update()
    {
        
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

    public void HandleAttackRecoveryTime()
    {
        if (CurrentAttackRecoveryTime > 0) CurrentAttackRecoveryTime -= Time.deltaTime;

        if (!_isCanAttack && CurrentAttackRecoveryTime <= 0) _isCanAttack = true;
    }
}
