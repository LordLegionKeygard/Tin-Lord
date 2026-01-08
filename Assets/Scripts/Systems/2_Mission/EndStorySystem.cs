using FMODUnity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class EndStorySystem : MonoBehaviour
{
    [Inject] private readonly EndMissionSystem _endMissionSystem;
    [SerializeField] private GameObject _nextPage;
    [SerializeField] private GameObject _previousPage;
    [SerializeField] private GameObject _endGameCanvas;
    [SerializeField] private StudioEventEmitter _endGameMusic;
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GameObject[] _falseObjects;
    [SerializeField] private PageData[] _pagesData;
    private int _currentPageIndex;

    public void ShowEndGameStory()
    {
        foreach (var item in _falseObjects)
        {
            item.SetActive(false);
        }

        CustomEvents.FireControlFadeMusic(false, MusicType.Main);
        _endGameMusic.Play();
        _endGameCanvas.SetActive(true);
        SetupPage();
    }

    public void NextPage()
    {
        if (_currentPageIndex == _pagesData.Length - 1)
        {
            CustomEvents.FireControlFadeMusic(false, MusicType.EndStory);
            _endMissionSystem.EndGameStory();
            return;
        }
        _currentPageIndex++;
        SetupPage();
    }

    public void PreviousPage()
    {
        if (_currentPageIndex == 0) return;
        _currentPageIndex--;
        SetupPage();
    }

    private void UpdateArrow()
    {
        _previousPage.SetActive(_currentPageIndex != 0 && _currentPageIndex != _pagesData.Length - 1);
        _nextPage.SetActive(_currentPageIndex <= _pagesData.Length - 2);
    }

    public void SetupPage()
    {
        _image.sprite = _pagesData[_currentPageIndex].Sprite;
        _text.text = Language.TextStatic[_pagesData[_currentPageIndex].LanguageNumber];
        UpdateArrow();
    }
}

[System.Serializable]
public class PageData
{
    public Sprite Sprite;
    public int LanguageNumber;
}
