using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
public class MissionInputSystem : MonoBehaviour
{
    [Inject] private readonly MissionModeSystem _missionModeSystem;
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
    private delegate void MiddleMouseClick();
    private MiddleMouseClick _middleMouseClick;

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
    public delegate void ResourcePanel(bool state);
    private ResourcePanel _resourcePanel;
    public delegate void ChangeMode();
    private ChangeMode _changeMode;

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
    public delegate void MachinePanelButton();
    private MachinePanelButton _machinePanelButton;
    public delegate void ToggleGeneralRepairButton();
    private ToggleGeneralRepairButton _toggleGeneralRepairButton;

    //Skills
    public delegate void ToggleSkillPanel();
    public ToggleSkillPanel _toggleSkillPanel;


    public delegate void SkillZero();
    private SkillZero _skillZero;
    public delegate void SkillOne();
    private SkillOne _skillOne;
    public delegate void SkillTwo();
    private SkillTwo _skillTwo;


    [Header("Links")]
    [SerializeField] private SkillTargetSystem _skillTargetSystem;
    [SerializeField] private TileDetector _tileDetector;
    [SerializeField] private CameraMovement _cameraMovement;
    [SerializeField] private GameSpeedSystem _gameSpeedSystem;
    [SerializeField] private SelectTilePanel _selectTilePanel;
    [SerializeField] private BuildTypesPanel _buildTypesPanel;
    [SerializeField] private BuildsPanel _buildsPanel;
    [SerializeField] private UIPanelsMission _uiPanels;
    [SerializeField] private MissionResourcePanel _resourcesPanel;
    [SerializeField] private MachinePanel _machinePanel;
    [SerializeField] private MissionHolderPanel _missionHolderPanel;
    [SerializeField] private BaseSkill[] _skills;
    [SerializeField] private ShipCannonAimer _leftShipCannon;
    [SerializeField] private ShipCannonAimer _rightShipCannon;

    private void Awake()
    {
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
        _playerInput.actions["MiddleMouseClick"].performed += _ => _middleMouseClick();

        //GameSpeed
        _playerInput.actions["GameSpeedPause"].performed += _ => _gameSpeedPause((int)GameSpeedEnum.Pause);
        _playerInput.actions["GameSpeedDefault"].performed += _ => _gameSpeedDefault((int)GameSpeedEnum.Default);
        _playerInput.actions["GameSpeedDouble"].performed += _ => _gameSpeedDouble((int)GameSpeedEnum.Double);
        _playerInput.actions["GameSpeedTriple"].performed += _ => _gameSpeedTriple((int)GameSpeedEnum.Triple);

        //UserInterface
        _playerInput.actions["Escape"].performed += _ => _escape();
        _playerInput.actions["ResourcePanel"].performed += _ => _resourcePanel(true);
        _playerInput.actions["ChangeMode"].performed += _ => _changeMode();

        //SelectTilePanel
        _playerInput.actions["SelectNumbers"].performed += ctx => _selectNumbers(ctx);
        _playerInput.actions["BuildTileButton"].performed += _ => _buildTileButton();
        _playerInput.actions["RotateTileButton"].performed += _ => _rotateTileButton();
        _playerInput.actions["DestroyTileButton"].performed += _ => _destroyTileButton();
        _playerInput.actions["WorkTileButton"].performed += _ => _workTileButton();
        _playerInput.actions["MachinePanelButton"].performed += _ => _machinePanelButton();
        _playerInput.actions["ToggleGeneralRepairButton"].performed += _ => _toggleGeneralRepairButton();
        _playerInput.actions["ToggleSkillPanel"].performed += _ => _toggleSkillPanel();
        _playerInput.actions["Skill_0"].performed += _ => _skillZero();
        _playerInput.actions["Skill_1"].performed += _ => _skillOne();
        _playerInput.actions["Skill_2"].performed += _ => _skillTwo();
    }

