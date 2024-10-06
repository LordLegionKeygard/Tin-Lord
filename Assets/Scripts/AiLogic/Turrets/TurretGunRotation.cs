using UnityEngine;

public class TurretGunRotation : MonoBehaviour
{
    [SerializeField] private Vector3 _rotationAxis;
    [SerializeField] private float _lerpSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField]private float _currentSpeed = 0f;
    [SerializeField]private float _targetSpeed = 0f;

    private void FixedUpdate()
    {
        _currentSpeed = Mathf.Lerp(_currentSpeed, _targetSpeed, Time.deltaTime * _lerpSpeed);

        transform.localRotation *= Quaternion.Euler(_rotationAxis * _currentSpeed * Time.deltaTime);
    }

    public void SetRotateToggle(bool isRotateNow)
    {
        _targetSpeed = isRotateNow ? _rotationSpeed : 0;
    }
}
