using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEventView : MonoBehaviour
{
    [SerializeField] private Image _image;
    public RectTransform _rectTransform;
    private Vector2 _startPosition;
    private Vector2 _endPosition;
    private float _duration;
    private float _elapsedTime = 0f;
    private bool _isMoving = false;
    private GameEventInfo _gameEventInfo;
    private int _eventNumber;
    public float GetAlreadyElapsedTime() => _elapsedTime;


    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Initialize(GameEventInfo gameEventInfo, Vector2 startPos, Vector2 endPos, float fullDuration, float alreadyElapsedTime, int eventNumber)
    {
        _eventNumber = eventNumber;
        _rectTransform.anchoredPosition = startPos;
        _gameEventInfo = gameEventInfo;
        _image.sprite = _gameEventInfo.EventIcon;
        _startPosition = startPos;
        _endPosition = endPos;
        _duration = fullDuration;
        _elapsedTime = alreadyElapsedTime;
        _isMoving = true;
    }

    private void Update()
    {
        if (_isMoving)
        {
            _elapsedTime += Time.deltaTime;
            float t = _elapsedTime / _duration;
            if (t >= 1f)
            {
                t = 1f;
                _isMoving = false;
                OnReachedEnd();
            }
            _rectTransform.anchoredPosition = Vector2.Lerp(_startPosition, _endPosition, t);
        }
    }

    private void OnReachedEnd()
    {
        CustomEvents.FireGameEventStart(_gameEventInfo.GameEventType, _eventNumber);
        Destroy(gameObject);
    }
}
