using UnityEngine;
using Zenject;

public class WorldSaveLoad : MonoBehaviour
{
    [Inject] private WorldSaveGame _worldSaveGame;
    [Inject] private readonly TilesSystem _tilesSystem;
    [SerializeField] private TileMapBuilder _tileMapBuilder;
    [SerializeField] private TimeTickSystem _timeTickSystem;
    [SerializeField] private EcologySystem _ecologySystem;
    [SerializeField] private GameSpeedSystem _gameSpeedSystem;

    private void Awake()
    {
        _worldSaveGame.WorldSaveLoad = this;
    }

    public void ResetData(ref WorldSaveData currentSaveData)
    {

    }

    public void SaveData(ref WorldSaveData currentSaveData)
    {
        currentSaveData.IsStartMission = false;
    }

    public void LoadData(ref WorldSaveData currentSaveData)
    {
        //Main
        CurrentMissionInfo.Instance.LoadMission(currentSaveData.MissionId);
        _tileMapBuilder.BuildMap(currentSaveData.IsStartMission);

        //UpPanel
        _timeTickSystem.LoadCurrentDay(currentSaveData.Day);
        _ecologySystem.LoadEcology(currentSaveData.Radiation);
        _gameSpeedSystem.ChangeGameSpeed(currentSaveData.GameSpeed);

        //Experience

        //Resources

        //Planet
        _tilesSystem.SetIsHaveBase(currentSaveData.IsHaveBase); 
        _tilesSystem.SetIsHaveRiver(currentSaveData.IsHaveRiver);

        CustomEvents.FireDataLoad();
    }
}
