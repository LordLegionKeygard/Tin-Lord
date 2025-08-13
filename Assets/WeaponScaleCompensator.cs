using UnityEngine;

public class WeaponScaleCompensator : MonoBehaviour
{
    [SerializeField] private Camera mainCamera; // основная игровая камера
    [SerializeField] private float baseSize = 40f; // размер mainCamera при котором scale = 1

    private Vector3 _initialScale;

    private void Awake()
    {
        _initialScale = transform.localScale;
    }

    private void LateUpdate()
    {
        // компенсируем зум изменением масштаба
        float scaleFactor = mainCamera.orthographicSize / baseSize;
        transform.localScale = _initialScale * scaleFactor;
    }
}
