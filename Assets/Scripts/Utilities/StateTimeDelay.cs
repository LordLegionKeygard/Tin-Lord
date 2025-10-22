using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateTimeDelay : MonoBehaviour
{
    [SerializeField] private GameObject _object;
    [SerializeField] private float _time;
    [SerializeField] private bool _state;
    private float _currentTime;

    private void OnEnable()
    {
        _currentTime = 0;
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime >= _time)
        {
            _object.SetActive(_state);
        }
    }
}
