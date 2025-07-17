using UnityEngine;

public class EnemyKnockBack : BaseKnockBack
{
    private EnemyAnimator _creatureAnimator;
    private EnemyInfo _enemyInfo;

    public override void Awake()
    {
        base.Awake();
        _creatureAnimator = GetComponent<EnemyAnimator>();
        _enemyInfo = GetComponent<EnemyInfo>();
    }

    public override void CheckKnockBack()
    {
        if (_enemyInfo != null && _enemyInfo.IsMiniBoss()) return;

        var rnd = Random.Range(MinimumKnockbackPoints, MaxKnockbackPoints);
        if (CurrentKnockBackPoints > rnd)
        {
            // Debug.Log("Points - " + CurrentKnockBackPoints + "Chance - " + rnd);
            _creatureAnimator.RandomTakeDamage();
            ResetKnockbackPoints();
        }
    }
}
