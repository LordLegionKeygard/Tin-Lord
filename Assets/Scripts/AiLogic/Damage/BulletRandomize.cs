using UnityEngine;

public class BulletRandomize : MonoBehaviour
{
    private Transform _transform;
    private Vector3 _defaultScale;

    [SerializeField] private bool _isRandomScale, _isRandomRotation;
    [SerializeField] private float _minScale, _maxScale;
    [SerializeField] private float _minRotation, _maxRotaion;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _defaultScale = _transform.localScale;
    }

    private void OnEnable()
    {
        if (_isRandomScale)
            _transform.localScale = _defaultScale * Random.Range(_minScale, _maxScale);

        if (_isRandomRotation)
            _transform.rotation *= Quaternion.Euler(0, 0, Random.Range(_minRotation, _maxRotaion));
    }
}