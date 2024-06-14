using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputSystem : MonoBehaviour
{
    public static PlayerInputSystem Instance;

    private PlayerInput _playerInput;

    public Vector2 MoveInput { get; private set; }

    private delegate void CameraZoom(InputAction.CallbackContext context);
    CameraZoom cameraZoom;

    private delegate void LeftMouseClick();
    LeftMouseClick leftMouseClick;

    private delegate void RightMouseClick();
    RightMouseClick rightMouseClick;

    [Header("Links")]
    [SerializeField] private TileDetector _tileDetector;
    [SerializeField] private CardHolderSystem _cardHolderSystem;
    [SerializeField] private CameraMovement _cameraMovement;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("More than one instance of PlayerInputSystem found!");
            return;
        }
        Instance = this;

        _playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        InputToggle(true);
        SetupInputActions();
        SetupDelegates();
    }

    private void Update()
    {
        UpdateInputs();
    }

    private void SetupInputActions()
    {
        _playerInput.actions["CameraZoom"].started += ctx => cameraZoom(ctx);
        _playerInput.actions["LeftMouseClick"].performed += _ => leftMouseClick();
        _playerInput.actions["RightMouseClick"].performed += _ => rightMouseClick();
    }

    private void SetupDelegates()
    {
        cameraZoom = new CameraZoom(_cameraMovement.ZoomCamera);
        leftMouseClick = new LeftMouseClick(_tileDetector.InputOnTile);
        rightMouseClick = new RightMouseClick(_cardHolderSystem.CancelSelectCard);
    }

    private void UpdateInputs()
    {
        MoveInput = _playerInput.actions["CameraMovement"].ReadValue<Vector2>();
    }

    public void InputToggle(bool state)
    {
        if (state) _playerInput.ActivateInput();
        else _playerInput.DeactivateInput();
    }

    private void OnDestroy()
    {
        InputToggle(false);

        leftMouseClick = delegate { };
        rightMouseClick = delegate { };
    }
}
