using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAnimator : MonoBehaviour
{
    protected Animator Animator;

    public virtual void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    public virtual void AttackAnim(int attackNumber)
    {
        Animator.SetInteger(AnimatorStrings.Attack, attackNumber);
        Invoke(nameof(ResetAttackAnimation), 0.5f);
    }

    public virtual void ResetAttackAnimation()
    {
        Animator.SetInteger(AnimatorStrings.Attack, 0);
    }
}
