using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSystem : MonoBehaviour
{
    [SerializeField] private float _daySpeed;
    [SerializeField] private int _currentTime;
    [SerializeField] private TimeView _timeView;
    private float _endTime = 11;
    private Coroutine _coroutine;

    private void Awake()
    {
        CustomEvents.OnPauseChanged += ToggleTimeCoroutine;
    }

    private void Start()
    {
        _coroutine = StartCoroutine(nameof(DayCoroutine));
    }

    private IEnumerator DayCoroutine()
    {
        yield return new WaitForSeconds(_daySpeed);
        _currentTime++;
        if (_currentTime >= _endTime)
        {
            CustomEvents.FireTheDayIsOver();
            _currentTime = 0;
        }
        _timeView.UpdateTimeSlotsView(_currentTime);

        _coroutine = StartCoroutine(nameof(DayCoroutine));
    }

    public void ToggleTimeCoroutine(bool isPause)
    {
        if (isPause) StopCoroutine(_coroutine);
        else _coroutine = StartCoroutine(nameof(DayCoroutine));
    }

    private void OnDestroy()
    {
        CustomEvents.OnPauseChanged -= ToggleTimeCoroutine;
    }
}
