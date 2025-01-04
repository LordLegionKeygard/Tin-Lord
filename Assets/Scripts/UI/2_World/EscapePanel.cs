using System.Collections;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class EscapePanel : MonoBehaviour
{
    [Inject] private readonly WorldSaveGame _worldSaveGame;
    [SerializeField] private RectTransform _objectTransform;
    [SerializeField] private GameSpeedSystem _gameSpeedSystem;
    [SerializeField] private GameObject _escapePanelBackgroundBlack;
    private bool _isOpen;

    public void PanelViewToggle()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.DefaultClick, transform.position);
        _isOpen = !_isOpen;

        _gameSpeedSystem.SpeedButtonInteractableToggle(_isOpen);
        _escapePanelBackgroundBlack.SetActive(_isOpen);

        if (_isOpen)
        {
            _gameSpeedSystem.ChangeGameSpeed((int)GameSpeedEnum.Pause);
            _objectTransform.DOAnchorPosY(-185.5f, 0.3f).SetUpdate(true);
        }
        else
        {
            _gameSpeedSystem.ChangeGameSpeed((int)GameSpeedEnum.Default);
            _objectTransform.DOAnchorPosY(-55, 0.3f).SetUpdate(true);
        }
    }

    public void MenuButton()
    {
        CustomEvents.FireFade(FadeType.StartFade);
        _worldSaveGame.SaveMissionGameData();
        StartCoroutine(nameof(PrepareLoad));
    }

    private IEnumerator PrepareLoad()
    {
        yield return new WaitForSecondsRealtime(1);
        CustomEvents.FireLoadScene(SceneEnum.CommandCenter, 5f, true);
    }
}
