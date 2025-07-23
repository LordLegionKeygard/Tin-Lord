using UnityEngine;
using UnityEngine.EventSystems;

public class MachineItemListener : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private MachineItem _machineItem;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_machineItem.gameObject.activeInHierarchy) return;
        _machineItem.SelectView();
    }
}
