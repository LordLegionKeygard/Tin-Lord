using UnityEngine;

public enum ScreenCorner { TopLeft, TopRight, BottomLeft, BottomRight }

public class ScreenCornerAnchor3D : MonoBehaviour
{
    [SerializeField] private Camera cam;                // <-- WeaponsCamera (Overlay)
    [SerializeField] private ScreenCorner corner = ScreenCorner.TopLeft;

    [Header("Offsets")]
    [Tooltip("Отступ от края (в пикселях)")]
    [SerializeField] private Vector2 pixelOffset = new(32, 32);
    [Tooltip("Насколько дальше near clip поставить объект")]
    [SerializeField] private float nearOffset = 0.3f;   // 0.2–1 обычно хватает

    [Header("Rotation")]
    [SerializeField] private bool matchCameraRotation = false; // если true — якорь смотрит как камера

    private void LateUpdate()
    {
        if (!cam) return;

        // Пиксели -> viewport
        float vxOff = pixelOffset.x / Mathf.Max(1f, cam.pixelWidth);
        float vyOff = pixelOffset.y / Mathf.Max(1f, cam.pixelHeight);

        float vx = (corner == ScreenCorner.TopRight  || corner == ScreenCorner.BottomRight) ? 1f - vxOff : vxOff;
        float vy = (corner == ScreenCorner.TopLeft   || corner == ScreenCorner.TopRight)    ? 1f - vyOff : vyOff;

        // Ставим у nearClip (+ небольшой оффсет)
        float z = Mathf.Clamp(cam.nearClipPlane + nearOffset,
                              cam.nearClipPlane + 0.01f,
                              cam.farClipPlane  - 0.01f);

        Vector3 worldPos = cam.ViewportToWorldPoint(new Vector3(vx, vy, z));
        transform.position = worldPos;

        if (matchCameraRotation)
            transform.rotation = Quaternion.LookRotation(cam.transform.forward, cam.transform.up);
    }

    // Вспомогательно, если надо менять угол из кода
    public void SetCorner(ScreenCorner c) => corner = c;
    public void SetCamera(Camera c) => cam = c;
}
