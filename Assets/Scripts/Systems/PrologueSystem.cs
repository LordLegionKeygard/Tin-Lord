using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;

public class PrologueSystem : MonoBehaviour
{
    [Inject] readonly CommandCenterSaveGame CommandCenterSaveGame;

    [Header("Planet")]
    [SerializeField] private RectTransform _planetRectTransform;
    [SerializeField] private Vector3 _defaultPlanetScale;
    [SerializeField] private Vector3 _startProloguePlanetScale;

    [Header("UI")]
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private GameObject _prologueCanvas;

    [Header("Other")]
    [SerializeField] private SkyBoxRotation _skyBoxRotation;

    public void StartPrologue(bool newGame)
    {
        if (!newGame) return;
        _skyBoxRotation.enabled = false;
        _prologueCanvas.SetActive(true);
        ScalePlanet();
        PrepareCanvas();
    }

    private void PrepareCanvas()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
    }

    private void ScalePlanet()
    {
        _planetRectTransform.localScale = _startProloguePlanetScale;
        _planetRectTransform.DOScale(_defaultPlanetScale, 94).OnComplete(() => ActiveCanvas());
    }

    private void ActiveCanvas()
    {
        _canvasGroup.DOFade(1, 1f).OnComplete(() =>
        {
            _canvasGroup.interactable = true;
            _skyBoxRotation.enabled = true;
            CommandCenterSaveGame.SaveGameData(false);
            _prologueCanvas.SetActive(false);
        });
    }
}
