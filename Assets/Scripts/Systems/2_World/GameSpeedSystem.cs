using UnityEngine;
using UnityEngine.UI;

public class GameSpeedSystem : MonoBehaviour
{
    [SerializeField] private Image[] _images;
    [SerializeField] private Sprite[] _spriteOn;
    [SerializeField] private Sprite[] _spriteOff;
    [SerializeField] private Button[] _speedButtons;
    private GameSpeedEnum _currentGameSpeedEnum = GameSpeedEnum.Default;
    public GameSpeedEnum CurrentGameSpeedEnum() => _currentGameSpeedEnum;
    private bool _isPause;
    public bool IsPause() => _isPause;
    private bool _canChangeGameSpeed = true;

    public void InputChangeGameSpeed(int gameSpeed)
    {
        if (!_canChangeGameSpeed) return;
        ChangeGameSpeedButton(gameSpeed, false);
    }

    public void ChangeGameSpeedButton(int gameSpeed, bool isEscapePanel)
    {
        //нажали на ту же скорость что щас уже стоит и это не пауза, делаем возврат
        if (_currentGameSpeedEnum != GameSpeedEnum.Pause && (int)_currentGameSpeedEnum == gameSpeed) return;

        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.GameSpeed], transform.position);

        //нажали на escape, но щас уже стоит пауза, обновляем ивент паузы и делаем возврат
        if (_currentGameSpeedEnum == GameSpeedEnum.Pause && gameSpeed == 0 && isEscapePanel)
        {
            CustomEvents.FireCheckPause(_isPause);
            return;
        }

        ChangeGameSpeed(gameSpeed);
    }

    public void ChangeGameSpeed(int gameSpeed)
    {
        GameSpeedEnum gameSpeedEnum = (GameSpeedEnum)gameSpeed;

        switch (gameSpeedEnum)
        {
            case GameSpeedEnum.Pause:
                _isPause = !_isPause;
                Time.timeScale = _isPause ? WorldGameInfo.PausedTimeScale : WorldGameInfo.DefaultTimeScale;
                _currentGameSpeedEnum = _isPause ? GameSpeedEnum.Pause : GameSpeedEnum.Default;
                break;
            case GameSpeedEnum.Default:
                _isPause = false;
                Time.timeScale = WorldGameInfo.DefaultTimeScale;
                _currentGameSpeedEnum = gameSpeedEnum;
                break;
            case GameSpeedEnum.Double:
                _isPause = false;
                Time.timeScale = WorldGameInfo.DoubleTimeScale;
                _currentGameSpeedEnum = gameSpeedEnum;
                break;
            case GameSpeedEnum.Triple:
                _isPause = false;
                Time.timeScale = WorldGameInfo.TripleTimeScale;
                _currentGameSpeedEnum = gameSpeedEnum;
                break;
        }
        CustomEvents.FireCheckPause(_isPause);
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
        _canChangeGameSpeed = !escapePanelIsOpen;
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
