using System;
using UnityEngine;
using Zenject;

public class EnemyDefence : MonoBehaviour
{
    [Inject] private EcologySystem _ecologySystem;
    private float _currentDefencePercent = 1;

    public float GetDefencePercent() => _currentDefencePercent;

    private void Start()
    {
        CustomEvents.OnUpdateEnemyDefence += ChangeDefence;
    }

    private void ChangeDefence()
    {
        int total = _ecologySystem.GetTotalEcology();

        if (total >= 0)
        {
            _currentDefencePercent = 1f;
            return;
        }

        int tens = Math.Abs(total) / 10;
        float percent = (tens + 1) * 0.1f;
        _currentDefencePercent = 1 - Math.Min(percent, 0.9f);
    }


    private void OnDestroy()
    {
        CustomEvents.OnUpdateEnemyDefence -= ChangeDefence;
    }
}
