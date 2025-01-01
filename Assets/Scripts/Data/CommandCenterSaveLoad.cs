using UnityEngine;
using Zenject;

public class CommandCenterSaveLoad : MonoBehaviour
{
    [Inject] private CommandCenterSaveGame _commandCenterSaveGame;
    [SerializeField] private BuildingsLearnPanel _buildingsLearnPanel;
    [SerializeField] private MissionPanel _missionPanel;

    private void Awake()
    {
        _commandCenterSaveGame.SaveLoad = this;
    }

    public void SaveData(ref CommandCenterSaveData currentSaveData)
    {
        currentSaveData.MemoryFragments = _buildingsLearnPanel.MemoryFragments();

        for (int i = 0; i < _buildingsLearnPanel.AllLearnBuildingItems().Length; i++)
        {
            currentSaveData.BuildingsLearned[i] = _buildingsLearnPanel.AllLearnBuildingItems()[i].IsLearn();
        }

        currentSaveData.LastOpenedMissionId = _missionPanel.LastOpenedMissionId;
    }

    public void LoadData(ref CommandCenterSaveData currentSaveData)
    {
        _buildingsLearnPanel.SetFragments(currentSaveData.MemoryFragments);

        for (int i = 0; i < _buildingsLearnPanel.AllLearnBuildingItems().Length; i++)
        {
            _buildingsLearnPanel.AllLearnBuildingItems()[i].SetupData(currentSaveData.BuildingsLearned[i]);
        }

        _missionPanel.LastOpenedMissionId = currentSaveData.LastOpenedMissionId;

        CustomEvents.FireDataLoad();
    }
}
