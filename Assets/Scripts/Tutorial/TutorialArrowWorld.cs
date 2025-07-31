using UnityEngine;

public class TutorialArrowWorld : MonoBehaviour
{
    [SerializeField] private RectTransform _arrowTransform;
    private float _heightOffset = 1;
    private Transform _objectTransform;
    private Camera _mainCamera;
    public void SetObjectTransform(Transform transform) => _objectTransform = transform;
    public virtual void Start()
    {
        _mainCamera = Camera.main;
    }
    private void LateUpdate()
    {
        if (_objectTransform == null || _mainCamera == null) return;

        Vector3 screenPosition = _mainCamera.WorldToScreenPoint(_objectTransform.position + Vector3.up * _heightOffset);
        _arrowTransform.position = screenPosition;
    }
}
