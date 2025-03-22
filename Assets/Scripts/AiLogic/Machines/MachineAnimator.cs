using System.Collections;
using UnityEngine;

public class MachineAnimator : BaseAnimator
{
    private MachineSpeed _machineSpeed;
    [SerializeField] private int _getHitAnimationsCount;


    public override void Awake()
    {
        base.Awake();
        _machineSpeed = GetComponent<MachineSpeed>();
    }

    public void Update()
    {
        Animator.SetFloat(AnimatorStrings.Speed, _machineSpeed.Speed(), 0, Time.deltaTime);
    }

    public void RandomTakeDamage()
    {
        int rnd = Random.Range(1, _getHitAnimationsCount + 1);
        Animator.SetInteger(AnimatorStrings.TakeDamage, rnd);

        _machineSpeed.CantMove();
        StartCoroutine(nameof(ResetTakeDamageAnimation));
    }

    private IEnumerator ResetTakeDamageAnimation()
    {
        yield return new WaitForSeconds(0.2f);
        Animator.SetInteger(AnimatorStrings.TakeDamage, 0);
        ResetAttackAnimation();
    }

    public override void AttackAnimation(int attackNumber)
    {
        base.AttackAnimation(attackNumber);
        _machineSpeed.CantMove();
    }

    public override void ResetAttackAnimation()
    {
        base.ResetAttackAnimation();
        _machineSpeed.CanMove();
    }
}
