using UnityEngine;

public class CreatureKnockBack : BaseKnockBack
{
    private CreatureAnimator _creatureAnimator;

    public override void Awake()
    {
        base.Awake();
        _creatureAnimator = GetComponent<CreatureAnimator>();
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
