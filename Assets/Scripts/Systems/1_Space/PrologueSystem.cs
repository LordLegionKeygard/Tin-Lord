using UnityEngine;
using Zenject;

public class PrologueSystem : MonoBehaviour
{
    [Inject] private readonly SpaceSaveGame _spaceSaveGame;

    [Header("UI")]
    [SerializeField] private EventNodePanel _eventPanel;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private GameObject _prologueCanvas;

    [Header("Other")]
    [SerializeField] private TutorialSystem _tutorialSystem;
    [SerializeField] private UIPanelsSpace _panels;
    [SerializeField] private DialogueSequence _prologueDialog;
    [SerializeField] private CosmosView _cosmosView;


    public void StartPrologueAndTutorial(bool prologueCompleted)
    {
        if (prologueCompleted) return;

        _eventPanel.Open(_prologueDialog, OnPrologueFinished);
        _panels.EventPanelOpen();
    }

    private void OnPrologueFinished()
    {
        _spaceSaveGame.CompletePrologue();
        if(!_tutorialSystem.IsStartTutorial()) _tutorialSystem.LoadTutorial(0, true);
    }
}
