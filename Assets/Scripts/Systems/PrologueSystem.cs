using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;

public class PrologueSystem : MonoBehaviour
{
    [Inject] readonly CommandCenterSaveGame CommandCenterSaveGame;
    
    [Header("Camera")]
    [SerializeField] private Transform _cameraTransform;
    private int _defaultCameraPosition = 359;
    private int _startPrologueCameraPosition = 430;

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
        MoveCamera();
        PrepareCanvas();
    }

    private void PrepareCanvas()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
    }

    private void MoveCamera()
    {
        _cameraTransform.position = new Vector3(_startPrologueCameraPosition, _cameraTransform.position.y, _cameraTransform.position.z);
        _cameraTransform.DOMoveX(_defaultCameraPosition, 94).OnComplete(() => ActiveCanvas());
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
