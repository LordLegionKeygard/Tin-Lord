using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private RectTransform _sliderTransform;
    private Camera _mainCamera;
    private float _heightOffset = -3.5f;
    private Transform _objectTransform;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    public void SetHealth(float health)
    {
        _slider.value = health;
        CheckSliderActive();
    }

    public void CheckSliderActive()
    {
        if (!_slider.gameObject.activeInHierarchy) _slider.gameObject.SetActive(true);
    }

    public void SetMaxHealth(float maxHealth)
    {
        _slider.maxValue = maxHealth;
        _slider.value = maxHealth;
    }

    public void SetObjectTransform(Transform transform) => _objectTransform = transform;

    private void LateUpdate()
    {
        if(_objectTransform == null) return;

        Vector3 screenPosition = _mainCamera.WorldToScreenPoint(_objectTransform.position + Vector3.up * _heightOffset);
        _sliderTransform.position = screenPosition;
    }
}
