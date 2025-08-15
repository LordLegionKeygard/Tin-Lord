using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class MissionSaveLoad : MonoBehaviour
{
    [Inject] private readonly EndMissionSystem _endMissionSystem;
    [Inject] private readonly HangarSaveGame _hangarSaveGame;
    [Inject] private readonly SpaceSaveGame _spaceSaveGame;
    [Inject] private MissionSaveGame _missionSaveGame;
    [Inject] private MissionResources _missionResources;
    [Inject] private readonly TutorialSystem _tutorialSystem;

    [Header("Main")]
    [SerializeField] private AllNodesInfo _allMissionsInfo;
    [SerializeField] private TileMapBuilder _tileMapBuilder;

    [Header("UpPanel")]
    [SerializeField] private EcologySystem _ecologySystem;
    [SerializeField] private TimeTickSystem _timeTickSystem;
    [SerializeField] private GameSpeedSystem _gameSpeedSystem;


    [Header("Cards")]
    [SerializeField] private CardHolderSystem _cardHolderSystem;

    [Header("WorldEvents")]
    [SerializeField] private MissionEventSystem _missionEventSystem;

    [Header("Enemies")]
    [SerializeField] private EnemiesSpawnerSystem _enemiesSpawnerSystem;

    [Header("Tiles")]
    [Inject] private readonly TilesSystem _tilesSystem;
    [SerializeField] private AllTileObjects _allTileObjects;

    [Header("Robot")]
    [SerializeField] private CurrentMachineSystem _currentMachineSystem;
    [SerializeField] private MachineSpawnerSystem _machineSpawnerSystem;

    [Header("LearnedBuildings")]
    [SerializeField] private LearnedBuildingsDataMission _learnedBuildingsDataMission;

    [Header("Objectives")]
    [SerializeField] private ObjectivesPanel _objectivesPanel;

    [Header("ShipCannons")]
    [SerializeField] private MissionModeSystem _missionModeSystem;
    [SerializeField] private MissionShipWeaponSystem _missionShipWeaponSystem;
    [SerializeField] private MissionShipWeaponSystem _missionWeaponSetterSystem;

    [Header("Skill")]
    [SerializeField] private AllSkills _allSkills;

    [Header("Quants")]
    [SerializeField] private QuantPickupPool _quantPool;
    [SerializeField] private MissionQuantSystem _missionQuantSystem;

    [Header("Hangar")]
    [SerializeField] private MissionHangarSystem _missionHangarSystem;

    [Header("Hazards")]
    [SerializeField] private SpawnedHazardSystem _spawnedHazardSystem;

    private void Awake()
    {
        _missionSaveGame.MissionSaveLoad = this;
    }

    private void Start()
    {
        CustomEvents.OnDayEnd += AutoSave;
    }

    public void ResetMissionData(ref MissionSaveData currentSaveData)
    {
        currentSaveData = new MissionSaveData
        {
            IsStartMission = true,
            GameSpeed = (int)GameSpeedEnum.Default,
            ResourcesData = new float[Enum.GetValues(typeof(ResourceEnum)).Length - 1],
        };

        for (int i = 0; i < _spaceSaveGame.SpaceSaveData.HangarCommandCenterData.MainResourcesData.Length; i++)
        {
            currentSaveData.ResourcesData[i] = _spaceSaveGame.SpaceSaveData.HangarCommandCenterData.MainResourcesData[i];
        }

        currentSaveData.ResourcesData[(int)ResourceEnum.DataFragment] = 0;
    }

    public void SaveMissionData(ref MissionSaveData currentSaveData)
    {
        //Main
        currentSaveData.IsStartMission = false;

        //UpPanel
        currentSaveData.Day = _timeTickSystem.GetCurrentDay();
        currentSaveData.Tick = _timeTickSystem.GetCurrentTick();
        currentSaveData.Radiation = _ecologySystem.GetRadiation();
        currentSaveData.EveryDayEcology = _ecologySystem.GetEveryDayEcology();
        currentSaveData.GameSpeed = (int)GameSpeedEnum.Pause;

        //Resources
        currentSaveData.ResourcesData = _missionResources.GetAllResourcesAmount();

        //Cards
        currentSaveData.Cards = _cardHolderSystem.GetAllCards();

        //DayEvents
        currentSaveData.DayEventsData = _missionEventSystem.GetAllCurrentEvents();

        //Enemies
        currentSaveData.EnemyData = _enemiesSpawnerSystem.GetAllCurrentEnemies();

        //Tiles
        currentSaveData.BaseLevel = _tilesSystem.GetBaseLevel();
        currentSaveData.IsHaveRiver = _tilesSystem.IsHaveRiver();
        currentSaveData.IsHaveMachineProduction = _tilesSystem.IsHaveMachineProduction();
        currentSaveData.TilesData = _allTileObjects.GetAllTileObjects();
        currentSaveData.RoadTilesId = _tileMapBuilder.GetRoadTilesId();

        //Robot
        currentSaveData.MachineData = _currentMachineSystem.GetMachineData();

        //Objectives
        currentSaveData.ObjectiveAmount = _objectivesPanel.GetAllObjectivesAmount();

        //ShipCannons
        currentSaveData.ShipCannonsData.IsWeaponMode = !_missionModeSystem.IsPlanetMode();
        currentSaveData.ShipCannonsData.LeftWeaponBulletsCount = _missionShipWeaponSystem.GetCurrentLeftShipWeaponBulletsCount();
        currentSaveData.ShipCannonsData.RightWeaponBulletsCount = _missionShipWeaponSystem.GetCurrentRightShipWeaponBulletsCount();

        //Skills
        currentSaveData.SkillsCooldown = _allSkills.GetAllSkillsCooldown();
        currentSaveData.SkillsDuration = _allSkills.GetAllSkillsDuration();

        //Quants
        currentSaveData.QuantsAmount = _missionQuantSystem.GetQuants();
        currentSaveData.QuantPickups = _quantPool.GetActiveQuants();

        //Hazard
        currentSaveData.Hazards = _spawnedHazardSystem.GetHazards();
    }

    public void LoadGameData(ref MissionSaveData currentSaveData)
    {
        // Main
        CurrentMissionInfo.Instance.LoadMission(BuildMissionFromSelected(), _spaceSaveGame.SpaceSaveData.CurrentMission.MissionDeckIndex);
        _tileMapBuilder.BuildMap(currentSaveData.IsStartMission);

        //UpPanel
        _timeTickSystem.LoadTime(currentSaveData.Day, currentSaveData.Tick);
        _ecologySystem.LoadEcology(currentSaveData.Radiation, currentSaveData.EveryDayEcology, currentSaveData.IsStartMission);
        _gameSpeedSystem.ChangeGameSpeed(currentSaveData.GameSpeed, false);

        //Resources
        _missionResources.LoadResources(currentSaveData.ResourcesData);

        //Cards
        _cardHolderSystem.LoadCards(currentSaveData.IsStartMission, currentSaveData.Cards);

        //DayEvents
        _missionEventSystem.LoadEvents(currentSaveData.DayEventsData, currentSaveData.IsStartMission);

        //Enemies
        _enemiesSpawnerSystem.LoadEnemies(currentSaveData.EnemyData, currentSaveData.IsStartMission);

        //Tiles
        _tilesSystem.SetBaseLevel(currentSaveData.BaseLevel);
        _tilesSystem.SetIsHaveRiver(currentSaveData.IsHaveRiver);
        _tilesSystem.SetIsHaveMachineProduction(currentSaveData.IsHaveMachineProduction);
        _allTileObjects.LoadTiles(currentSaveData.TilesData, currentSaveData.IsStartMission);
        _tileMapBuilder.LoadRoadTiles(currentSaveData.RoadTilesId, currentSaveData.IsStartMission);

        //Machine
        _machineSpawnerSystem.LoadSpawnMachine(currentSaveData);

        //Buildings
        _learnedBuildingsDataMission.LoadLearnedBuildings(_spaceSaveGame.SpaceSaveData.BuildingsLearned);

        //Objectives
        _objectivesPanel.LoadObjectiveItems(currentSaveData.ObjectiveAmount, currentSaveData.IsStartMission);

        //ShipCannons
        _missionModeSystem.LoadMode(currentSaveData.ShipCannonsData.IsWeaponMode);
        _missionWeaponSetterSystem.LoadWeapons(_spaceSaveGame.SpaceSaveData.HangarCommandCenterData.WeaponData, currentSaveData.ShipCannonsData, currentSaveData.IsStartMission);

        //Skills
        _allSkills.LoadAllSkills(currentSaveData.SkillsCooldown, currentSaveData.SkillsDuration, _spaceSaveGame.SpaceSaveData.HangarCommandCenterData.OpenedSkills);

        //Quants
        _missionQuantSystem.SetQuants(currentSaveData.QuantsAmount);
        _quantPool.LoadQuantPickup(currentSaveData.QuantPickups);

        //Hangar
        _missionHangarSystem.LoadHangarData(_spaceSaveGame.SpaceSaveData.HangarCommandCenterData);

        //Hazard
        _spawnedHazardSystem.LoadHazardData(currentSaveData.Hazards, currentSaveData.IsStartMission);

        // Tutorial
        _tutorialSystem.LoadTutorial(_hangarSaveGame.HangarSaveData.TutorialProgress, _spaceSaveGame.SpaceSaveData.PrologueCompleted);

        CustomEvents.FireDataLoad();
    }

    //  Собирает MissionNode из SelectedMissionData
    private MissionNode BuildMissionFromSelected()
    {
        SelectedMissionData sel = _spaceSaveGame.SpaceSaveData.CurrentMission;
        if (sel == null) return null;

        var definition = _allMissionsInfo.MissionDeck[sel.MissionDeckIndex];
        var template = _allMissionsInfo.MissionNodeTemplate;
        var landscape = _allMissionsInfo.Landscapes[sel.LandscapeId];
        var spawnerSO = definition.Spawner;

        var wrappers = new ObjectiveWrapper[sel.SavedObjectives.Length];
        for (int i = 0; i < wrappers.Length; i++)
        {
            wrappers[i] = new ObjectiveWrapper
            {
                ObjectiveEnum = sel.SavedObjectives[i].Objective,
                ObjectiveAmount = sel.SavedObjectives[i].Amount
            };
        }
        var objectiveSO = ScriptableObject.CreateInstance<Objective>();
        objectiveSO.Objectives = wrappers;

        var node = ScriptableObject.CreateInstance<MissionNode>();
        node.Landscape = landscape;
        node.Objective = objectiveSO;
        node.EnemiesSpawner = spawnerSO;

        node.Icon = template.Icon;
        node.IconColor = template.IconColor;
        node.IconWidth = template.IconWidth;
        node.IconHeight = template.IconHeight;
        node.DescriptionTextNumber = template.DescriptionTextNumber;
        node.CosmosVariations = landscape.CosmosVariations;

        return node;
    }

    public void PrepareRestartMission()
    {
        StartCoroutine(nameof(PrepareRestartMissionCoroutine));
    }

    public void PrepareSaveMission()
    {
        StartCoroutine(nameof(PrepareSaveMissionCoroutine));
    }


    private IEnumerator PrepareRestartMissionCoroutine()
    {
        yield return new WaitForSecondsRealtime(1);
        _missionSaveGame.ResetMissionJson();
        CustomEvents.FireLoadScene(SceneEnum.Mission, WorldGameInfo.LoadSceneTime, CurrentMissionInfo.Instance.GetCurrentLandscape().LoadingScreenSprite);
    }

    private IEnumerator PrepareSaveMissionCoroutine()
    {
        yield return new WaitForSecondsRealtime(1);
        _missionSaveGame.SaveMissionToJson();
        CustomEvents.FireLoadScene(SceneEnum.Space, WorldGameInfo.LoadSceneTime, null);
    }

    public void AutoSave(int day)
    {
        if (_endMissionSystem.IsMissionEnd() || _tutorialSystem.GetTutorialStepEnum() < TutorialStepEnum.MissionGoodLuckDescription_66) return;
        _missionSaveGame.SaveMissionToJson();
    }

    private void OnDestroy()
    {
        CustomEvents.OnDayEnd -= AutoSave;
    }
}
