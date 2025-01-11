using UnityEngine;
using Zenject;

public class WorldSaveLoad : MonoBehaviour
{
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

    [Header("DayEvent")]
    [SerializeField] private DayEventSystem _dayEventSystem;

    [Header("Tiles")]
    [Inject] private readonly TilesSystem _tilesSystem;
    [SerializeField] private AllTileObjects _allTileObjects;

    [Header("Robot")]
    [SerializeField] private CurrentRobotSystem _currentRobotSystem;
    [SerializeField] private RobotSpawnerSystem _robotSpawnerSystem;

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
        currentSaveData.Time = _timeTickSystem.GetCurrentTime();
        currentSaveData.Radiation = _ecologySystem.GetRadiation();
        currentSaveData.GameSpeed = (int)GameSpeedEnum.Pause;

        //Resources
        currentSaveData.ResourcesData = _playerResources.GetAllResourcesAmount();

        //Cards
        currentSaveData.Cards = _cardHolderSystem.GetAllCards();

        //DayEvents
        currentSaveData.DayEventsData = _dayEventSystem.GetAllCurrentEvents();

        //Tiles
        currentSaveData.IsHaveBase = _tilesSystem.IsHaveBase();
        currentSaveData.IsHaveRiver = _tilesSystem.IsHaveRiver();
        currentSaveData.TilesData = _allTileObjects.GetAllTileObjects();
        currentSaveData.RoadTilesId = _tileMapBuilder.GetRoadTilesId();

        //Robot
        currentSaveData.RobotsExperienceData = RobotsDataWorld.Instance.GetAllRobotsExperience();
        currentSaveData.RobotData = _currentRobotSystem.GetRobotData();
    }

    public void LoadMissionData(ref WorldSaveData currentSaveData)
    {
        //Main
        CurrentMissionInfo.Instance.LoadMission(currentSaveData.MissionId);
        _tileMapBuilder.BuildMap(currentSaveData.IsStartMission);

        //UpPanel
        _timeTickSystem.LoadCurrentDay(currentSaveData.Day, currentSaveData.Time);
        _ecologySystem.LoadEcology(currentSaveData.Radiation);
        _gameSpeedSystem.ChangeGameSpeed(currentSaveData.GameSpeed);

        //Resources
        _playerResources.LoadResources(currentSaveData.ResourcesData);

        //Cards
        _cardHolderSystem.LoadCards(currentSaveData.IsStartMission, currentSaveData.Cards);

        //DayEvents
        _dayEventSystem.LoadEvents(currentSaveData.DayEventsData);

        //Tiles
        _tilesSystem.SetIsHaveBase(currentSaveData.IsHaveBase);
        _tilesSystem.SetIsHaveRiver(currentSaveData.IsHaveRiver);
        _allTileObjects.LoadTiles(currentSaveData.TilesData, currentSaveData.IsStartMission);
        _tileMapBuilder.LoadRoadTiles(currentSaveData.RoadTilesId);

        //Robot
        RobotsDataWorld.Instance.LoadRobotsExperience(currentSaveData.RobotsExperienceData,currentSaveData.IsStartMission);
        _robotSpawnerSystem.LoadSpawnRobot(currentSaveData);  //сначала спавним и передаем в патрол патх из даты индекс

        CustomEvents.FireDataLoad();
    }
}
