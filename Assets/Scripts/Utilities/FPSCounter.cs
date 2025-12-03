using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _text;

    [Header("Sampling")]
    [SerializeField, Tooltip("Количество последних кадров для усреднения")]
    private int _averageFromAmount = 30;

    [SerializeField, Tooltip("Использовать сглаживание (скользящее среднее)")]
    private bool _smooth = true;

    [SerializeField, Tooltip("Максимальное значение для кешированных строк")]
    private int _cacheNumbersAmount = 300;

    [SerializeField, Tooltip("Частота обновления текста в секундах")]
    private float _displayInterval = 1f;

    private readonly Dictionary<int, string> _cachedNumberStrings = new();
    private int[] _frameRateSamples;
    private int _averageCounter;
    private int _currentAveraged;
    private float _timeSinceLastDisplay;

#if UNITY_EDITOR
    private void Awake()
    {
        for (int i = 0; i < _cacheNumbersAmount; i++)
        {
            _cachedNumberStrings[i] = i.ToString();
        }

        _frameRateSamples = new int[_averageFromAmount];
    }

    private void Update()
    {
        // Используем unscaledDeltaTime, чтобы timeScale и GameSpeedSystem не искажали FPS.
        float delta = Time.unscaledDeltaTime;
        int currentFrame = delta > 0f ? Mathf.RoundToInt(1f / delta) : 0;

        if (_smooth)
        {
            _frameRateSamples[_averageCounter] = currentFrame;

            float sum = 0f;
            for (int i = 0; i < _averageFromAmount; i++)
            {
                sum += _frameRateSamples[i];
            }

            _currentAveraged = Mathf.RoundToInt(sum / _averageFromAmount);
            _averageCounter = (_averageCounter + 1) % _averageFromAmount;
        }
        else
        {
            _currentAveraged = currentFrame;
        }

        _timeSinceLastDisplay += delta;
        if (_timeSinceLastDisplay >= _displayInterval)
        {
            _timeSinceLastDisplay = 0f;
            _text.text = _currentAveraged switch
            {
                var x when x >= 0 && x < _cacheNumbersAmount => _cachedNumberStrings[x],
                var x when x >= _cacheNumbersAmount => $"> {_cacheNumbersAmount}",
                var x when x < 0 => "< 0",
                _ => "?"
            };
        }
#endif
    }
}
