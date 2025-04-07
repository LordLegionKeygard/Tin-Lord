using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeView : MonoBehaviour
{
    [SerializeField] private Sprite _emptySlot;
    [SerializeField] private Sprite _fullSlot;
    [SerializeField] private Sprite _halfSlot;
    [SerializeField] private Image[] _timeSlots;

    public void UpdateTimeSlotsView(int time)
    {
        int fullSlots = time / 2; // Количество полностью заполненных слотов
        bool hasHalfSlot = (time % 2) != 0; // Есть ли наполовину заполненный слот

        for (int i = 0; i < _timeSlots.Length; i++)
        {
            if (i < fullSlots)
            {
                _timeSlots[i].sprite = _fullSlot;
            }
            else if (i == fullSlots && hasHalfSlot)
            {
                _timeSlots[i].sprite = _halfSlot;
            }
            else
            {
                _timeSlots[i].sprite = _emptySlot;
            }
        }
    }
}
