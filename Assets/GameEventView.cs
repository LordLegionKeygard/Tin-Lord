using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEventView : MonoBehaviour
{
    [SerializeField] private Image _image;
    public RectTransform rectTransform;
    private Vector2 startPosition;
    private Vector2 endPosition;
    private float duration;
    private float elapsedTime = 0f;
    private bool isMoving = false;
    private GameEventInfo _gameEventInfo;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void Initialize(GameEventInfo gameEventInfo, Vector2 startPos, Vector2 endPos, float moveDuration)
    {
        _gameEventInfo = gameEventInfo;
        _image.sprite = _gameEventInfo.EventIcon;
        startPosition = startPos;
        endPosition = endPos;
        duration = moveDuration;
        elapsedTime = 0f;
        isMoving = true;
    }

    private void Update()
    {
        if (isMoving)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            if (t >= 1f)
            {
                t = 1f;
                isMoving = false;
                OnReachedEnd();
            }
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, endPosition, t);
        }
    }

    private void OnReachedEnd()
    {
        CustomEvents.FireGameEventStart(_gameEventInfo.GameEventType);
        Destroy(gameObject);
    }
}
