using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _cameraSpeedCoeff;
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private CardHolderSystem _cardHolderSystem;

    [Header("Horizontal Translation")]
    [SerializeField] private float _currentMaxSpeed;
    private float _speed;
    private readonly float _acceleration = 10;
    private readonly float _damping = 15;

    [Header("Vertical Translation")]
    private readonly float _stepSize = 2;
    private readonly float _zoomDampening = 7.5f;
    private readonly float _minHeight = 12;
    private readonly float _maxHeight = 60;
    private readonly float _zoomSpeed = 4;

    [Header("Mouse Dragging")]
    private bool _isDragging = false;
    private Vector3 _startPos;

    [Header("Edge Movement")]
    [SerializeField, Range(0f, 0.1f)] private float edgeTolerance = 0.05f;
    private Vector3 _targetPosition;
    private float _zoomHeight;
    private Vector3 _horizontalVelocity;
    private Vector3 _lastPosition;
    [SerializeField] private int _xMin = -50;
    [SerializeField] private int _xMax = 150; //20
    [SerializeField] private int _yMin = -50;
    [SerializeField] private int _yMax = 110; //16

    private void Start()
    {
        CustomEvents.OnDataLoad += SetCameraEdges;
    }

    private void OnEnable()
    {
        _zoomHeight = cameraTransform.localPosition.y;
        cameraTransform.LookAt(transform);

        _lastPosition = transform.position;
    }

    private void SetCameraEdges()
    {
        var mission = CurrentMissionInfo.Instance.GetCurrentMission();

        _xMax = 50 + (mission.MapLength - 10) * 10;
        _yMax = 50 + (mission.MapWidth - 10) * 10;
    }

    public void ChangeCameraSpeedCoeff(float value)
    {
        _cameraSpeedCoeff = value;
    }

    private void ChangeMaxSpeed()
    {
        _currentMaxSpeed = _camera.orthographicSize * _cameraSpeedCoeff * 0.1f;
    }

    private void Update()
    {
        GetKeyboardMovement();
        // CheckMouseAtScreenEdge();

        UpdateVelocity();
        UpdateBasePosition();
        UpdateCameraPosition();
        ChangeMaxSpeed();
        HandleMouseDrag();
        UpdateLimits();
    }

    private void HandleMouseDrag()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame && !IsPointerOverUISystem.IsPointerOverUI)
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (plane.Raycast(ray, out float distance))
            {
                _startPos = ray.GetPoint(distance);
                _isDragging = true;
            }
        }

        if (Mouse.current.rightButton.wasReleasedThisFrame)
        {
            _isDragging = false;
        }

        if (_isDragging)
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (plane.Raycast(ray, out float distance))
            {
                Vector3 currentPos = ray.GetPoint(distance);
                Vector3 offset = _startPos - currentPos;
                Vector3 newCamPos = transform.position + offset;
                transform.position = newCamPos;
            }
        }
    }

    private void UpdateVelocity()
    {
        _horizontalVelocity = (transform.position - _lastPosition) / Time.unscaledDeltaTime;
        _horizontalVelocity.y = 0f;
        _lastPosition = transform.position;
    }

    private void GetKeyboardMovement()
    {
        Vector3 inputValue = InputSystemWorld.Instance.MoveInput.x * GetCameraRight()
                    + InputSystemWorld.Instance.MoveInput.y * GetCameraForward();

        inputValue = inputValue.normalized;

        if (inputValue.sqrMagnitude > 0.1f)
            _targetPosition += inputValue;
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

        _targetPosition += moveDirection;
    }

    private void UpdateBasePosition()
    {
        if (_targetPosition.sqrMagnitude > 0.1f)
        {
            _speed = Mathf.Lerp(_speed, _currentMaxSpeed, Time.unscaledDeltaTime * _acceleration);
            transform.position += _targetPosition * _speed * Time.unscaledDeltaTime;
        }
        else
        {
            _horizontalVelocity = Vector3.Lerp(_horizontalVelocity, Vector3.zero, Time.unscaledDeltaTime * _damping);
            transform.position += _horizontalVelocity * Time.unscaledDeltaTime;
        }
        _targetPosition = Vector3.zero;
    }

    public void ZoomCamera(InputAction.CallbackContext callBack)
    {
        if (IsPointerOverUISystem.IsPointerOverUI) return;

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
        Vector3 zoomTarget = new Vector3(cameraTransform.localPosition.x, _zoomHeight, cameraTransform.localPosition.z);

        zoomTarget -= _zoomSpeed * (_zoomHeight - cameraTransform.localPosition.y) * Vector3.forward;

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

    private void OnDestroy()
    {
        CustomEvents.OnDataLoad -= SetCameraEdges;
    }
}

