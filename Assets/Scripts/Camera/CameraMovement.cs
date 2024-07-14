using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform cameraTransform;

    [Header("Horizontal Translation")]
    [SerializeField] private float _currentMaxSpeed;
    private float speed;
    private float _acceleration = 10;
    private float _damping = 15;

    [Header("Vertical Translation")]
    private float _stepSize = 2;
    private float _zoomDampening = 7.5f;
    private float _minHeight = 12;
    private float _maxHeight = 60;
    private float _zoomSpeed = 4;

    [Header("Edge Movement")]
    [SerializeField, Range(0f, 0.1f)] private float edgeTolerance = 0.05f;
    private Vector3 targetPosition;
    private float zoomHeight;
    private Vector3 horizontalVelocity;
    private Vector3 lastPosition;
    private int _xMin = -50;
    private int _xMax = 150;

    private int _yMin = -50;
    private int _yMax = 110;


    private void OnEnable()
    {
        zoomHeight = cameraTransform.localPosition.y;
        cameraTransform.LookAt(transform);

        lastPosition = transform.position;
    }

    private void ChangeMaxSpeed()
    {
        _currentMaxSpeed = _camera.orthographicSize * 1.4f;
    }

    private void Update()
    {
        GetKeyboardMovement();
        // CheckMouseAtScreenEdge();

        UpdateVelocity();
        UpdateBasePosition();
        UpdateCameraPosition();
        UpdateLimits();
        ChangeMaxSpeed();
    }

    private void UpdateVelocity()
    {
        horizontalVelocity = (transform.position - lastPosition) / Time.unscaledDeltaTime;
        horizontalVelocity.y = 0f;
        lastPosition = transform.position;
    }

    private void GetKeyboardMovement()
    {
        Vector3 inputValue = PlayerInputSystem.Instance.MoveInput.x * GetCameraRight()
                    + PlayerInputSystem.Instance.MoveInput.y * GetCameraForward();

        inputValue = inputValue.normalized;

        if (inputValue.sqrMagnitude > 0.1f)
            targetPosition += inputValue;
    }

    private void CheckMouseAtScreenEdge()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector3 moveDirection = Vector3.zero;

        if (mousePosition.x < edgeTolerance * Screen.width)
            moveDirection += -GetCameraRight();
        else if (mousePosition.x > (1f - edgeTolerance) * Screen.width)
            moveDirection += GetCameraRight();

        if (mousePosition.y < edgeTolerance * Screen.height)
            moveDirection += -GetCameraForward();
        else if (mousePosition.y > (1f - edgeTolerance) * Screen.height)
            moveDirection += GetCameraForward();

        targetPosition += moveDirection;
    }

    private void UpdateBasePosition()
    {
        if (targetPosition.sqrMagnitude > 0.1f)
        {
            speed = Mathf.Lerp(speed, _currentMaxSpeed, Time.unscaledDeltaTime * _acceleration);
            transform.position += targetPosition * speed * Time.unscaledDeltaTime;
        }
        else
        {
            horizontalVelocity = Vector3.Lerp(horizontalVelocity, Vector3.zero, Time.unscaledDeltaTime * _damping);
            transform.position += horizontalVelocity * Time.unscaledDeltaTime;
        }
        targetPosition = Vector3.zero;
    }

    public void ZoomCamera(InputAction.CallbackContext callBack)
    {
        if(EventSystem.current.IsPointerOverGameObject()) return;
        
        float inputValue = -callBack.ReadValue<Vector2>().y / 100f;

        if (Mathf.Abs(inputValue) > 0.1f)
        {
            _camera.orthographicSize += inputValue * _stepSize;

            if (_camera.orthographicSize < _minHeight)
                _camera.orthographicSize = _minHeight;
            else if (_camera.orthographicSize > _maxHeight)
                _camera.orthographicSize = _maxHeight;
        }
    }

    private void UpdateCameraPosition()
    {
        Vector3 zoomTarget = new Vector3(cameraTransform.localPosition.x, zoomHeight, cameraTransform.localPosition.z);

        zoomTarget -= _zoomSpeed * (zoomHeight - cameraTransform.localPosition.y) * Vector3.forward;

        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, zoomTarget, Time.unscaledDeltaTime * _zoomDampening);
        cameraTransform.LookAt(transform);
    }

    private Vector3 GetCameraForward()
    {
        Vector3 forward = cameraTransform.forward;
        forward.y = 0f;
        return forward;
    }

    private Vector3 GetCameraRight()
    {
        Vector3 right = cameraTransform.right;
        right.y = 0f;
        return right;
    }

    public void UpdateLimits()
    {
        Vector3 vector = transform.position;
        if (vector.x < _xMin) vector.x = _xMin;
        else if (vector.x > _xMax) vector.x = _xMax;
        if (vector.z < _yMin) vector.z = _yMin;
        else if (vector.z > _yMax) vector.z = _yMax;

        transform.position = vector;
    }
}

