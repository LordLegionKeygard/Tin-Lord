using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HangarSystem : MonoBehaviour
{
    [Inject] readonly MissionSaveGame _missionSaveGame;
    [Inject] readonly SpaceSaveGame _spaceSaveGame;
    [Inject] private readonly HangarSaveGame _hangarSaveGame;

    [SerializeField] private Resource[] _allResources;
    [SerializeField] private Building[] _learnedOnStartBuildings;
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

    [Header("Crates")]
    [SerializeField] private GameObject _buyCrateButtonObject;
    [SerializeField] HangarCrateItem[] _hangarCrateItems;
    [SerializeField] private GameObject[] _crateModels;
    [SerializeField] private HangarCrateResourcesView _hangarCrateResourcesView;
    [SerializeField] private int _currentCrate = -1;
    [SerializeField] private int _currentSelectCrate = -1;

    [Header("Skills")]
    [SerializeField] private GameObject _buySkillButtonObject;
    [SerializeField] HangarSkillItem[] _hangarSkillItems;
    [SerializeField] private TextMeshProUGUI _skillDescription;
    [SerializeField] private int _currentFirstSkill = -1;
    [SerializeField] private int _currentSecondSkill = -1;
    [SerializeField] private int _currentSelectSkillForBuy = -1;

    [Header("ShipWeapons")]
    [SerializeField] private GameObject _buyShipWeaponButtonObject;
    [SerializeField] private HangarShipWeaponItem[] _hangarShipWeaponItems;
    [SerializeField] private TextMeshProUGUI _shipWeaponsDescription;
    [SerializeField] private int _currentLeftShipWeapon = -1;
    [SerializeField] private int _currentRightShipWeapon = -1;
    [SerializeField] private int _currentSelectShipWeaponForBuy = -1;

    [SerializeField] private TextMeshProUGUI _leftShipWeaponNameText;
    [SerializeField] private TextMeshProUGUI _leftShipWeaponDamageText;
    [SerializeField] private TextMeshProUGUI _leftShipWeaponAmmoText;

    [SerializeField] private TextMeshProUGUI _rightShipWeaponNameText;
    [SerializeField] private TextMeshProUGUI _rightShipWeaponDamageText;
    [SerializeField] private TextMeshProUGUI _rightShipWeaponAmmoText;


    public bool EnoughtShards(int price) => _shardsSystem.GetShards() >= price;
    private bool HaveSaveData() => _spaceSaveGame.GetCommandCenterSaveGameDataWriter().CheckIfSaveFileExists();


    public void LoadHangar(HangarSaveData hangarSaveData)
    {
        for (int i = 0; i < _hangarRobotItems.Length; i++)
        {
            _hangarRobotItems[i].SetIsOpen(hangarSaveData.OpenedRobots[i]);
        }

        for (int i = 0; i < _hangarCrateItems.Length; i++)
        {
            _hangarCrateItems[i].SetIsOpen(hangarSaveData.OpenedCrates[i]);
        }
        for (int i = 0; i < _hangarSkillItems.Length; i++)
        {
            _hangarSkillItems[i].SetIsOpen(hangarSaveData.OpenedSkills[i]);
        }

        for (int i = 0; i < _hangarShipWeaponItems.Length; i++)
        {
            if (_hangarShipWeaponItems[i] == null) continue;
            _hangarShipWeaponItems[i].SetIsOpen(hangarSaveData.OpenedShipWeapons[i]);
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

    public bool[] GetOpenedCrates()
    {
        var openedCrates = new bool[_hangarCrateItems.Length];

        for (int i = 0; i < _hangarCrateItems.Length; i++)
        {
            openedCrates[i] = _hangarCrateItems[i].IsOpen();
        }

        return openedCrates;
    }

    public bool[] GetOpenedSkills()
    {
        var openedSkills = new bool[_hangarSkillItems.Length];

        for (int i = 0; i < _hangarSkillItems.Length; i++)
        {
            openedSkills[i] = _hangarSkillItems[i].IsOpen();
        }

        return openedSkills;
    }

    public bool[] GetOpenedShipWeapons()
    {
        var openedWeapons = new bool[_hangarShipWeaponItems.Length];

        for (int i = 0; i < _hangarShipWeaponItems.Length; i++)
        {
            if (_hangarShipWeaponItems[i] == null) openedWeapons[i] = false;
            else
            {
                openedWeapons[i] = _hangarShipWeaponItems[i].IsOpen();
            }
        }

        return openedWeapons;
    }

    public void OpenHangar()
    {
        if (_currentRobot == -1) SelectRobot(HangarRobotType.Arbalester, true);
        if (_currentCrate == -1) SelectCrate(HangarCrateType.BaseCrate, true);
        if (_currentFirstSkill == -1) SelectSkill(SkillEnum.GeneralRepair, true);

        if (_currentLeftShipWeapon == -1) SelectShipWeapon(ShipWeaponEnum.Left_SteelRiffle_0, true, true);

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
            case HangarRobotType.Arbalester:
                _robotPassiveAbility.text = isOpen ? $"{Language.TextStatic[82]}:\n-{WorldGameInfo.PatchPassiveAbility}% {Language.TextStatic[79]}" : $"{Language.TextStatic[82]}:\n{Language.TextStatic[194]}";
                break;
            case HangarRobotType.Titan:
                _robotPassiveAbility.text = isOpen ? $"{Language.TextStatic[82]}:\n+{WorldGameInfo.TitanPassiveAbility}% {Language.TextStatic[80]}" : $"{Language.TextStatic[82]}:\n{Language.TextStatic[194]}";
                break;
            case HangarRobotType.Sniper:
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

    public void SelectCrate(HangarCrateType crateType, bool isOpen)
    {
        for (int i = 0; i < _hangarCrateItems.Length; i++)
        {
            _hangarCrateItems[i].SelectToggleState(false);
        }

        _hangarCrateItems[(int)crateType].SelectToggleState(true);
        _buyCrateButtonObject.SetActive(!isOpen);

        _hangarCrateResourcesView.SetResources(_hangarCrateItems[(int)crateType].GetInfo().ResourceWrapper, isOpen);

        _currentSelectCrate = (int)crateType;
        _currentCrate = (int)crateType;

        if (isOpen)
        {
            foreach (var item in _crateModels)
            {
                item.SetActive(false);
            }
            _crateModels[(int)crateType].SetActive(true);
        }

        UpdateLaunchButtonActive();
    }

    public void SelectSkill(SkillEnum skillType, bool isOpen)
    {
        for (int i = 0; i < _hangarSkillItems.Length; i++)
        {
            _hangarSkillItems[i].SelectToggleState(false, -1);
        }

        _buySkillButtonObject.SetActive(!isOpen);

        if (isOpen)
        {
            if (_currentFirstSkill == -1)
            {
                _currentFirstSkill = (int)skillType;
            }
            else
            {
                _currentSecondSkill = (int)skillType;
            }
            if (_currentFirstSkill != -1) _hangarSkillItems[_currentFirstSkill].SelectToggleState(true, 0);
            if (_currentSecondSkill != -1) _hangarSkillItems[_currentSecondSkill].SelectToggleState(true, 1);
        }
        else
        {
            _currentFirstSkill = -1;
            _currentSecondSkill = -1;
            _currentSelectSkillForBuy = (int)skillType;
            _hangarSkillItems[_currentSelectSkillForBuy].SelectToggleState(true, -1);
        }
        UpdateLaunchButtonActive();
    }

    public void SetSkillDescription(SkillEnum skillType, bool isOpen, bool isExit)
    {
        if (isExit && _currentFirstSkill == -1 && _currentSecondSkill == -1)
        {
            _skillDescription.text = Language.TextStatic[221];
        }
        else
        {
            var info = _hangarSkillItems[(int)skillType].GetInfo();
            _skillDescription.text = isOpen ? $"{Language.TextStatic[info.NameNumber]} - {Language.TextStatic[info.InfoNumber]}" : $"{Language.TextStatic[298]} {Language.TextStatic[194]}";
        }
    }

    public void UnselectSkill(int selectSkillIndex)
    {
        if (selectSkillIndex == -1) // не купленный
        {
            _hangarSkillItems[_currentSelectSkillForBuy].SelectToggleState(false, -1);
            _currentSelectSkillForBuy = -1;
        }
        else if (selectSkillIndex == 0)
        {
            _hangarSkillItems[_currentFirstSkill].SelectToggleState(false, -1);
            _currentFirstSkill = -1;
        }
        else
        {
            _hangarSkillItems[_currentSecondSkill].SelectToggleState(false, -1);
            _currentSecondSkill = -1;
        }

        if (_currentFirstSkill == -1 && _currentSecondSkill == -1)
        {
            _skillDescription.text = Language.TextStatic[221];
        }


        _buySkillButtonObject.SetActive(false);
        UpdateLaunchButtonActive();
    }

    private void SetShipWeaponTexts()
    {
        if (_currentLeftShipWeapon == -1 && _currentRightShipWeapon == -1)
        {
            _shipWeaponsDescription.text = Language.TextStatic[87];
        }
        else
        {
            _shipWeaponsDescription.text = "-";
        }


        _leftShipWeaponNameText.text = _currentLeftShipWeapon == -1 ? $"{Language.TextStatic[88]}: -" : $"{Language.TextStatic[88]}: {Language.TextStatic[_hangarShipWeaponItems[_currentLeftShipWeapon].GetInfo().NameNumber]}";
        _rightShipWeaponNameText.text = _currentRightShipWeapon == -1 ? $"{Language.TextStatic[89]}: -" : $"{Language.TextStatic[89]}: {Language.TextStatic[_hangarShipWeaponItems[_currentRightShipWeapon].GetInfo().NameNumber]}";

        _leftShipWeaponDamageText.text = _currentLeftShipWeapon == -1 ? $"{Language.TextStatic[98]}: -" : $"{Language.TextStatic[98]}: {_hangarShipWeaponItems[_currentLeftShipWeapon].GetInfo().Damage}";
        _rightShipWeaponDamageText.text = _currentRightShipWeapon == -1 ? $"{Language.TextStatic[98]}: -" : $"{Language.TextStatic[98]}: {_hangarShipWeaponItems[_currentRightShipWeapon].GetInfo().Damage}";

        _leftShipWeaponAmmoText.text = _currentLeftShipWeapon == -1 ? $"{Language.TextStatic[230]}: -" : $"{Language.TextStatic[230]}: {_hangarShipWeaponItems[_currentLeftShipWeapon].GetInfo().BulletsCount}";
        _rightShipWeaponAmmoText.text = _currentRightShipWeapon == -1 ? $"{Language.TextStatic[230]}: -" : $"{Language.TextStatic[230]}: {_hangarShipWeaponItems[_currentRightShipWeapon].GetInfo().BulletsCount}";
    }

    public void UnselectShipWeapon(int selectShipWeaponIndex)
    {
        if (selectShipWeaponIndex == -1) // не купленный
        {
            _hangarShipWeaponItems[_currentSelectShipWeaponForBuy].SelectToggleState(false, -1);
            _currentSelectShipWeaponForBuy = -1;
        }
        else if (selectShipWeaponIndex == 0)
        {
            _hangarShipWeaponItems[_currentLeftShipWeapon].SelectToggleState(false, -1);
            _currentLeftShipWeapon = -1;
        }
        else
        {
            _hangarShipWeaponItems[_currentRightShipWeapon].SelectToggleState(false, -1);
            _currentRightShipWeapon = -1;
        }

        _buyShipWeaponButtonObject.SetActive(false);
        SetShipWeaponTexts();
        UpdateLaunchButtonActive();
    }

    public void SelectShipWeapon(ShipWeaponEnum shipWeaponEnum, bool isOpen, bool isLeft)
    {
        for (int i = 0; i < _hangarShipWeaponItems.Length; i++)
        {
            if (_hangarShipWeaponItems[i] == null) continue;
            _hangarShipWeaponItems[i].SelectToggleState(false, -1);
        }

        _buyShipWeaponButtonObject.SetActive(!isOpen);

        if (isOpen)
        {
            if (isLeft)
            {
                _currentLeftShipWeapon = (int)shipWeaponEnum;
            }
            else
            {
                _currentRightShipWeapon = (int)shipWeaponEnum;
            }

            if (_currentLeftShipWeapon != -1) _hangarShipWeaponItems[_currentLeftShipWeapon].SelectToggleState(true, 0);
            if (_currentRightShipWeapon != -1) _hangarShipWeaponItems[_currentRightShipWeapon].SelectToggleState(true, 1);
        }
        else
        {
            _currentLeftShipWeapon = -1;
            _currentRightShipWeapon = -1;
            _currentSelectShipWeaponForBuy = (int)shipWeaponEnum;
            _hangarShipWeaponItems[_currentSelectShipWeaponForBuy].SelectToggleState(true, -1);
        }

        SetShipWeaponTexts();
        UpdateLaunchButtonActive();
    }

    public void BuyRobot()
    {
        var robotInfo = _hangarRobotItems[_currentSelectRobot].GetInfo();
        if (EnoughtShards(robotInfo.Price))
        {
            AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Buy], transform.position);
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

    public void BuyCrate()
    {
        var crateInfo = _hangarCrateItems[_currentSelectCrate].GetInfo();
        if (EnoughtShards(crateInfo.Price))
        {
            AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Buy], transform.position);
            _shardsSystem.ChangeShards(-crateInfo.Price);
            _hangarCrateItems[_currentSelectCrate].SetIsOpen(true);
            SelectCrate(crateInfo.HangarCrateType, true);
            _hangarSaveGame.SaveDataToJson();
        }
        else
        {
            AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Error], transform.position);
        }
    }

    public void BuySkill()
    {
        var skillInfo = _hangarSkillItems[_currentSelectSkillForBuy].GetInfo();
        if (EnoughtShards(skillInfo.ShardPrice))
        {
            AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Buy], transform.position);
            _shardsSystem.ChangeShards(-skillInfo.ShardPrice);
            _hangarSkillItems[_currentSelectSkillForBuy].SetIsOpen(true);
            SelectSkill(skillInfo.SkillEnum, true);
            _hangarSaveGame.SaveDataToJson();
        }
        else
        {
            AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Error], transform.position);
        }
    }

    public void BuyShipWeapon()
    {
        var shipWeaponInfo = _hangarShipWeaponItems[_currentSelectShipWeaponForBuy].GetInfo();
        if (EnoughtShards(shipWeaponInfo.ShardPrice))
        {
            AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Buy], transform.position);
            _shardsSystem.ChangeShards(-shipWeaponInfo.ShardPrice);
            _hangarShipWeaponItems[_currentSelectShipWeaponForBuy].SetIsOpen(true);
            SelectShipWeapon(shipWeaponInfo.ShipWeaponEnum, true, shipWeaponInfo.IsLeft);
            _hangarSaveGame.SaveDataToJson();
        }
        else
        {
            AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Error], transform.position);
        }
    }

    public void UpdateLaunchButtonActive()
    {
        if (_currentCrate == -1 || _currentRobot == -1) return;

        var robotSelect = _hangarRobotItems[_currentRobot].IsOpen();
        var crateSelect = _hangarCrateItems[_currentCrate].IsOpen();

        var firstSkillSelect = _currentFirstSkill != -1 ? _hangarSkillItems[_currentFirstSkill].IsOpen() : false;
        var secondSkillSelect = _currentSecondSkill != -1 ? _hangarSkillItems[_currentSecondSkill].IsOpen() : false;

        var leftShipWeaponSelect = _currentLeftShipWeapon != -1 ? _hangarShipWeaponItems[_currentLeftShipWeapon].IsOpen() : false;
        var rightShipWeaponSelect = _currentRightShipWeapon != -1 ? _hangarShipWeaponItems[_currentRightShipWeapon].IsOpen() : false;

        _launchButton.SetActive(robotSelect && crateSelect && (firstSkillSelect || secondSkillSelect) && (leftShipWeaponSelect || rightShipWeaponSelect));
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
        var data = new SpaceSaveData
        {
            Act = 0,
            Quants = 35,
            AiCores = 6,
            PreviousActsShards = 0,
            HangarCommandCenterData = new HangarCommandCenterData(),

            PrologueCompleted = false,
            BuildingsLearned = new bool[_configLoaderBuildings.AllBuidingsCount()],
        };

        data.HangarCommandCenterData.Robot = _currentRobot;
        data.HangarCommandCenterData.MainResourcesData = new float[_allResources.Length];
        data.HangarCommandCenterData.OpenedSkills = new bool[WorldGameInfo.SkillsCount];
        data.HangarCommandCenterData.WeaponData = new WeaponData()
        {
            LeftWeapon = _currentLeftShipWeapon,
            RightWeapon = _currentRightShipWeapon,
            LeftWeaponLevel = 1,
            RightWeaponLevel = 1
        };

        foreach (var wrapper in _hangarCrateItems[_currentCrate].GetInfo().ResourceWrapper)
        {
            int index = (int)wrapper.ResourceEnum;
            data.HangarCommandCenterData.MainResourcesData[index] = wrapper.RecourceAmount;
        }

        for (int i = 0; i < _learnedOnStartBuildings.Length; i++)
        {
            data.BuildingsLearned[_learnedOnStartBuildings[i].Id] = true;
        }

        if (_currentFirstSkill != -1 && _hangarSkillItems[_currentFirstSkill].IsOpen()) data.HangarCommandCenterData.OpenedSkills[_currentFirstSkill] = true;
        if (_currentSecondSkill != -1 && _hangarSkillItems[_currentSecondSkill].IsOpen()) data.HangarCommandCenterData.OpenedSkills[_currentSecondSkill] = true;


        _missionSaveGame.DeleteMissionJson();
        _spaceSaveGame.NewCommandCenterData(data);
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
