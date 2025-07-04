using UnityEngine;
using UnityEngine.UI;

public class CellsView : MonoBehaviour
{
    [SerializeField] private Sprite _emptySlot;
    [SerializeField] private Sprite _fullSlot;
    [SerializeField] private Sprite _halfSlot;
    [SerializeField] private Image[] _cellSlots;

    public void UpdateCellSlotsView(int value)
    {
        if (value < 0)
        {
            foreach (var item in _cellSlots)
            {
                item.sprite = _emptySlot;
            }
            return;
        }

        int fullSlots = value / 2; // Количество полностью заполненных слотов
        bool hasHalfSlot = (value % 2) != 0; // Есть ли наполовину заполненный слот

        for (int i = 0; i < _cellSlots.Length; i++)
        {
            if (i < fullSlots)
            {
                _cellSlots[i].sprite = _fullSlot;
            }
            else if (i == fullSlots && hasHalfSlot)
            {
                _cellSlots[i].sprite = _halfSlot;
            }
            else
            {
                _cellSlots[i].sprite = _emptySlot;
            }
        }
    }
}
