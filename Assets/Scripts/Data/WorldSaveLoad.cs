using System;
using UnityEngine;
using Zenject;

public class WorldSaveLoad : MonoBehaviour
{
    [Inject] private readonly CommandCenterSaveGame _commandCenterSaveGame;
    [Inject] private WorldSaveGame _worldSaveGame;

    [Header("Main")]
    [SerializeField] private TileMapBuilder _tileMapBuilder;

    [Header("UpPanel")]
    [SerializeField] private TimeTickSystem _timeTickSystem;
    [SerializeField] private EcologySystem _ecologySystem;
    [SerializeField] private GameSpeedSystem _gameSpeedSystem;

    [Header("Resources")]
    [Inject] private PlayerResources _playerResources;

    [Header("Cards")]
    [SerializeField] private CardHolderSystem _cardHolderSystem;

    [Header("WorldEvents")]
    [SerializeField] private WorldEventSystem _worldEventSystem;

    [Header("Enemies")]
    [SerializeField] private EnemiesSpawnerSystem _enemiesSpawnerSystem;

    [Header("Tiles")]
    [Inject] private readonly TilesSystem _tilesSystem;
    [SerializeField] private AllTileObjects _allTileObjects;

    [Header("Robot")]
    [SerializeField] private CurrentMachineSystem _currentMachineSystem;
    [SerializeField] private MachineSpawnerSystem _machineSpawnerSystem;

    [Header("LearnedBuildings")]
    [SerializeField] private LearnedBuildingsDataWorld _learnedBuildingsDataWorld;

    [Header("Objectives")]
    [SerializeField] private ObjectivesPanel _objectivesPanel;

    private void Awake()
    {
        _worldSaveGame.WorldSaveLoad = this;
    }

    public void ResetMissionData(ref WorldSaveData currentSaveData)
    {
        var mission = CurrentMissionInfo.Instance.GetCurrentMission();
        currentSaveData = new WorldSaveData
        {
            IsStartMission = true,
            MissionId = mission.MissionId,
            GameSpeed = (int)GameSpeedEnum.Default,
            ResourcesData = new float[Enum.GetValues(typeof(ResourceEnum)).Length - 1],
        };

        for (int i = 0; i < mission.StartResources.Length; i++)
        {
            int resourceIndex = (int)mission.StartResources[i].ResourceEnum;
            currentSaveData.ResourcesData[resourceIndex] = mission.StartResources[i].RecourceAmount;
        }
    }

    public void SaveMissionData(ref WorldSaveData currentSaveData)
    {
        //Main
        currentSaveData.IsStartMission = false;
        currentSaveData.MissionId = CurrentMissionInfo.Instance.GetCurrentMission().MissionId; // при завершении миссии нужно будет вручную поднимать лвл в скрипте чтобы тут было правильное значение

        //UpPanel
        currentSaveData.Day = _timeTickSystem.GetCurrentDay();
        currentSaveData.Tick = _timeTickSystem.GetCurrentTick();
        currentSaveData.Radiation = _ecologySystem.GetRadiation();
        currentSaveData.GameSpeed = (int)GameSpeedEnum.Pause;

        //Resources
        currentSaveData.ResourcesData = _playerResources.GetAllResourcesAmount();

        //Cards
        currentSaveData.Cards = _cardHolderSystem.GetAllCards();

        //DayEvents
        currentSaveData.DayEventsData = _worldEventSystem.GetAllCurrentEvents();

        //Enemies
        currentSaveData.EnemyData = _enemiesSpawnerSystem.GetAllCurrentEnemies();

        //Tiles
        currentSaveData.IsHaveBase = _tilesSystem.IsHaveBase();
        currentSaveData.IsHaveRiver = _tilesSystem.IsHaveRiver();
        currentSaveData.IsHaveMachineProduction = _tilesSystem.IsHaveMachineProduction();
        currentSaveData.TilesData = _allTileObjects.GetAllTileObjects();
        currentSaveData.RoadTilesId = _tileMapBuilder.GetRoadTilesId();

        //Robot
        currentSaveData.MachinesExperienceData = MachinesDataWorld.Instance.GetAllMachinesExperience();
        currentSaveData.MachineData = _currentMachineSystem.GetMachineData();

        //Objectives
        currentSaveData.ObjectiveAmount = _objectivesPanel.GetAllObjectivesAmount();
    }

    public void LoadMissionData(ref WorldSaveData currentSaveData)
    {
        //Main
        CurrentMissionInfo.Instance.LoadMission(currentSaveData.MissionId, _commandCenterSaveGame.CommandCenterSaveData.LastOpenedMissionId);
        _tileMapBuilder.BuildMap(currentSaveData.IsStartMission);

        //UpPanel
        _timeTickSystem.LoadTime(currentSaveData.Day, currentSaveData.Tick);
        _ecologySystem.LoadEcology(currentSaveData.Radiation);
        _gameSpeedSystem.ChangeGameSpeed(currentSaveData.GameSpeed);

        //Resources
        _playerResources.LoadResources(currentSaveData.ResourcesData);

        //Cards
        _cardHolderSystem.LoadCards(currentSaveData.IsStartMission, currentSaveData.Cards);

        //DayEvents
        _worldEventSystem.LoadEvents(currentSaveData.DayEventsData, currentSaveData.IsStartMission);

        //Enemies
        _enemiesSpawnerSystem.LoadEnemies(currentSaveData.EnemyData, currentSaveData.IsStartMission);

        //Tiles
        _tilesSystem.SetIsHaveBase(currentSaveData.IsHaveBase);
        _tilesSystem.SetIsHaveRiver(currentSaveData.IsHaveRiver);
        _tilesSystem.SetIsHaveMachineProduction(currentSaveData.IsHaveMachineProduction);
        _allTileObjects.LoadTiles(currentSaveData.TilesData, currentSaveData.IsStartMission);
        _tileMapBuilder.LoadRoadTiles(currentSaveData.RoadTilesId, currentSaveData.IsStartMission);

        //Machine
        MachinesDataWorld.Instance.LoadMachinesExperience(currentSaveData.MachinesExperienceData, currentSaveData.IsStartMission);
        _machineSpawnerSystem.LoadSpawnRobot(currentSaveData);

        //Buildings
        _learnedBuildingsDataWorld.LoadLearnedBuildings(_commandCenterSaveGame.CommandCenterSaveData.BuildingsLearned);

        //Objectives
        _objectivesPanel.LoadObjectiveItems(currentSaveData.ObjectiveAmount, currentSaveData.IsStartMission);

        CustomEvents.FirePlayRandomLevelMusic();
        CustomEvents.FireDataLoad();
    }
}
