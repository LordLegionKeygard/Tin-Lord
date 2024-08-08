using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSpeedSystem : MonoBehaviour
{
    [SerializeField] private bool _isPause;
    [SerializeField] private Image[] _images;
    [SerializeField] private GameSpeedEnum _currentGameSpeedEnum = GameSpeedEnum.Default;

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
        foreach (var item in _images)
        {
            item.color = Colors.AlphaGrey;
        }
        _images[(int)_currentGameSpeedEnum].color = Color.white;
    }
}

public enum GameSpeedEnum
{
    Pause = 0,
    Default = 1,
    Double = 2,
    Triple = 3,
}
