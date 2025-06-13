using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;

public class PrologueSystem : MonoBehaviour
{
    [Inject] private readonly CommandCenterSaveGame _commandCenterSaveGame;

    [Header("UI")]
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private GameObject _prologueCanvas;

    [Header("Other")]
    [SerializeField] private SkyBoxRotation _skyBoxRotation;

    public void StartPrologue(bool newGame)
    {
        return;
        if (!newGame) return;
        _skyBoxRotation.enabled = false;
        _prologueCanvas.SetActive(true);
        PrepareCanvas();
    }

    private void PrepareCanvas()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
    }

    private void ActiveCanvas()
    {
        _canvasGroup.DOFade(1, 1f).OnComplete(() =>
        {
            _canvasGroup.interactable = true;
            _skyBoxRotation.enabled = true;
            _commandCenterSaveGame.CompletePrologue();
            _prologueCanvas.SetActive(false);
        });
    }
}
