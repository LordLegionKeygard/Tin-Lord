using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : BaseAnimator
{
    private PlayerSpeed _playerSpeed;
    [SerializeField] private int _getHitAnimationsCount;


    public override void Awake()
    {
        base.Awake();
        _playerSpeed = GetComponent<PlayerSpeed>();
    }

    public void Update()
    {
        Animator.SetFloat(AnimatorStrings.Speed, _playerSpeed.Speed(), 0.1f, Time.deltaTime);
    }

    public void RandomTakeDamage()
    {
        int rnd = Random.Range(1, _getHitAnimationsCount + 1);
        Animator.SetInteger(AnimatorStrings.TakeDamage, rnd);

        _playerSpeed.CantMove();
        StartCoroutine(nameof(ResetTakeDamageAnimation));
    }

    private IEnumerator ResetTakeDamageAnimation()
    {
        yield return new WaitForSeconds(0.2f);
        Animator.SetInteger(AnimatorStrings.TakeDamage, 0);
        ResetAttackAnimation();
    }

    public override void AttackAnim(int attackNumber)
    {
        base.AttackAnim(attackNumber);
        _playerSpeed.CantMove();
    }

    public override void ResetAttackAnimation()
    {
        base.ResetAttackAnimation();
        _playerSpeed.CanMove();
    }

    public void DeathAnim()
    {
        Animator.SetBool(AnimatorStrings.Death, true);
    }
}
