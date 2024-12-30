using UnityEngine;
using UnityEngine.EventSystems;

public class RobotItemListener : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private RobotItem _robotItem;
    public void OnPointerEnter(PointerEventData eventData)
    {
        _robotItem.SelectView();
    }
}
