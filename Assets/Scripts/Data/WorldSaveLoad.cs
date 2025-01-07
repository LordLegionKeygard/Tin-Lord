using UnityEngine;
using Zenject;

public class WorldSaveLoad : MonoBehaviour
{
    [Inject] private WorldSaveGame _worldSaveGame;
    [Inject] private readonly TilesSystem _tilesSystem;
    [Inject] private PlayerResources _playerResources;
    [SerializeField] private TileMapBuilder _tileMapBuilder;
    [SerializeField] private TimeTickSystem _timeTickSystem;
    [SerializeField] private EcologySystem _ecologySystem;
    [SerializeField] private GameSpeedSystem _gameSpeedSystem;
    [SerializeField] private CardHolderSystem _cardHolderSystem;
    [SerializeField] private SetTileNeighbours _setTileNeighbours;

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

        //Experience

        //Resources
        currentSaveData.ResourcesData = _playerResources.GetAllResourcesAmount();

        //Cards
        currentSaveData.Cards = _cardHolderSystem.GetAllCards();

        //Tiles
        currentSaveData.IsHaveBase = _tilesSystem.IsHaveBase();
        currentSaveData.IsHaveRiver = _tilesSystem.IsHaveRiver();
        currentSaveData.TilesData = _setTileNeighbours.GetAllTileObjects();
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

        //Experience

        //Resources
        _playerResources.LoadResources(currentSaveData.ResourcesData);

        //Cards
        _cardHolderSystem.LoadCards(currentSaveData.IsStartMission, currentSaveData.Cards);

        //Tiles
        _tilesSystem.SetIsHaveBase(currentSaveData.IsHaveBase);
        _tilesSystem.SetIsHaveRiver(currentSaveData.IsHaveRiver);
        _setTileNeighbours.LoadTiles(currentSaveData.TilesData, currentSaveData.IsStartMission);

        CustomEvents.FireDataLoad();
    }
}
