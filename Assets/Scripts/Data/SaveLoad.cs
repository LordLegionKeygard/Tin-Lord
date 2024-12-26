using UnityEngine;
using Zenject;

public class SaveLoad : MonoBehaviour
{
    [Inject] private WorldSaveGame _worldSaveGame;

    private void Awake()
    {
        _worldSaveGame.SaveLoad = this;
    }

    public void SaveData(ref CommandCenterSaveData currentSaveData)
    {
       
    }

    public void LoadData(ref CommandCenterSaveData currentSaveData)
    {
        CustomEvents.FireDataLoad();
    }
}
