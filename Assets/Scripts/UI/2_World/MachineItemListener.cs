using UnityEngine;
using UnityEngine.EventSystems;

public class MachineItemListener : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private MachineItem _machineItem;
    public void OnPointerEnter(PointerEventData eventData)
    {
        _machineItem.SelectView();
    }
}
