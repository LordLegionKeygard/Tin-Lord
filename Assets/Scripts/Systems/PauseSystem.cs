using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    [SerializeField] private bool _isPause;
    public bool IsPause() => _isPause;
    [SerializeField] private TimeSystem _timeSystem;
    public void PauseToggle()
    {
        _isPause = !_isPause;
        _timeSystem.ToggleTimeCoroutine(_isPause);
        Time.timeScale = _isPause ? 0.00001f : 1;
    }
}
