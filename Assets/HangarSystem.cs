using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

public class HangarSystem : MonoBehaviour
{
    [Inject] private readonly HangarSaveGame _hangarSaveGame;

    [SerializeField] private Animator _cameraAnimator;
    [SerializeField] private Animator _manipulatorAnimator;
    [SerializeField] private PanelDoMoveX _hangarPanelDoMoveX;
    [SerializeField] private GameObject _mainButtons;
    [SerializeField] private GameObject _hangarButtons;
    [SerializeField] private ShardsSystem _shardsSystem;

    [Header("Robot")]
    [SerializeField] private GameObject _buyRobotButtonObject;
    [SerializeField] HangarRobotItem[] _hangarRobotItems;
    [SerializeField] private GameObject[] _robotModels;
    [SerializeField] private Material _robotEye;
    [SerializeField] private TextMeshProUGUI _robotPassiveAbility;
    [SerializeField] private int _currentRobot = -1;
    [SerializeField] private int _currentSelectRobot = -1;
    private Coroutine _robotEyeCoroutine;

    public bool EnoughtShards(int price) => _shardsSystem.GetShards() >= price;

    public void LoadHangar(bool[] openedRobots)
    {
        for (int i = 0; i < _hangarRobotItems.Length; i++)
        {
            _hangarRobotItems[i].SetIsOpen(openedRobots[i]);
        }
    }

    public bool[] GetOpenedRobots()
    {
        var openedRobots = new bool[_hangarRobotItems.Length];

        for (int i = 0; i < _hangarRobotItems.Length; i++)
        {
            openedRobots[i] = _hangarRobotItems[i].IsOpen();
        }

        return openedRobots;
    }

    public void OpenHangar()
    {
        SelectRobot(HangarRobotType.Patch, true);

        _cameraAnimator.SetBool(AnimatorStrings.CameraHangarState, true);
        _manipulatorAnimator.SetBool(AnimatorStrings.CameraHangarState, true);
        _hangarPanelDoMoveX.PanelMove();
        ToggleRobotEyes(true);
        _mainButtons.SetActive(false);
        _hangarButtons.SetActive(true);
    }

    public void CloseHangar()
    {
        _cameraAnimator.SetBool(AnimatorStrings.CameraHangarState, false);
        _manipulatorAnimator.SetBool(AnimatorStrings.CameraHangarState, false);
        _hangarPanelDoMoveX.PanelMove();
        ToggleRobotEyes(false);

        _mainButtons.SetActive(true);
        _hangarButtons.SetActive(false);
    }

    public void ToggleRobotEyes(bool state)
    {
        if (state)
        {
            _robotEyeCoroutine = StartCoroutine(nameof(RobotEyeCoroutine));
        }
        else
        {
            StopCoroutine(_robotEyeCoroutine);
            _robotEyeCoroutine = null;
            _robotEye.DisableKeyword("_EMISSION");
        }
    }

    private IEnumerator RobotEyeCoroutine()
    {
        yield return new WaitForSeconds(2.2f);
        _robotEye.EnableKeyword("_EMISSION");
        yield return new WaitForSeconds(0.1f);
        _robotEye.DisableKeyword("_EMISSION");
        yield return new WaitForSeconds(0.1f);
        _robotEye.EnableKeyword("_EMISSION");
    }

    public void SelectRobot(HangarRobotType robotType, bool isOpen)
    {
        for (int i = 0; i < _hangarRobotItems.Length; i++)
        {
            _hangarRobotItems[i].SelectToggleState(false);
        }

        _hangarRobotItems[(int)robotType].SelectToggleState(true);
        _buyRobotButtonObject.SetActive(!isOpen);

        switch (robotType)
        {
            case HangarRobotType.Patch:
                _robotPassiveAbility.text = $"{Language.TextStatic[82]}:\n-{WorldGameInfo.PatchPassiveAbility}% {Language.TextStatic[79]}";
                break;
            case HangarRobotType.Titan:
                _robotPassiveAbility.text = $"{Language.TextStatic[82]}:\n+{WorldGameInfo.TitanPassiveAbility}% {Language.TextStatic[80]}";
                break;
            case HangarRobotType.AimBot:
                _robotPassiveAbility.text = $"{Language.TextStatic[82]}:\n+{WorldGameInfo.AimBotPassiveAbility}% {Language.TextStatic[81]}";
                break;
        }


        if (isOpen)
        {
            foreach (var item in _robotModels)
            {
                item.SetActive(false);
            }
            _currentRobot = (int)robotType;
            _robotModels[(int)robotType].SetActive(true);


        }
        else
        {
            _currentSelectRobot = (int)robotType;
        }
    }

    public void BuyRobot()
    {
        var robotInfo = _hangarRobotItems[_currentSelectRobot].GetInfo();
        if (EnoughtShards(robotInfo.Price))
        {
            AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.LearnBuilding], transform.position);
            _shardsSystem.ChangeShards(-robotInfo.Price);
            _hangarRobotItems[_currentSelectRobot].SetIsOpen(true);
            SelectRobot(robotInfo.HangarRobotType, true);
            _hangarSaveGame.SaveDataToJson();
        }
        else
        {
            AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Error], transform.position);
        }
    }
}
