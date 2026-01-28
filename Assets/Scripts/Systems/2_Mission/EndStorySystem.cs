using FMODUnity;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using System.Collections;

public class EndStorySystem : MonoBehaviour
{
    [Inject] private readonly EndMissionSystem _endMissionSystem;
    [SerializeField] private GameObject _endGameCanvas;
    [SerializeField] private StudioEventEmitter _endGameMusic;
    [SerializeField] private Image _image;
    [SerializeField] private Image _blackImage;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private RectTransform _textPanel;
    [SerializeField] private GameObject[] _falseObjects;
    [SerializeField] private PageData[] _pagesData;
    private float _pageAnimationDuration = 12;
    private float _blackFadeDuration = 3;
    private int _currentPageIndex;
    private Sequence _pageSequence;
    private bool _fadeOutAtStart;
    private TextPanelLayout _textPanelDefault;
    private bool _hasTextPanelDefault;

    private void Start()
    {
        StartCoroutine(StartWithDelay());
    }

    private IEnumerator StartWithDelay()
    {
        yield return new WaitForSeconds(5f);
        ShowEndGameStory();
    }

    public void ShowEndGameStory()
    {
        foreach (var item in _falseObjects)
        {
            item.SetActive(false);
        }

        CustomEvents.FireControlFadeMusic(false, MusicType.Main);
        _endGameMusic.Play();
        _endGameCanvas.SetActive(true);
        StartStory();
    }

    private void StartStory()
    {
        if (_pagesData == null || _pagesData.Length == 0) return;
        KillSequence();
        CacheTextPanelDefault();

        _currentPageIndex = 0;
        _fadeOutAtStart = false;
        SetBlackAlpha(0f);
        PlayCurrentPage();
    }

    private void PlayCurrentPage()
    {
        var page = _pagesData[_currentPageIndex];
        SetupPage(page);

        _pageSequence = DOTween.Sequence().SetUpdate(true);
        _pageSequence.AppendInterval(_pageAnimationDuration);

        if (_fadeOutAtStart && _blackImage != null)
        {
            SetBlackAlpha(1f);
            _pageSequence.Insert(0f, _blackImage.DOFade(0f, _blackFadeDuration).SetEase(Ease.Linear).SetUpdate(true));
        }
        _fadeOutAtStart = false;

        AddAnimationTweens(_pageSequence, page);

        bool hasNextPage = _currentPageIndex < _pagesData.Length - 1;
        if (hasNextPage)
        {
            if (_blackImage != null)
            {
                float fadeStartTime = Mathf.Max(0f, _pageAnimationDuration - _blackFadeDuration);
                _pageSequence.Insert(fadeStartTime, _blackImage.DOFade(1f, _blackFadeDuration).SetEase(Ease.Linear).SetUpdate(true));
            }
            _pageSequence.OnComplete(() =>
            {
                _currentPageIndex++;
                _fadeOutAtStart = true;
                PlayCurrentPage();
            });
        }
        else
        {
            _pageSequence.OnComplete(() =>
            {
                CustomEvents.FireControlFadeMusic(false, MusicType.EndStory);
                _endMissionSystem.EndGameStory();
            });
        }
    }

    private void AddAnimationTweens(Sequence sequence, PageData page)
    {
        var rectTransform = _image.rectTransform;

        if (page.StartScale != page.EndScale)
        {
            sequence.Join(rectTransform.DOScale(page.EndScale, _pageAnimationDuration).SetUpdate(true));
        }

        if (!Mathf.Approximately(page.StartPosition.x, page.EndPosition.x))
        {
            sequence.Join(rectTransform.DOAnchorPosX(page.EndPosition.x, _pageAnimationDuration).SetUpdate(true));
        }

        if (!Mathf.Approximately(page.StartPosition.y, page.EndPosition.y))
        {
            sequence.Join(rectTransform.DOAnchorPosY(page.EndPosition.y, _pageAnimationDuration).SetUpdate(true));
        }
    }

    private void SetupPage(PageData page)
    {
        _image.sprite = page.Sprite;
        _text.text = Language.TextStatic[page.LanguageNumber];

        var rectTransform = _image.rectTransform;
        rectTransform.anchoredPosition = page.StartPosition;
        rectTransform.localScale = page.StartScale;

        bool isLastPage = _currentPageIndex == _pagesData.Length - 1;
        SetupTextPanelForPage(isLastPage);
    }

    private void SetBlackAlpha(float alpha)
    {
        if (_blackImage == null) return;
        Color color = _blackImage.color;
        color.a = alpha;
        _blackImage.color = color;
    }

    private void KillSequence()
    {
        if (_pageSequence != null && _pageSequence.IsActive())
        {
            _pageSequence.Kill();
            _pageSequence = null;
        }
    }

    private void CacheTextPanelDefault()
    {
        if (_textPanel == null || _hasTextPanelDefault) return;

        _textPanelDefault = new TextPanelLayout
        {
            AnchorMin = _textPanel.anchorMin,
            AnchorMax = _textPanel.anchorMax,
            Pivot = _textPanel.pivot,
            AnchoredPosition = _textPanel.anchoredPosition,
            SizeDelta = _textPanel.sizeDelta,
            LocalScale = _textPanel.localScale
        };
        _hasTextPanelDefault = true;
    }

    private void SetupTextPanelForPage(bool isLastPage)
    {
        if (_textPanel == null || !_hasTextPanelDefault) return;

        if (isLastPage)
        {
            _textPanel.anchorMin = new Vector2(0f, 0.5f);
            _textPanel.anchorMax = new Vector2(1f, 0.5f);
            _textPanel.pivot = new Vector2(0.5f, 0.5f);
            _textPanel.anchoredPosition = Vector2.zero;
            _textPanel.sizeDelta = new Vector2(_textPanelDefault.SizeDelta.x, _textPanelDefault.SizeDelta.y);
            _textPanel.localScale = _textPanelDefault.LocalScale;
        }
        else
        {
            _textPanel.anchorMin = _textPanelDefault.AnchorMin;
            _textPanel.anchorMax = _textPanelDefault.AnchorMax;
            _textPanel.pivot = _textPanelDefault.Pivot;
            _textPanel.anchoredPosition = _textPanelDefault.AnchoredPosition;
            _textPanel.sizeDelta = _textPanelDefault.SizeDelta;
            _textPanel.localScale = _textPanelDefault.LocalScale;
        }
    }
}

public struct TextPanelLayout
{
    public Vector2 AnchorMin;
    public Vector2 AnchorMax;
    public Vector2 Pivot;
    public Vector2 AnchoredPosition;
    public Vector2 SizeDelta;
    public Vector3 LocalScale;
}

[System.Serializable]
public class PageData
{
    public Sprite Sprite;
    public int LanguageNumber;
    public Vector2 StartPosition;
    public Vector2 EndPosition;
    public Vector3 StartScale = Vector3.one;
    public Vector3 EndScale = Vector3.one;
}

[System.Serializable]
public enum PageAnimation
{

}
