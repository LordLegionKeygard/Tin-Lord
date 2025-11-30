using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private DeltaTimeType _deltaType;
    private Dictionary<int, string> _cachedNumberStrings = new();
    private int[] _frameRateSamples;
    private int _cacheNumbersAmount = 300;
    private int _averageFromAmount = 30;
    private int _averageCounter;
    private int _currentAveraged;

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
        var currentFrame = (int)Math.Round(1f / _deltaType switch
        {
            DeltaTimeType.Smooth => Time.smoothDeltaTime,
            DeltaTimeType.Unscaled => Time.unscaledDeltaTime,
            _ => Time.unscaledDeltaTime
        });
        _frameRateSamples[_averageCounter] = currentFrame;

        var average = 0f;

        foreach (var frameRate in _frameRateSamples)
        {
            average += frameRate;
        }

        _currentAveraged = (int)Math.Round(average / _averageFromAmount);
        _averageCounter = (_averageCounter + 1) % _averageFromAmount;

        _text.text = _currentAveraged switch
        {
            var x when x >= 0 && x < _cacheNumbersAmount => _cachedNumberStrings[x],
            var x when x >= _cacheNumbersAmount => $"> {_cacheNumbersAmount}",
            var x when x < 0 => "< 0",
            _ => "?"
        };
#endif
    }
}

public enum DeltaTimeType
{
    Smooth,
    Unscaled
}