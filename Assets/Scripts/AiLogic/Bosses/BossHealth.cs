using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : EnemyHealth
{
    public override void Death()
    {
        DestroyHealthSlider();
        _isDeath = true;
        _characterController.enabled = false;
        _aiPath.enabled = false;
        _enemyAnimator.DeathAnim();
        DeathSound();
        CustomEvents.FireEnemyDeath(_enemyInfo.GetEnemyNumber());

        StartCoroutine(nameof(BossDeathEvent));
    }

    public override void DeathSound()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.Death[(int)_enemyInfo.GetEnemyEnum()], transform.position);
    }

    private IEnumerator BossDeathEvent()
    {
        yield return new WaitForSeconds(5f);
        CustomEvents.FireObjectiveAmountChange(ObjectiveEnum.KillBoss, 1);       
    }
}
