using UnityEngine;
using UnityEngine.InputSystem;
public class InputSystemWorld : MonoBehaviour
{
    public static InputSystemWorld Instance;
    private PlayerInput _playerInput;

    //CameraControl
    public Vector2 MoveInput { get; private set; }
    private delegate void CameraZoom(InputAction.CallbackContext context);
    private CameraZoom _cameraZoom;

    //MouseClick
    private delegate void LeftMouseClick();
    private LeftMouseClick _leftMouseClick;
    private delegate void RightMouseClick();
    private RightMouseClick _rightMouseClick;

    // GameSpeed
    private delegate void GameSpeedPause(int gameSpeed);
    private GameSpeedPause _gameSpeedPause;
    private delegate void GameSpeedDefault(int gameSpeed);
    private GameSpeedDefault _gameSpeedDefault;
    private delegate void GameSpeedDouble(int gameSpeed);
    private GameSpeedDouble _gameSpeedDouble;
    private delegate void GameSpeedTriple(int gameSpeed);
    private GameSpeedTriple _gameSpeedTriple;

    //UserInterface
    public delegate void Escape();
    private Escape _escape;

    //SelectTilePanel
    private delegate void SelectNumbers(InputAction.CallbackContext context);
    private SelectNumbers _selectNumbers;
    private delegate void BuildTileButton();
    private BuildTileButton _buildTileButton;
    public delegate void RotateTileButton();
    private RotateTileButton _rotateTileButton;
    public delegate void DestroyTileButton();
    private DestroyTileButton _destroyTileButton;
    public delegate void WorkTileButton();
    private WorkTileButton _workTileButton;
    public delegate void RobotPanelButton();
    private RobotPanelButton _robotPanelButton;


    [Header("Links")]
    [SerializeField] private TileDetector _tileDetector;
    [SerializeField] private CameraMovement _cameraMovement;
    [SerializeField] private GameSpeedSystem _gameSpeedSystem;
    [SerializeField] private SelectTilePanel _selectTilePanel;
    [SerializeField] private BuildTypesPanel _buildTypesPanel;
    [SerializeField] private BuildsPanel _buildsPanel;
    [SerializeField] private UIPanelsWorld _uiPanels;
    [SerializeField] private RobotPanel _robotPanel;

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

        CustomEvents.OnMissionEnd += MissionEndDisableInputs;
    }

    private void MissionEndDisableInputs(MissionEndEnum _)
    {
        InputToggle(false);
    }

    private void Update()
    {
        UpdateInputs();
    }

    private void SetupInputActions()
    {
        //CameraControl
        _playerInput.actions["CameraZoom"].started += ctx => _cameraZoom(ctx);

        //MouseClick
        _playerInput.actions["LeftMouseClick"].performed += _ => _leftMouseClick();
        _playerInput.actions["RightMouseClick"].performed += _ => _rightMouseClick();

        //GameSpeed
        _playerInput.actions["GameSpeedPause"].performed += _ => _gameSpeedPause((int)GameSpeedEnum.Pause);
        _playerInput.actions["GameSpeedDefault"].performed += _ => _gameSpeedDefault((int)GameSpeedEnum.Default);
        _playerInput.actions["GameSpeedDouble"].performed += _ => _gameSpeedDouble((int)GameSpeedEnum.Double);
        _playerInput.actions["GameSpeedTriple"].performed += _ => _gameSpeedTriple((int)GameSpeedEnum.Triple);

        //UserInterface
        _playerInput.actions["Escape"].performed += _ => _escape();

        //SelectTilePanel
        _playerInput.actions["SelectNumbers"].performed += ctx => _selectNumbers(ctx);
        _playerInput.actions["BuildTileButton"].performed += _ => _buildTileButton();
        _playerInput.actions["RotateTileButton"].performed += _ => _rotateTileButton();
        _playerInput.actions["DestroyTileButton"].performed += _ => _destroyTileButton();
        _playerInput.actions["WorkTileButton"].performed += _ => _workTileButton();
        _playerInput.actions["RobotPanelButton"].performed += _ => _robotPanelButton();

    }

    private void SetupDelegates()
    {
        //CameraControl
        _cameraZoom = new CameraZoom(_cameraMovement.ZoomCamera);

        //MouseClick
        _leftMouseClick = new LeftMouseClick(_tileDetector.InputOnTile);
        _rightMouseClick = new RightMouseClick(_uiPanels.ClearAndCancelCardHolderAndTileDetector);

        //GameSpeed
        _gameSpeedPause = new GameSpeedPause(_gameSpeedSystem.InputChangeGameSpeed);
        _gameSpeedDefault = new GameSpeedDefault(_gameSpeedSystem.InputChangeGameSpeed);
        _gameSpeedDouble = new GameSpeedDouble(_gameSpeedSystem.InputChangeGameSpeed);
        _gameSpeedTriple = new GameSpeedTriple(_gameSpeedSystem.InputChangeGameSpeed);

        //UserInterface
        _escape = new Escape(_uiPanels.EscapeClick);

        //SelectTilePanel
        _selectNumbers = new SelectNumbers(OnNumberInput);
        _buildTileButton = new BuildTileButton(_selectTilePanel.BuildButton);
        _rotateTileButton = new RotateTileButton(_selectTilePanel.RotateButton);
        _destroyTileButton = new DestroyTileButton(_selectTilePanel.DestroyButton);
        _workTileButton = new WorkTileButton(_selectTilePanel.ToggleBuildingWorkButton);
        _robotPanelButton = new RobotPanelButton(_selectTilePanel.RobotPanelButton);
    }

    private void OnNumberInput(InputAction.CallbackContext context)
    {
        var key = context.control.displayName; // Получаем нажатую клавишу как строку
        int pressedNumber;
        if (int.TryParse(key, out pressedNumber))
        {
            if(_robotPanel.PanelActive())
            {
                _robotPanel.PlayerInputRobotItemButton(pressedNumber);
            }
            else if (_uiPanels.ActiveInHierarchy(UIPanelsEnum.BuildsPanel))
            {
                _buildsPanel.PlyerInputBuildItemButton(pressedNumber);
            }
            else
            {
                _buildTypesPanel.PlayerInputBuildTypesButton(pressedNumber);
            }

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
        CustomEvents.OnMissionEnd -= MissionEndDisableInputs;

        InputToggle(false);

        _cameraZoom = delegate { };

        //MouseClick
        _leftMouseClick = delegate { };
        _rightMouseClick = delegate { };

        //GameSpeed
        _gameSpeedPause = delegate { };
        _gameSpeedDefault = delegate { };
        _gameSpeedDouble = delegate { };
        _gameSpeedTriple = delegate { };

        //UserInterface
        _escape = delegate { };

        //SelectTilePanel
        _selectNumbers = delegate { };
        _buildTileButton = delegate { };
        _rotateTileButton = delegate { };
        _destroyTileButton = delegate { };
        _workTileButton = delegate { };
        _robotPanelButton = delegate { };
    }
}
