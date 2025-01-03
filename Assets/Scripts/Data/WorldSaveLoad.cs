using UnityEngine;
using Zenject;

public class WorldSaveLoad : MonoBehaviour
{
    [Inject] private WorldSaveGame _worldSaveGame;

    private void Awake()
    {
        _worldSaveGame.WorldSaveLoad = this;
    }

    public void ResetData(ref WorldSaveData currentSaveData)
    {
        
    }

    public void SaveData(ref WorldSaveData currentSaveData)
    {

    }

    public void LoadData(ref WorldSaveData currentSaveData)
    {

        // CustomEvents.FireDataLoad();
    }
}
