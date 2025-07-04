using UnityEngine;
using UnityEngine.EventSystems;

public class IsPointerOverUISystem : MonoBehaviour
{
    public static bool IsPointerOverUI;

    private void Update()
    {
        IsPointerOverUI = EventSystem.current.IsPointerOverGameObject();
    }
}
