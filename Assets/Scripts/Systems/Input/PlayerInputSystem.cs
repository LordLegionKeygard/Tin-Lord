using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputSystem : MonoBehaviour
{
    public static PlayerInputSystem Instance;

    private PlayerInput _playerInput;

    public Vector2 MoveInput { get; private set; }

    private delegate void CameraZoom(InputAction.CallbackContext context);
    private CameraZoom cameraZoom;

    private delegate void LeftMouseClick();
    private LeftMouseClick _leftMouseClick;

    private delegate void RightMouseClick();
    private RightMouseClick _rightMouseClick;

    private delegate void GameSpeedPause(int gameSpeed);
    private GameSpeedPause _gameSpeedPause;

    private delegate void GameSpeedDefault(int gameSpeed);
    private GameSpeedDefault _gameSpeedDefault;

    private delegate void GameSpeedDouble(int gameSpeed);
    private GameSpeedDouble _gameSpeedDouble;

    private delegate void GameSpeedTriple(int gameSpeed);
    private GameSpeedTriple _gameSpeedTriple;

    private delegate void BuildButton();
    private BuildButton _buildButton;

    private delegate void SelectNumbers(InputAction.CallbackContext context);
    private SelectNumbers _selectNumbers;


    [Header("Links")]
    [SerializeField] private TileDetector _tileDetector;
    [SerializeField] private CardHolderSystem _cardHolderSystem;
    [SerializeField] private CameraMovement _cameraMovement;
    [SerializeField] private GameSpeedSystem _gameSpeedSystem;
    [SerializeField] private SelectTilePanel _selectTilePanel;
    [SerializeField] private BuildTypesPanel _buildTypesPanel;

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
        _playerInput.actions["LeftMouseClick"].performed += _ => _leftMouseClick();
        _playerInput.actions["RightMouseClick"].performed += _ => _rightMouseClick();
        _playerInput.actions["GameSpeedPause"].performed += _ => _gameSpeedPause((int)GameSpeedEnum.Pause);
        _playerInput.actions["GameSpeedDefault"].performed += _ => _gameSpeedDefault((int)GameSpeedEnum.Default);
        _playerInput.actions["GameSpeedDouble"].performed += _ => _gameSpeedDouble((int)GameSpeedEnum.Double);
        _playerInput.actions["GameSpeedTriple"].performed += _ => _gameSpeedTriple((int)GameSpeedEnum.Triple);
        _playerInput.actions["BuildButton"].performed += _ => _buildButton();
        _playerInput.actions["SelectNumbers"].performed += ctx => _selectNumbers(ctx);
    }

    private void SetupDelegates()
    {
        cameraZoom = new CameraZoom(_cameraMovement.ZoomCamera);
        _leftMouseClick = new LeftMouseClick(_tileDetector.InputOnTile);
        _rightMouseClick = new RightMouseClick(_cardHolderSystem.CancelSelectCard);
        _gameSpeedPause = new GameSpeedPause(_gameSpeedSystem.ChangeGameSpeed);
        _gameSpeedDefault = new GameSpeedDefault(_gameSpeedSystem.ChangeGameSpeed);
        _gameSpeedDouble = new GameSpeedDouble(_gameSpeedSystem.ChangeGameSpeed);
        _gameSpeedTriple = new GameSpeedTriple(_gameSpeedSystem.ChangeGameSpeed);
        _buildButton = new BuildButton(_selectTilePanel.PlayerInputBuildButton);
        _selectNumbers = new SelectNumbers(OnNumberInput);
    }

    private void OnNumberInput(InputAction.CallbackContext context)
    {
        var key = context.control.displayName; // Получаем нажатую клавишу как строку
        int pressedNumber;
        if (int.TryParse(key, out pressedNumber))
        {
            _buildTypesPanel.PlayerInputBuildTypesButton(pressedNumber);
        }
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

        _leftMouseClick = delegate { };
        _rightMouseClick = delegate { };
        _gameSpeedPause = delegate { };
        _gameSpeedDefault = delegate { };
        _gameSpeedDouble = delegate { };
        _gameSpeedTriple = delegate { };
        _buildButton = delegate { };
        _selectNumbers = delegate { };
    }
}
