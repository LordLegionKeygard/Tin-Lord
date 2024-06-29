using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    [SerializeField] private bool _isPause;
    public bool IsPause() => _isPause;
    public void PauseToggle()
    {
        _isPause = !_isPause;
        CustomEvents.FirePauseChanged(_isPause);
        Time.timeScale = _isPause ? 0.00001f : 1;
    }
}
