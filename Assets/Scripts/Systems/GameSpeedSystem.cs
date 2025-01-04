using UnityEngine;
using UnityEngine.UI;

public class GameSpeedSystem : MonoBehaviour
{
    [SerializeField] private Image[] _images;
    [SerializeField] private Sprite[] _spriteOn;
    [SerializeField] private Sprite[] _spriteOff;
    [SerializeField] private Button[] _speedButtons; 
    private GameSpeedEnum _currentGameSpeedEnum = GameSpeedEnum.Default;
    private bool _isPause;
    public bool IsPause() => _isPause;

    public void ChangeGameSpeed(int gameSpeed)
    {
        GameSpeedEnum gameSpeedEnum = (GameSpeedEnum)gameSpeed;

        switch (gameSpeedEnum)
        {
            case GameSpeedEnum.Pause:
                _isPause = !_isPause;
                Time.timeScale = _isPause ? WorldGameInfo.PausedTimeScale : (int)GameSpeedEnum.Default;
                _currentGameSpeedEnum = _isPause ? GameSpeedEnum.Pause : GameSpeedEnum.Default;
                break;
            case GameSpeedEnum.Default or GameSpeedEnum.Double or GameSpeedEnum.Triple:
                _isPause = false;
                Time.timeScale = (int)gameSpeedEnum;
                _currentGameSpeedEnum = gameSpeedEnum;
                break;
        }
        CustomEvents.FirePauseChanged(_isPause);
        UpdateGameSpeedView();
    }

    private void UpdateGameSpeedView()
    {
        for (int i = 0; i < _images.Length; i++)
        {
            _images[i].sprite = _spriteOff[i];
        }

        _images[(int)_currentGameSpeedEnum].sprite = _spriteOn[(int)_currentGameSpeedEnum];
    }

    public void SpeedButtonInteractableToggle(bool escapePanelIsOpen)
    {
        foreach (var item in _speedButtons)
        {
            item.interactable = !escapePanelIsOpen;
        }
    }
}

public enum GameSpeedEnum
{
    Pause = 0,
    Default = 1,
    Double = 2,
    Triple = 3,
}
