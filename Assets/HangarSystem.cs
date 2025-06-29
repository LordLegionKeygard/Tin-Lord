using TMPro;
using UnityEngine;

public class HangarSystem : MonoBehaviour
{
    [SerializeField] private Animator _cameraAnimator;
    [SerializeField] private PanelDoMoveX _hangarPanelDoMoveX;
    [SerializeField] private GameObject _mainButtons;
    [SerializeField] private GameObject _hangarButtons;

    [Header("Robot")]
    [SerializeField] private GameObject[] _robotModels;
    [SerializeField] private Material _robotEye;
    [SerializeField] HangarRobotItem[] _hangarRobotItems;
    [SerializeField] private TextMeshProUGUI _robotPassiveAbility;
    [SerializeField] private int _currentRobot = -1;

    public void OpenHangar()
    {
        SelectRobot(HangarRobotType.Patch);

        _cameraAnimator.SetBool(AnimatorStrings.CameraHangarState, true);
        _hangarPanelDoMoveX.PanelMove();
        _robotEye.EnableKeyword("_EMISSION");
        _mainButtons.SetActive(false);
        _hangarButtons.SetActive(true);
    }

    public void CloseHangar()
    {
        _cameraAnimator.SetBool(AnimatorStrings.CameraHangarState, false);
        _hangarPanelDoMoveX.PanelMove();
        _robotEye.DisableKeyword("_EMISSION");
        _mainButtons.SetActive(true);
        _hangarButtons.SetActive(false);
    }

    public void SelectRobot(HangarRobotType robotType)
    {
        for (int i = 0; i < _hangarRobotItems.Length; i++)
        {
            _hangarRobotItems[i].SelectToggleState(false);
        }

        foreach (var item in _robotModels)
        {
            item.SetActive(false);
        }

        _currentRobot = (int)robotType;
        _hangarRobotItems[(int)robotType].SelectToggleState(true);
        _robotModels[(int)robotType].SetActive(true);

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
    }
}
