using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    // private CameraControlActions cameraActions;
    // private InputAction movement;
    [SerializeField] private Transform cameraTransform;

    [Header("Horizontal Translation")]
    [SerializeField] private float maxSpeed = 5f;
    private float speed;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float damping = 15f;

    [Header("Vertical Translation")]
    [SerializeField] private float stepSize = 2f;
    [SerializeField] private float zoomDampening = 7.5f;
    [SerializeField] private float minHeight = 5f;
    [SerializeField] private float maxHeight = 50f;
    [SerializeField] private float zoomSpeed = 2f;

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


    private void Awake()
    {
        // cameraActions = new CameraControlActions();
        // cameraTransform = this.GetComponentInChildren<Camera>().transform;
    }

    private void OnEnable()
    {
        zoomHeight = cameraTransform.localPosition.y;
        cameraTransform.LookAt(this.transform);

        lastPosition = this.transform.position;

        // movement = cameraActions.Camera.Movement;
        // cameraActions.Camera.Zoom.performed += ZoomCamera;
        // cameraActions.Camera.Enable();
    }

    private void OnDisable()
    {
        // cameraActions.Camera.Zoom.performed -= ZoomCamera;
        // cameraActions.Camera.Disable();
    }

    private void Update()
    {
        GetKeyboardMovement();
        // CheckMouseAtScreenEdge();

        UpdateVelocity();
        UpdateBasePosition();
        UpdateCameraPosition();
        UpdateLimits();
    }

    private void UpdateVelocity()
    {
        horizontalVelocity = (this.transform.position - lastPosition) / Time.deltaTime;
        horizontalVelocity.y = 0f;
        lastPosition = this.transform.position;
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
            speed = Mathf.Lerp(speed, maxSpeed, Time.deltaTime * acceleration);
            transform.position += targetPosition * speed * Time.deltaTime;
        }
        else
        {
            horizontalVelocity = Vector3.Lerp(horizontalVelocity, Vector3.zero, Time.deltaTime * damping);
            transform.position += horizontalVelocity * Time.deltaTime;
        }
        targetPosition = Vector3.zero;
    }

    public void ZoomCamera(InputAction.CallbackContext callBack)
    {
        float inputValue = -callBack.ReadValue<Vector2>().y / 100f;

        if (Mathf.Abs(inputValue) > 0.1f)
        {
            _camera.orthographicSize += inputValue * stepSize;

            if (_camera.orthographicSize < minHeight)
                _camera.orthographicSize = minHeight;
            else if (_camera.orthographicSize > maxHeight)
                _camera.orthographicSize = maxHeight;
        }
    }

    private void UpdateCameraPosition()
    {
        Vector3 zoomTarget = new Vector3(cameraTransform.localPosition.x, zoomHeight, cameraTransform.localPosition.z);

        zoomTarget -= zoomSpeed * (zoomHeight - cameraTransform.localPosition.y) * Vector3.forward;

        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, zoomTarget, Time.deltaTime * zoomDampening);
        cameraTransform.LookAt(this.transform);
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

