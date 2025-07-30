using System.Collections;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class EscapePanelSpace : MonoBehaviour
{
    [Inject] private readonly SpaceSaveGame _saveGame;
    [SerializeField] private RectTransform _objectTransform;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _escapePanelBackgroundBlack;
    [SerializeField] private CanvasGroup _tutorialCanvasGroup;
    private bool _isOpen;

    public void PanelViewToggle()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.EscapePanel, transform.position);

        _isOpen = !_isOpen;

        _escapePanelBackgroundBlack.SetActive(_isOpen);

        if (_isOpen)
        {
            _objectTransform.DOAnchorPosY(-62f, 0.8f).SetUpdate(true);
            _tutorialCanvasGroup.alpha = 0;
        }
        else
        {
            _objectTransform.DOAnchorPosY(100, 0.8f).SetUpdate(true);
            _tutorialCanvasGroup.alpha = 1;
        }
    }

    public void ContinueButton()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        PanelViewToggle();
    }

    public void SettingsButton()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        _settingsPanel.SetActive(true);
    }

    public void MenuButton()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        CustomEvents.FireFade(FadeType.StartFade);
        StartCoroutine(nameof(PrepareLoad));
    }

    private IEnumerator PrepareLoad()
    {
        yield return new WaitForSecondsRealtime(1);
        _saveGame.SaveDataToJson();
        CustomEvents.FireLoadScene(SceneEnum.Hangar, WorldGameInfo.LoadSceneTime, null);

    }
}
