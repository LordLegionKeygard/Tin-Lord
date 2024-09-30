using System.Collections;
using Pathfinding;
using UnityEngine;

public class EnemyAnimator : BaseAnimator
{
    private IAstarAI _ai;
    private EnemySpeed _creatureSpeed;
    [SerializeField] private int _getHitAnimationsCount;


    public override void Awake()
    {
        base.Awake();
        _ai = GetComponent<IAstarAI>();
        _creatureSpeed = GetComponent<EnemySpeed>();
    }

    public void Update()
    {
        if (_ai.reachedEndOfPath)
        {
            Animator.SetFloat(AnimatorStrings.Speed, 0, 0.1f, Time.deltaTime);
        }
        else
        {
            Vector3 relVelocity = transform.InverseTransformDirection(_ai.velocity);
            relVelocity.y = 0;
            Animator.SetFloat(AnimatorStrings.Speed, relVelocity.magnitude / Animator.transform.lossyScale.x, 0.2f, Time.deltaTime);
        }
    }

    public void RandomTakeDamage()
    {
        int rnd = Random.Range(1, _getHitAnimationsCount + 1);
        Animator.SetInteger(AnimatorStrings.TakeDamage, rnd);

        _creatureSpeed.CantMove();
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
        _creatureSpeed.CantMove();
    }

    public override void ResetAttackAnimation()
    {
        base.ResetAttackAnimation();
        _creatureSpeed.CanMove();
    }

    public void DeathAnim()
    {
        Animator.SetBool(AnimatorStrings.Death, true);
    }
}
