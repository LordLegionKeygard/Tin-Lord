using System.Collections;
using UnityEngine;

public class RobotAnimator : BaseAnimator
{
    private RobotSpeed _robotSpeed;
    [SerializeField] private int _getHitAnimationsCount;


    public override void Awake()
    {
        base.Awake();
        _robotSpeed = GetComponent<RobotSpeed>();
    }

    public void Update()
    {
        Animator.SetFloat(AnimatorStrings.Speed, _robotSpeed.Speed(), 0, Time.deltaTime);
    }

    public void RandomTakeDamage()
    {
        int rnd = Random.Range(1, _getHitAnimationsCount + 1);
        Animator.SetInteger(AnimatorStrings.TakeDamage, rnd);

        _robotSpeed.CantMove();
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
        _robotSpeed.CantMove();
    }

    public override void ResetAttackAnimation()
    {
        base.ResetAttackAnimation();
        _robotSpeed.CanMove();
    }
}
