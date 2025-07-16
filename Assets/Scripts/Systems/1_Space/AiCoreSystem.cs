using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class AiCoreSystem : MonoBehaviour
{
    [Inject] private HangarSaveGame _hangarSaveGame;
    [Inject] private SpaceSaveGame _spaceSaveGame;
    [Inject] private MissionSaveGame _missionSaveGame;
    [SerializeField] private int _aiCore;
    [SerializeField] private CellsView _cellsView;
    [SerializeField] private EventNodePanel _eventPanel;
    [SerializeField] private DialogueSequence _endGameDialogue;
    [SerializeField] private UIPanelsSpace _uiPanelsSpace;
    [SerializeField] private ShardsCalculateSystem _shardsCalculateSystem;

    public int GetAiCores() => _aiCore;

    public void LoadAiCore(int core)
    {
        _aiCore = core;
        _cellsView.UpdateCellSlotsView(_aiCore);
        CheckAiDeath();
    }

    public void ChangeAiCores(int value)
    {
        _aiCore += value > WorldGameInfo.MaxAiCores ? WorldGameInfo.MaxAiCores : value;
        _cellsView.UpdateCellSlotsView(_aiCore);
        CheckAiDeath();
    }

    public void CheckAiDeath()
    {
        if (_aiCore > 0) return;

        ShowGameOverPanel();
    }

    private void ShowGameOverPanel()
    {
        _shardsCalculateSystem.Calculate();
        _eventPanel.Open(_endGameDialogue, GameOver);
        _uiPanelsSpace.EventPanelOpen();
    }

    private void GameOver()
    {
        _hangarSaveGame.SaveEndGameDataToJson(_shardsCalculateSystem.GetCalculatedShards());
        _missionSaveGame.DeleteMissionJson();
        _spaceSaveGame.GetCommandCenterSaveGameDataWriter().DeleteSaveFile();
        LoadMainMenu();
    }

    public void LoadMainMenu()
    {
        CustomEvents.FireFade(FadeType.StartFade);
        StartCoroutine(nameof(PrepareLoad));
    }

    private IEnumerator PrepareLoad()
    {
        yield return new WaitForSecondsRealtime(1);
        CustomEvents.FireLoadScene(SceneEnum.Hangar, WorldGameInfo.LoadSceneTime, null);
    }

}
