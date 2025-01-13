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

    [Header("DayEvents")]
    [SerializeField] private DayEventSystem _dayEventSystem;

    [Header("Enemies")]
    [SerializeField] private EnemiesSpawnerSystem _enemiesSpawnerSystem;

    [Header("Tiles")]
    [Inject] private readonly TilesSystem _tilesSystem;
    [SerializeField] private AllTileObjects _allTileObjects;

    [Header("Robot")]
    [SerializeField] private CurrentRobotSystem _currentRobotSystem;
    [SerializeField] private RobotSpawnerSystem _robotSpawnerSystem;

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

    }

    public void SaveMissionData(ref WorldSaveData currentSaveData)
    {
        //Main
        currentSaveData.IsStartMission = false;
        currentSaveData.MissionId = CurrentMissionInfo.Instance.CurrentMission().MissionId; // при завершении миссии нужно будет вручную поднимать лвл в скрипте чтобы тут было правильное значение

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
        currentSaveData.DayEventsData = _dayEventSystem.GetAllCurrentEvents();

        //Enemies
        currentSaveData.EnemyData = _enemiesSpawnerSystem.GetAllCurrentEnemies();

        //Tiles
        currentSaveData.IsHaveBase = _tilesSystem.IsHaveBase();
        currentSaveData.IsHaveRiver = _tilesSystem.IsHaveRiver();
        currentSaveData.TilesData = _allTileObjects.GetAllTileObjects();
        currentSaveData.RoadTilesId = _tileMapBuilder.GetRoadTilesId();

        //Robot
        currentSaveData.RobotsExperienceData = RobotsDataWorld.Instance.GetAllRobotsExperience();
        currentSaveData.RobotData = _currentRobotSystem.GetRobotData();

        //Objectives
        currentSaveData.ObjectiveAmount = _objectivesPanel.GetAllObjectivesAmount();
    }

    public void LoadMissionData(ref WorldSaveData currentSaveData)
    {
        //Main
        CurrentMissionInfo.Instance.LoadMission(currentSaveData.MissionId);
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
        _dayEventSystem.LoadEvents(currentSaveData.DayEventsData);

        //Enemies
        _enemiesSpawnerSystem.LoadEnemies(currentSaveData.EnemyData, currentSaveData.IsStartMission);

        //Tiles
        _tilesSystem.SetIsHaveBase(currentSaveData.IsHaveBase);
        _tilesSystem.SetIsHaveRiver(currentSaveData.IsHaveRiver);
        _allTileObjects.LoadTiles(currentSaveData.TilesData, currentSaveData.IsStartMission);
        _tileMapBuilder.LoadRoadTiles(currentSaveData.RoadTilesId);

        //Robot
        RobotsDataWorld.Instance.LoadRobotsExperience(currentSaveData.RobotsExperienceData, currentSaveData.IsStartMission);
        _robotSpawnerSystem.LoadSpawnRobot(currentSaveData);

        //Buildings
        _learnedBuildingsDataWorld.LoadLearnedBuildings(_commandCenterSaveGame.CommandCenterSaveData.BuildingsLearned);

        //Objectives
        _objectivesPanel.LoadObjectiveItems(currentSaveData.ObjectiveAmount, currentSaveData.IsStartMission);

        CustomEvents.FireDataLoad();
    }
}
