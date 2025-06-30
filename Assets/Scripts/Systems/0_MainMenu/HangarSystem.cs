using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HangarSystem : MonoBehaviour
{
    [Inject] readonly WorldSaveGame WorldSaveGame;
    [Inject] readonly CommandCenterSaveGame CommandCenterSaveGame;
    [Inject] private readonly HangarSaveGame _hangarSaveGame;

    [SerializeField] private Animator _cameraAnimator;
    [SerializeField] private Animator _manipulatorAnimator;
    [SerializeField] private PanelDoMoveX _hangarPanelDoMoveX;
    [SerializeField] private GameObject _mainButtons;
    [SerializeField] private GameObject _hangarButtons;
    [SerializeField] private GameObject _launchButton;
    [SerializeField] private ShardsSystem _shardsSystem;
    [SerializeField] private GameObject _areYouSurePanel;
    [SerializeField] private Button[] _buttons;
    [SerializeField] private TextMeshProUGUI[] _buttonsText;
    [SerializeField] private ConfigLoaderBuildings _configLoaderBuildings;
    private bool _canLaunch = true;

    [Header("Robot")]
    [SerializeField] private GameObject _buyRobotButtonObject;
    [SerializeField] HangarRobotItem[] _hangarRobotItems;
    [SerializeField] private GameObject[] _robotModels;
    [SerializeField] private Material[] _robotEyes;
    [SerializeField] private TextMeshProUGUI _robotPassiveAbility;
    [SerializeField] private int _currentRobot = -1;
    [SerializeField] private int _currentSelectRobot = -1;
    private Coroutine _robotEyeCoroutine;

    public bool EnoughtShards(int price) => _shardsSystem.GetShards() >= price;
    private bool HaveSaveData() => CommandCenterSaveGame.GetCommandCenterSaveGameDataWriter().CheckIfSaveFileExists();

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
        if(_currentRobot == -1) SelectRobot(HangarRobotType.Patch, true);

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
            _robotEyes[0].DisableKeyword("_EMISSION");
            _robotEyes[1].DisableKeyword("_EMISSION");
        }
    }

    private IEnumerator RobotEyeCoroutine()
    {
        yield return new WaitForSeconds(2.2f);
        _robotEyes[0].EnableKeyword("_EMISSION");
        _robotEyes[1].EnableKeyword("_EMISSION");
        yield return new WaitForSeconds(0.1f);
        _robotEyes[0].DisableKeyword("_EMISSION");
        _robotEyes[1].DisableKeyword("_EMISSION");
        yield return new WaitForSeconds(0.1f);
        _robotEyes[0].EnableKeyword("_EMISSION");
        _robotEyes[1].EnableKeyword("_EMISSION");
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
                _robotPassiveAbility.text = isOpen ? $"{Language.TextStatic[82]}:\n-{WorldGameInfo.PatchPassiveAbility}% {Language.TextStatic[79]}" : $"{Language.TextStatic[82]}:\n{Language.TextStatic[194]}";
                break;
            case HangarRobotType.Titan:
                _robotPassiveAbility.text = isOpen ? $"{Language.TextStatic[82]}:\n+{WorldGameInfo.TitanPassiveAbility}% {Language.TextStatic[80]}" : $"{Language.TextStatic[82]}:\n{Language.TextStatic[194]}";
                break;
            case HangarRobotType.AimBot:
                _robotPassiveAbility.text = isOpen ? $"{Language.TextStatic[82]}:\n+{WorldGameInfo.AimBotPassiveAbility}% {Language.TextStatic[81]}" : $"{Language.TextStatic[82]}:\n{Language.TextStatic[194]}";
                break;
        }

        _currentSelectRobot = (int)robotType;
        _currentRobot = (int)robotType;

        if (isOpen)
        {
            foreach (var item in _robotModels)
            {
                item.SetActive(false);
            }
            _robotModels[(int)robotType].SetActive(true);
        }

        UpdateLaunchButtonActive();
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

    public void UpdateLaunchButtonActive()
    {
        var robotOpened = _hangarRobotItems[_currentRobot].IsOpen();

        _launchButton.SetActive(robotOpened);
    }

    public void LaunchButton()
    {
        if (!_canLaunch) return;

        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);

        if (HaveSaveData())
        {
            _areYouSurePanel.SetActive(true);
            ButtonsToggle(false);
        }
        else
        {
            CustomEvents.FireFade(FadeType.StartFade);
            StartCoroutine(nameof(StartNewGameCoroutine));
        }

    }

    public void AreYouSureYes()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        _canLaunch = false;
        CustomEvents.FireFade(FadeType.StartFade);
        StartCoroutine(nameof(StartNewGameCoroutine));
        _areYouSurePanel.SetActive(false);
        CustomEvents.FireCloseTooltips();
    }

    public void AreYouSureNo()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        _canLaunch = true;
        _areYouSurePanel.SetActive(false);
        ButtonsToggle(true);
        CustomEvents.FireCloseTooltips();
    }

    private IEnumerator StartNewGameCoroutine()
    {
        yield return new WaitForSecondsRealtime(1);
        CreateNewCommandCenterData();
    }

    private void CreateNewCommandCenterData()
    {
        var data = new CommandCenterSaveData
        {
            Quants = 35,
            AiCores = 6,
            HangarCommandCenterData = new HangarCommandCenterData(),
            MainResourcesData = new float[WorldGameInfo.ResourcesCount],
            PrologueCompleted = false,
            TutorialCompleted = false,
            BuildingsLearned = new bool[_configLoaderBuildings.AllBuidingsCount()],
            OpenedSkills = new bool[WorldGameInfo.SkillsCount],
        };

        data.HangarCommandCenterData.Robot = _currentRobot;

        data.BuildingsLearned[20] = true; // WoodManualMining
        data.BuildingsLearned[32] = true; // StoneManualMining
        data.BuildingsLearned[75] = true; // Ballista
        data.BuildingsLearned[0] = true;  // Shelter
        data.BuildingsLearned[20] = true; // WoodManualMining
        data.BuildingsLearned[32] = true; // StoneManualMining
        data.BuildingsLearned[75] = true; // Ballista

        data.MainResourcesData[(int)ResourceEnum.Wood] = 100;
        data.MainResourcesData[(int)ResourceEnum.Stone] = 50;

        data.OpenedSkills[0] = true;


        WorldSaveGame.DeleteMissionJson();
        CommandCenterSaveGame.NewCommandCenterData(data);
    }

    private void ButtonsToggle(bool state)
    {
        foreach (var item in _buttons)
        {
            item.interactable = state;
        }

        foreach (var item in _buttonsText)
        {
            item.color = state == false ? Colors.GreySix : Color.white;
        }
    }
}
