using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingItemListener : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private BuildingItem _buildingItem;
    public void OnPointerEnter(PointerEventData eventData)
    {
        _buildingItem.SetViewFromListener();
    }
}
