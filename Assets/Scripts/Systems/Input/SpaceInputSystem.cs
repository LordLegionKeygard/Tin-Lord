using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceInputSystem : MonoBehaviour
{
    public static SpaceInputSystem Instance;
    private PlayerInput _playerInput;

    //UserInterface
    public delegate void Escape(bool emptyEscapeClick);
    private Escape _escape;

    private delegate void SelectNumbers(InputAction.CallbackContext ctx);
    private SelectNumbers _selectNumbers;

    [Header("Links")]
    [SerializeField] private UIPanelsSpace _uiPanels;
    [SerializeField] private EventNodePanel _eventNodePanel;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("More than one instance of InputSystemCommandCenter found!");
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
        _playerInput.actions["Escape"].performed += _ => _escape(false);
        _playerInput.actions["SelectNumbers"].performed += ctx => _selectNumbers(ctx);
    }

    private void SetupDelegates()
    {
        //UserInterface
        _escape = new Escape(_uiPanels.EscapeClick);
        _selectNumbers = new SelectNumbers(OnNumberInput);
    }

    private void OnNumberInput(InputAction.CallbackContext ctx)
    {
        // ctx.control.displayName: "1", "Numpad 3", …
        string key = ctx.control.displayName;

        if (int.TryParse(key, out int pressedNumber))
        {
            // Если активна панель событий — направляем туда
            if (_eventNodePanel.gameObject.activeInHierarchy)
            {
                _eventNodePanel.PlayerInputSelectNumber(pressedNumber);
            }
            // …иначе можно обработать другие панели, если надо
        }
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
        _selectNumbers = delegate { };
    }
}
