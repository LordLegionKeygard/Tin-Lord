using UnityEngine;
using Zenject;

public class PrologueSystem : MonoBehaviour
{
    [Inject] private readonly CommandCenterSaveGame _commandCenterSaveGame;

    [Header("UI")]
    [SerializeField] private EventNodePanel _eventPanel;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private GameObject _prologueCanvas;

    [Header("Other")]
    [SerializeField] private UIPanelsCommandCenter _panels;
    [SerializeField] private DialogueSequence _prologueDialog;
    [SerializeField] private CosmosView _cosmosView;


    public void StartPrologue(bool newGame)
    {
        if (!newGame) return;

        _eventPanel.Open(_prologueDialog);
        _panels.EventPanelOpen();
        _commandCenterSaveGame.CompletePrologue(); // логика старого пролога, пока удалять не будем
    }


    // Старый пролог
    // public void StartPrologue(bool newGame)
    // {
    //     if (!newGame) return;
    //     _cosmosView.SetDefaultCosmos();
    //     _prologueCanvas.SetActive(true);
    //     PrepareCanvas();
    // }

    // private void PrepareCanvas()
    // {
    //     _canvasGroup.alpha = 0;
    //     _canvasGroup.interactable = false;
    // }

    // public void ActiveCanvas()
    // {
    //     _canvasGroup.DOFade(1, 1f).OnComplete(() =>
    //     {
    //         _canvasGroup.interactable = true;
    //         _commandCenterSaveGame.CompletePrologue();
    //         _prologueCanvas.SetActive(false);
    //     });
    // }
}
