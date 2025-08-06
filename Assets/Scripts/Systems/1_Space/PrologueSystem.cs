using UnityEngine;
using Zenject;

public class PrologueSystem : MonoBehaviour
{
    [Inject] private readonly SpaceSaveGame _spaceSaveGame;
    [Inject] private readonly TutorialSystem _tutorialSystem;
    [SerializeField] private EventNodePanel _eventPanel;
    [SerializeField] private UIPanelsSpace _panels;
    [SerializeField] private DialogueSequence _prologueDialogue;


    public void StartPrologueAndTutorial(bool prologueCompleted)
    {
        if (prologueCompleted) return;

        _eventPanel.Open(_prologueDialogue, OnPrologueFinished);
        _panels.EventPanelOpen();
    }

    private void OnPrologueFinished()
    {
        _spaceSaveGame.CompletePrologue();
        if (_tutorialSystem.IsStartTutorial() && !_tutorialSystem.IsCompleteAllTutorial())
        {
            _tutorialSystem.LoadTutorial(0, true);
        }
    }
}
