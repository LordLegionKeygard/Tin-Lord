using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeSystem : MonoBehaviour
{
    [SerializeField] private float _daySpeed;
    [SerializeField] private int _currentTime;
    [SerializeField] private TimeView _timeView;
    [SerializeField] private TextMeshProUGUI _dayText;
    private float _endTime = 11;
    private int _currentDay = 0;
    private Coroutine _coroutine;

    private void Awake()
    {
        CustomEvents.OnPauseChanged += ToggleTimeCoroutine;
    }

    private void Start()
    {
        UpdateDayText();
        _coroutine = StartCoroutine(nameof(DayCoroutine));
    }

    private IEnumerator DayCoroutine()
    {
        yield return new WaitForSeconds(_daySpeed);
        _currentTime++;
        CustomEvents.FireTimeTick();
        if (_currentTime >= _endTime)
        {
            _currentDay++;
            _currentTime = 0;
            UpdateDayText();
        }
        _timeView.UpdateTimeSlotsView(_currentTime);

        _coroutine = StartCoroutine(nameof(DayCoroutine));
    }

    private void UpdateDayText()
    {
        _dayText.text = $"{Language.TextStatic[12]} {_currentDay}"; 
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
