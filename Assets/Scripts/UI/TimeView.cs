using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeView : MonoBehaviour
{
    [SerializeField] private Sprite _emptySlot;
    [SerializeField] private Sprite _fullSlot;
    [SerializeField] private Image[] _timeSlots;

    public void UpdateTimeSlotsView(int time)
    {
        for (int i = 0; i < _timeSlots.Length; i++)
        {
            if(i < time) _timeSlots[i].sprite = _fullSlot;
            else _timeSlots[i].sprite = _emptySlot; 
        }
    }

}
