using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class CreatureAnimator : MonoBehaviour
{
    [HideInInspector] public Animator Animator;
    public BaseAiStateChanger BaseAiStateChanger { get; private set; }
    private IAstarAI _ai;
    private CreatureSpeed _creatureSpeed;
    [SerializeField] private int _getHitAnimationsCount;


    public virtual void Awake()
    {
        _ai = GetComponent<IAstarAI>();
        Animator = GetComponent<Animator>();
        _creatureSpeed = GetComponent<CreatureSpeed>();
        BaseAiStateChanger = GetComponent<BaseAiStateChanger>();
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

    public void AttackAnim(int attackNumber)
    {
        Animator.SetInteger(AnimatorStrings.Attack, attackNumber);
        _creatureSpeed.CantMove();
        Invoke(nameof(ResetAttackAnimation), 0.5f);
    }

    public void ResetAttackAnimation()
    {
        Animator.SetInteger(AnimatorStrings.Attack, 0);
        _creatureSpeed.CanMove();
    }
}
