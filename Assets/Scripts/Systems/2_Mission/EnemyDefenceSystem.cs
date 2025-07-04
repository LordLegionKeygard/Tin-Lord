using System;
using UnityEngine;

public class EnemyDefenceSystem : MonoBehaviour
{
    [SerializeField] private EcologySystem _ecologySystem;
    private float _currentDefencePercent = 1;
    private int _currentSliderValue = 0;
    public float GetDefencePercent() => _currentDefencePercent;
    public int GetSliderValue() => _currentSliderValue;


    public void ChangeDefence()
    {
        int total = _ecologySystem.GetTotalEcology();

        // 0 % резиста → 0 активных ячеек
        if (total >= 0)
        {
            _currentDefencePercent = 1;
            _currentSliderValue = 0;
        }
        else
        {
            int tens = Math.Abs(total) / 10;     // 0..8
            float damage = (tens + 1) * 0.1f;        // 0.1 … 0.9
            _currentDefencePercent = 1f - Math.Min(damage, 0.9f);

            _currentSliderValue = tens + 1;                    // 1…9 (сколько «лампочек» показать)
        }

        CustomEvents.FireUpdateEnemySliderDefence(_currentSliderValue);
    }
}
