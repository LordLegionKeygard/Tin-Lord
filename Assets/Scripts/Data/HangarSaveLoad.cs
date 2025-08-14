using UnityEngine;
using Zenject;

public class HangarSaveLoad : MonoBehaviour
{
    [Inject] private readonly HangarSaveGame _hangarSaveGame;
    [SerializeField] private ShardsSystem _shardsSystem;
    [SerializeField] private HangarSystem _hangarSystem;

    private void Awake()
    {
        _hangarSaveGame.HangarSaveLoad = this;
    }

    public void SaveData(ref HangarSaveData currentSaveData)
    {
        currentSaveData.Shards = _shardsSystem.GetShards();
        currentSaveData.OpenedRobots = _hangarSystem.GetOpenedRobots();
        currentSaveData.OpenedCrates = _hangarSystem.GetOpenedCrates();
        currentSaveData.OpenedSkills = _hangarSystem.GetOpenedSkills();
        currentSaveData.OpenedShipWeapons = _hangarSystem.GetOpenedShipWeapons();
    }

    public void LoadGameData(ref HangarSaveData currentSaveData)
    {
        _shardsSystem.LoadShards(currentSaveData.Shards);
        _hangarSystem.LoadHangar(currentSaveData);
    }
}
