using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MissionModeSystem : MonoBehaviour
{
    [Inject] private readonly EndMissionSystem _endMissionSystem;
    private bool _isPlanetMode = true;
    [SerializeField] private ShipWeaponsPanel _shipWeaponsPanel;

    [Header("View")]
    [SerializeField] private Image _modeImage;
    [SerializeField] private Sprite[] _modeSprites;
    [SerializeField] private TextMeshProUGUI _modeText;
    [SerializeField] private CanvasGroup[] _canvasGroups;

    [Header("Ship Mode")]
    [SerializeField] private Transform _shipWeaponParentObjects;
    [SerializeField] private ScreenCornerAnchor3D[] _screenCornerAnchor3D;

    private readonly float _hiddenY = -70f;
    private readonly float _shownY = 0f;
    private readonly float _showDuration = 0.45f;
    private readonly float _hideDuration = 0.35f;
    private Tween _shipTween;

    public bool IsPlanetMode() => _isPlanetMode;

    public void LoadMode(bool isCannonMode)
    {
        if (isCannonMode)
        {
            ChangeMode(false);
        }
    }

    public void ChangeModeAfterMissionEnd()
    {
        _isPlanetMode = true;
        ChangeView(false);
    }

    public void ChangeMode(bool needSound)
    {
        if (_endMissionSystem.IsMissionEnd()) return;

        _isPlanetMode = !_isPlanetMode;
        ChangeView(needSound);
    }

    private void ChangeView(bool needSound)
    {
        _modeImage.sprite = _modeSprites[_isPlanetMode ? 0 : 1];
        _modeText.text = Language.TextStatic[_isPlanetMode ? 226 : 227];
        _modeText.color = _isPlanetMode ? Colors.GreySeven : Colors.WarningRed;

        _shipWeaponsPanel.SetupPanelsActive(_isPlanetMode);

        if (_isPlanetMode)
        {
            if (needSound) AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.TurnOffShipMode, transform.position);
            PlayHideShipMode();
        }
        else
        {
            if (needSound) AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.TurnOnShipMode, transform.position);
            PlayShowShipMode();
        }
    }
    private void PlayShowShipMode()
    {
        SetAnchorsActive(false);
        SetCanvasGroupsAlpha(0);

        // убьём прошлую анимацию, если была
        _shipTween?.Kill();

        // стартуем от текущей позиции
        _shipTween = _shipWeaponParentObjects.DOLocalMoveY(_shownY, _showDuration).SetEase(Ease.OutCubic).SetUpdate(true)
        .OnComplete(() =>
        {
            // включаем якоря обратно, когда пушки уже на месте
            SetAnchorsActive(true);
        });
    }

    private void PlayHideShipMode()
    {
        // выключаем якоря перед анимацией
        SetAnchorsActive(false);
        SetCanvasGroupsAlpha(1);

        _shipTween?.Kill();

        _shipTween = _shipWeaponParentObjects.DOLocalMoveY(_hiddenY, _hideDuration).SetEase(Ease.InCubic).SetUpdate(true);
    }

    private void SetAnchorsActive(bool state)
    {
        _screenCornerAnchor3D[0].enabled = state;
        _screenCornerAnchor3D[1].enabled = state;
    }

    private void SetCanvasGroupsAlpha(int alpha)
    {
        foreach (var item in _canvasGroups)
        {
            item.DOFade(alpha, _showDuration * 0.8f).SetEase(Ease.OutQuad).SetUpdate(true);
            item.interactable = alpha == 1;
        }
    }

    private void OnDestroy()
    {
        _shipTween?.Kill();
    }
}
