using System.Collections;
using UnityEngine;
using Zenject;

public class EndGameSystem : MonoBehaviour
{
    [Inject] private HangarSaveGame _hangarSaveGame;
    [Inject] private SpaceSaveGame _spaceSaveGame;
    [Inject] private MissionSaveGame _missionSaveGame;
    [SerializeField] private UIPanelsSpace _uiPanelsSpace;
    [SerializeField] private EventNodePanel _eventPanel;
    [SerializeField] private ShardsCalculateSystem _shardsCalculateSystem;
    [SerializeField] private Landscape _megastructureLandscape;
    [SerializeField] private CosmosView _cosmosView;
    [SerializeField] private GameObject _explosionVfx;
    [SerializeField] private GameObject[] _falseObjects;
    [SerializeField] private DialogueSequence _completeGameDialogue;
    public void CheckCompleteGame(bool completeGame)
    {
        if (!completeGame || WorldGameInfo.IsDemo) return;

        foreach (var item in _falseObjects)
        {
            item.SetActive(false);
        }

        _cosmosView.ChangeCosmos(_megastructureLandscape.CosmosVariations, 0);
        // _explosionVfx.SetActive(true);
        ShowEndGamePanel(_completeGameDialogue);
    }

    public void ShowEndGamePanel(DialogueSequence dialogueSequence)
    {
        _shardsCalculateSystem.CalculateAllShards();
        _eventPanel.Open(dialogueSequence, PrepareSaveData);
        _uiPanelsSpace.EventPanelOpen();
    }

    private void PrepareSaveData()
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