    private void SetupDelegates()
    {
        //CameraControl
        _cameraZoom = new CameraZoom(_cameraMovement.ZoomCamera);

        //MouseClick
        _leftMouseClick = new LeftMouseClick(_tileDetector.InputOnTile);
        _rightMouseClick = new RightMouseClick(_uiPanels.ClearAndCancelCardHolderAndTileDetector);
        _middleMouseClick = new MiddleMouseClick(_skillTargetSystem.CancelSkillCircle);

        //GameSpeed
        _gameSpeedPause = new GameSpeedPause(_gameSpeedSystem.InputChangeGameSpeed);
        _gameSpeedDefault = new GameSpeedDefault(_gameSpeedSystem.InputChangeGameSpeed);
        _gameSpeedDouble = new GameSpeedDouble(_gameSpeedSystem.InputChangeGameSpeed);
        _gameSpeedTriple = new GameSpeedTriple(_gameSpeedSystem.InputChangeGameSpeed);

        //UserInterface
        _escape = new Escape(_uiPanels.EscapeClick);
        _resourcePanel = new ResourcePanel(_resourcesPanel.PanelMove);
        _changeMode = new ChangeMode(_missionModeSystem.ChangeMode);

        //SelectTilePanel
        _selectNumbers = new SelectNumbers(OnNumberInput);
        _buildTileButton = new BuildTileButton(_selectTilePanel.BuildButton);
        _rotateTileButton = new RotateTileButton(_selectTilePanel.RotateButton);
        _destroyTileButton = new DestroyTileButton(_uiPanels.InputDestroyButton);
        _workTileButton = new WorkTileButton(_selectTilePanel.ToggleBuildingWorkButton);
        _machinePanelButton = new MachinePanelButton(_selectTilePanel.MachinePanelButton);
        _toggleGeneralRepairButton = new ToggleGeneralRepairButton(_selectTilePanel.ToggleGeneralRepairButton);
        _toggleSkillPanel = new ToggleSkillPanel(_missionHolderPanel.PanelMove);
        _skillZero = new SkillZero(_skills[0].UseSkill);
        _skillOne = new SkillOne(_skills[1].UseSkill);
        _skillTwo = new SkillTwo(_skills[2].UseSkill);
    }

    private void OnNumberInput(InputAction.CallbackContext context)
    {
        var key = context.control.displayName; // Получаем нажатую клавишу как строку

        if (int.TryParse(key, out int pressedNumber))
        {
            if (_machinePanel.PanelActive())
            {
                _machinePanel.PlayerInputMachineItemButton(pressedNumber);
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

        if (!_missionModeSystem.IsPlanetMode() && _playerInput.actions["LeftMouseClick"].IsPressed() && !IsPointerOverUISystem.IsPointerOverUI)
        {
            _leftShipCannon.TryFireHold();
        }

        if (!_missionModeSystem.IsPlanetMode() && _playerInput.actions["RightMouseClick"].IsPressed() && !IsPointerOverUISystem.IsPointerOverUI)
        {
            _rightShipCannon.TryFireHold();
        }

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
        _middleMouseClick = delegate { };

        //GameSpeed
        _gameSpeedPause = delegate { };
        _gameSpeedDefault = delegate { };
        _gameSpeedDouble = delegate { };
        _gameSpeedTriple = delegate { };

        //UserInterface
        _escape = delegate { };
        _resourcePanel = delegate { };

        //SelectTilePanel
        _selectNumbers = delegate { };
        _buildTileButton = delegate { };
        _rotateTileButton = delegate { };
        _destroyTileButton = delegate { };
        _workTileButton = delegate { };
        _machinePanelButton = delegate { };
        _toggleGeneralRepairButton = delegate { };

        //Skills
        _toggleSkillPanel = delegate { };
        _skillZero = delegate { };
        _skillOne = delegate { };
        _skillTwo = delegate { };
    }
}
