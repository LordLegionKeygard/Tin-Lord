using UnityEngine;
using UnityEngine.UI;

public class BaseSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private RectTransform _sliderTransform;
    private Camera _mainCamera;
    private float _heightOffset;
    private Transform _objectTransform;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    public virtual void SetValue(float value)
    {
        _slider.value = value;
        CheckSliderActive();
    }

    public void CheckSliderActive()
    {
        _slider.gameObject.SetActive(_slider.value != _slider.maxValue);
    }

    public void SetHeightOffset(float value)
    {
        _heightOffset = value;
    }

    public void SetupAllHealthValue(float maxValue)
    {
        _slider.maxValue = maxValue;
        _slider.value = maxValue;
    }

    public void SetupMaxHealth(float maxValue)
    {
        _slider.maxValue = maxValue;
    }

    public virtual void SetLevel(string level)
    {
        
    }

    public void SetObjectTransform(Transform transform) => _objectTransform = transform;

    private void LateUpdate()
    {
        if(_objectTransform == null || _mainCamera == null) return;

        Vector3 screenPosition = _mainCamera.WorldToScreenPoint(_objectTransform.position + Vector3.up * _heightOffset);
        _sliderTransform.position = screenPosition;
    }
}
