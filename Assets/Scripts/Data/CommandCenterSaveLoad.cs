using UnityEngine;
using Zenject;

public class CommandCenterSaveLoad : MonoBehaviour
{
    [Inject] private readonly CommandCenterSaveGame _commandCenterSaveGame;
    [SerializeField] private PrologueSystem _prologue;
    [SerializeField] private QuantsSystem _quantsSystem;
    [SerializeField] private AiCoreSystem _aiCoreSystem;
    [SerializeField] private BuildingsLearnPanel _buildingsLearnPanel;
    [SerializeField] private MainResources _mainResources;

    private void Awake()
    {
        _commandCenterSaveGame.CommandCenterSaveLoad = this;
    }

    public void SaveData(ref CommandCenterSaveData currentSaveData)
    {
        currentSaveData.Quants = _quantsSystem.GetQuants();
        currentSaveData.AiCores = _aiCoreSystem.GetAiCores();
        currentSaveData.MainResourcesData = _mainResources.GetAllResourcesAmount();

        for (int i = 0; i < _buildingsLearnPanel.AllLearnBuildingItems().Length; i++)
        {
            currentSaveData.BuildingsLearned[i] = _buildingsLearnPanel.AllLearnBuildingItems()[i].IsLearn();
        }
    }

    public void LoadData(ref CommandCenterSaveData currentSaveData)
    {
        _quantsSystem.LoadQuants(currentSaveData.Quants);
        _aiCoreSystem.LoadAiCore(currentSaveData.AiCores);
        _mainResources.LoadResources(currentSaveData.MainResourcesData);
        _prologue.StartPrologue(!currentSaveData.PrologueCompleted);

        for (int i = 0; i < _buildingsLearnPanel.AllLearnBuildingItems().Length; i++)
        {
            _buildingsLearnPanel.AllLearnBuildingItems()[i].SetupData(currentSaveData.BuildingsLearned[i]);
        }

        CustomEvents.FireDataLoad();
    }
}
