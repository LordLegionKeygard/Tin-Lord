using UnityEngine;

public enum ScreenCorner { TopLeft, TopRight, BottomLeft, BottomRight }

public class ScreenCornerAnchor3D : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private ScreenCorner corner = ScreenCorner.TopLeft;

    [Header("Offsets")]
    [Tooltip("Отступ от края (в пикселях)")]
    [SerializeField] private Vector2 pixelOffset = new(32, 32);
    [Tooltip("Насколько дальше near clip поставить объект")]
    [SerializeField] private float nearOffset = 0.3f;

    private void LateUpdate()
    {
        if (!_camera) return;

        // Пиксели -> viewport
        float vxOff = pixelOffset.x / Mathf.Max(1f, _camera.pixelWidth);
        float vyOff = pixelOffset.y / Mathf.Max(1f, _camera.pixelHeight);

        float vx = (corner == ScreenCorner.TopRight  || corner == ScreenCorner.BottomRight) ? 1f - vxOff : vxOff;
        float vy = (corner == ScreenCorner.TopLeft   || corner == ScreenCorner.TopRight)    ? 1f - vyOff : vyOff;

        // Ставим у nearClip (+ небольшой оффсет)
        float z = Mathf.Clamp(_camera.nearClipPlane + nearOffset,
                              _camera.nearClipPlane + 0.01f,
                              _camera.farClipPlane  - 0.01f);

        Vector3 worldPos = _camera.ViewportToWorldPoint(new Vector3(vx, vy, z));
        transform.position = worldPos;
    }

    // Вспомогательно, если надо менять угол из кода
    public void SetCorner(ScreenCorner c) => corner = c;
    public void SetCamera(Camera c) => _camera = c;
}
