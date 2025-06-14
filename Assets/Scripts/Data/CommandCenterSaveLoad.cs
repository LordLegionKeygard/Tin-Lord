using UnityEngine;
using Zenject;

public class CommandCenterSaveLoad : MonoBehaviour
{
    [Inject] private readonly CommandCenterSaveGame _commandCenterSaveGame;
    [SerializeField] private BuildingsLearnPanel _buildingsLearnPanel;
    [SerializeField] private PrologueSystem _prologue;
    [SerializeField] private AiCoreSystem _aiCoreSystem;

    private void Awake()
    {
        _commandCenterSaveGame.CommandCenterSaveLoad = this;
    }

    public void SaveData(ref CommandCenterSaveData currentSaveData)
    {
        currentSaveData.AiCores = _aiCoreSystem.GetAiCores();
        currentSaveData.MemoryFragments = _buildingsLearnPanel.MemoryFragments();

        for (int i = 0; i < _buildingsLearnPanel.AllLearnBuildingItems().Length; i++)
        {
            currentSaveData.BuildingsLearned[i] = _buildingsLearnPanel.AllLearnBuildingItems()[i].IsLearn();
        }
    }

    public void LoadData(ref CommandCenterSaveData currentSaveData)
    {
        _aiCoreSystem.LoadAiCore(currentSaveData.AiCores);
        _buildingsLearnPanel.SetFragments(currentSaveData.MemoryFragments);
        _prologue.StartPrologue(!currentSaveData.PrologueCompleted);

        for (int i = 0; i < _buildingsLearnPanel.AllLearnBuildingItems().Length; i++)
        {
            _buildingsLearnPanel.AllLearnBuildingItems()[i].SetupData(currentSaveData.BuildingsLearned[i]);
        }


        // _missionPanel.LoadLastOpenedMissionId(currentSaveData.LastOpenedMissionId);

        CustomEvents.FireDataLoad();
    }
}
