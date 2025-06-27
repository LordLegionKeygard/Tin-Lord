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

    public virtual void AttackAnimation(int attackNumber)
    {
        Animator.SetInteger(AnimatorStrings.Attack, attackNumber);
        Invoke(nameof(ResetAttackAnimation), 0.5f);
    }

    public virtual void ResetAttackAnimation()
    {
        Animator.SetInteger(AnimatorStrings.Attack, 0);
    }

    public void RepairAnimation(bool state)
    {
        Animator.SetBool(AnimatorStrings.Repair, state);
    }

    public virtual void IsCombat(bool state)
    {

    }

    public virtual void CanTarget()
    {

    }

    public virtual void CantTarget()
    {

    }
}
