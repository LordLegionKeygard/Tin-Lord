using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionModeSystem : MonoBehaviour
{
    private bool _isPlanetMode = true;
    public bool IsPlanetMode() => _isPlanetMode;
    [SerializeField] private UIPanelsMission _uiPanelsMission;

    [Header("View")]
    [SerializeField] private Image _modeImage;
    [SerializeField] private Sprite[] _modeSprites;
    [SerializeField] private TextMeshProUGUI _modeText;
    [SerializeField] private CanvasGroup[] _canvasGroups;

    [Header("Ship Mode")]
    [SerializeField] private GameObject _shipWeaponCamera;          // объект камеры оружия (или её корневой GO)
    [SerializeField] private Transform _shipWeaponParentObjects;    // корень 3D пушек (тут твиним localPosition.y)

    [SerializeField] private float _hiddenY = -30f;                 // спрятано
    [SerializeField] private float _shownY = 0f;                    // показано
    [SerializeField] private float _showDuration = 0.45f;
    [SerializeField] private float _hideDuration = 0.35f;
    [SerializeField] private ScreenCornerAnchor3D[] _screenCornerAnchor3D;

    private Tween _shipTween;

    public void LoadMode(bool isPlanetMode)
    {
        _isPlanetMode = isPlanetMode;
        // выставляем стейт без анимации
        if (_isPlanetMode)
        {
            _shipWeaponCamera.SetActive(false);
            var pos = _shipWeaponParentObjects.localPosition;
            pos.y = _hiddenY;
            _shipWeaponParentObjects.localPosition = pos;
        }
        else
        {
            _shipWeaponCamera.SetActive(true);
            var pos = _shipWeaponParentObjects.localPosition;
            pos.y = _shownY;
            _shipWeaponParentObjects.localPosition = pos;
        }
        ChangeView();
    }

    public void ChangeMode()
    {
        _isPlanetMode = !_isPlanetMode;
        ChangeView();
    }

    private void ChangeView()
    {
        _modeImage.sprite = _modeSprites[_isPlanetMode ? 0 : 1];
        _modeText.text = Language.TextStatic[_isPlanetMode ? 226 : 227];
        _modeText.color = _isPlanetMode ? Colors.GreySeven : Colors.WarningRed;

        if (_isPlanetMode)
        {
            PlayHideShipMode();
        }
        else
        {
            _uiPanelsMission.PreparePanelsToShipMode();
            PlayShowShipMode();
        }
    }
    private void PlayShowShipMode()
    {
        SetAnchorsActive(false);
        SetCanvasGroupsAlpha(0);

        // активируем камеру перед показом
        if (_shipWeaponCamera && !_shipWeaponCamera.activeSelf)
        {
            _shipWeaponCamera.SetActive(true);
        }

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

        _shipTween = _shipWeaponParentObjects.DOLocalMoveY(_hiddenY, _hideDuration).SetEase(Ease.InCubic).SetUpdate(true)
            .OnComplete(() =>
            {
                // после ухода выключаем камеру
                if (_shipWeaponCamera && _shipWeaponCamera.activeSelf)
                {
                    _shipWeaponCamera.SetActive(false);
                }
            });
    }

    private void SetAnchorsActive(bool state)
    {
        _screenCornerAnchor3D[0].enabled = state;
        _screenCornerAnchor3D[1].enabled = state;
    }

    private void SetCanvasGroupsAlpha(int alpha)
    {
        foreach (var cg in _canvasGroups)
        {
            cg.DOFade(alpha, _showDuration * 0.8f).SetEase(Ease.OutQuad).SetUpdate(true);
        }
    }

    private void OnDestroy()
    {
        _shipTween?.Kill();
    }
}
