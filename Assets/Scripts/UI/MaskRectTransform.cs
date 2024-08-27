using UnityEngine;
using UnityEngine.UI;

public class MaskRectTransform : MonoBehaviour
{
    [SerializeField] public RectTransform _targetRectTransform;
    [SerializeField] private RectTransform _currentRectTransform;
    private Vector2 _previousSizeDelta;
    
    private void SetSizeDelta() => _previousSizeDelta = _targetRectTransform.sizeDelta;

    private void Start()
    {
        SetSizeDelta();
        CopyRectTransformValues();
    }

    private void Update()
    {
        if (_targetRectTransform.sizeDelta != _previousSizeDelta)
        {
            CopyRectTransformValues();
            SetSizeDelta();
        }
    }

    private void CopyRectTransformValues()
    {
        _currentRectTransform.anchorMin = _targetRectTransform.anchorMin;
        _currentRectTransform.anchorMax = _targetRectTransform.anchorMax;
        _currentRectTransform.pivot = _targetRectTransform.pivot;
        _currentRectTransform.sizeDelta = _targetRectTransform.sizeDelta;
        _currentRectTransform.anchoredPosition = _targetRectTransform.anchoredPosition;
        _currentRectTransform.localScale = _targetRectTransform.localScale;
        _currentRectTransform.localRotation = _targetRectTransform.localRotation;
    }

}
