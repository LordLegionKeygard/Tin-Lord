using UnityEngine;
using Zenject;

public class HangarSaveLoad : MonoBehaviour
{
    [Inject] private readonly HangarSaveGame _hangarSaveGame;
    [SerializeField] private ShardsSystem _shardsSystem;

    private void Awake()
    {
        _hangarSaveGame.HangarSaveLoad = this;
    }

    public void SaveData(ref HangarSaveData currentSaveData)
    {
        currentSaveData.Shards = _shardsSystem.GetShards();
    }

    public void LoadGameData(ref HangarSaveData currentSaveData)
    {
        _shardsSystem.LoadShards(currentSaveData.Shards);
    }
}
