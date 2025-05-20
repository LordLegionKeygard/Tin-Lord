using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystemCommandCenter : MonoBehaviour
{
    public static InputSystemCommandCenter Instance;
    private PlayerInput _playerInput;

    //UserInterface
    public delegate void Escape();
    private Escape _escape;

    public delegate void LeftPanel();
    private LeftPanel _leftPanel;

    [Header("Links")]
    [SerializeField] private UIPanelsCommandCenter _uiPanels;

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

    private void SetupInputActions()
    {
        //UserInterface
        _playerInput.actions["Escape"].performed += _ => _escape();
        _playerInput.actions["LeftPanel"].performed += _ => _leftPanel();
    }

    private void SetupDelegates()
    {
        //UserInterface
        _escape = new Escape(_uiPanels.EscapeClick);
        _leftPanel = new LeftPanel(_uiPanels.Click);
    }

    public void InputToggle(bool state)
    {
        if (state) _playerInput.ActivateInput();
        else _playerInput.DeactivateInput();
    }

    private void OnDestroy()
    {
        InputToggle(false);

        //UserInterface
        _escape = delegate { };
        _leftPanel = delegate { };
    }
}
