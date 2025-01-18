using UnityEngine;
using Zenject;

public class CommandCenterSaveLoad : MonoBehaviour
{
    [Inject] private readonly CommandCenterSaveGame _commandCenterSaveGame;
    [SerializeField] private BuildingsLearnPanel _buildingsLearnPanel;
    [SerializeField] private MissionPanel _missionPanel;

    private void Awake()
    {
        _commandCenterSaveGame.CommandCenterSaveLoad = this;
    }

    public void SaveData(ref CommandCenterSaveData currentSaveData)
    {
        currentSaveData.MemoryFragments = _buildingsLearnPanel.MemoryFragments();

        for (int i = 0; i < _buildingsLearnPanel.AllLearnBuildingItems().Length; i++)
        {
            currentSaveData.BuildingsLearned[i] = _buildingsLearnPanel.AllLearnBuildingItems()[i].IsLearn();
        }

        // currentSaveData.LastOpenedMissionId = _missionPanel.LoadLastOpenedMissionId() _lastOpenedMissionId;
    }

    public void LoadData(ref CommandCenterSaveData currentSaveData)
    {
        _buildingsLearnPanel.SetFragments(currentSaveData.MemoryFragments);

        for (int i = 0; i < _buildingsLearnPanel.AllLearnBuildingItems().Length; i++)
        {
            _buildingsLearnPanel.AllLearnBuildingItems()[i].SetupData(currentSaveData.BuildingsLearned[i]);
        }

        _missionPanel.LoadLastOpenedMissionId(currentSaveData.LastOpenedMissionId);

        CustomEvents.FireDataLoad();
    }
}
