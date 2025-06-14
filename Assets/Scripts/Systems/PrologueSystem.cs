using DG.Tweening;
using UnityEngine;
using Zenject;

public class PrologueSystem : MonoBehaviour
{
    [Inject] private readonly CommandCenterSaveGame _commandCenterSaveGame;

    [Header("UI")]
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private GameObject _prologueCanvas;

    [Header("Other")]

    [SerializeField] private CosmosView _cosmosView;

    public void StartPrologue(bool newGame)
    {
        return;
        if (!newGame) return;
        _cosmosView.SetDefaultCosmos();
        _prologueCanvas.SetActive(true);
        PrepareCanvas();
    }

    private void PrepareCanvas()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
    }

    public void ActiveCanvas()
    {
        _canvasGroup.DOFade(1, 1f).OnComplete(() =>
        {
            _canvasGroup.interactable = true;
            _commandCenterSaveGame.CompletePrologue();
            _prologueCanvas.SetActive(false);
        });
    }
}
