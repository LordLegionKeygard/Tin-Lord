using UnityEngine;
using Zenject;

public class SpaceSaveLoad : MonoBehaviour
{
    [Inject] private readonly HangarSaveGame _hangarSaveGame;
    [Inject] private readonly SpaceSaveGame _spaceSaveGame;
    [Inject] private readonly TutorialSystem _tutorialSystem;
    [SerializeField] private PrologueSystem _prologue;
    [SerializeField] private QuantsSystem _quantsSystem;
    [SerializeField] private AiCoreSystem _aiCoreSystem;
    [SerializeField] private BuildingsLearnPanel _buildingsLearnPanel;
    [SerializeField] private MainResources _mainResources;

    private void Awake()
    {
        _spaceSaveGame.SpaceSaveLoad = this;
    }

    public void SaveData(ref SpaceSaveData currentSaveData)
    {
        currentSaveData.Quants = _quantsSystem.GetQuants();
        currentSaveData.AiCores = _aiCoreSystem.GetAiCores();
        currentSaveData.MainResourcesData = _mainResources.GetAllResourcesAmount();

        for (int i = 0; i < _buildingsLearnPanel.AllLearnBuildingItems().Length; i++)
        {
            currentSaveData.BuildingsLearned[i] = _buildingsLearnPanel.AllLearnBuildingItems()[i].IsLearn();
        }
    }

    public void LoadGameData(ref SpaceSaveData currentSaveData)
    {
        _quantsSystem.LoadQuants(currentSaveData.Quants);
        _aiCoreSystem.LoadAiCore(currentSaveData.AiCores);
        _mainResources.LoadResources(currentSaveData.MainResourcesData);
        _prologue.StartPrologueAndTutorial(currentSaveData.PrologueCompleted);

        for (int i = 0; i < _buildingsLearnPanel.AllLearnBuildingItems().Length; i++)
        {
            _buildingsLearnPanel.AllLearnBuildingItems()[i].SetupData(currentSaveData.BuildingsLearned[i]);
        }

        _tutorialSystem.LoadTutorial(_hangarSaveGame.HangarSaveData.TutorialProgress, currentSaveData.PrologueCompleted);

        CustomEvents.FireDataLoad();
    }
}
