using UnityEngine;

public class EnemyKnockBack : BaseKnockBack
{
    private EnemyAnimator _creatureAnimator;

    public override void Awake()
    {
        base.Awake();
        _creatureAnimator = GetComponent<EnemyAnimator>();
    }

    public override void CheckKnockBack()
    {
        var rnd = Random.Range(MinimumKnockbackPoints, MaxKnockbackPoints);
        if (CurrentKnockBackPoints > rnd)
        {
            // Debug.Log("Points - " + CurrentKnockBackPoints + "Chance - " + rnd);
            _creatureAnimator.RandomTakeDamage();
            ResetKnockbackPoints();
        }
    }
}
