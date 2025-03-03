using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetSettings : MonoBehaviour
{
    [SerializeField] private GameObject[] _panels;

    private VideoPanel _videoPanel;
    private ControlPanel _controlPanel;
    private GamePlayPanel _gamePlayPanel;
    private AudioPanel _audioPanel;

    private void Awake()
    {
        _videoPanel = GetComponent<VideoPanel>();
        _audioPanel = GetComponent<AudioPanel>();
        _gamePlayPanel = GetComponent<GamePlayPanel>();
        _controlPanel = GetComponent<ControlPanel>();
    }

    public void ResetButton()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);

        _videoPanel.Reset();
        _controlPanel.Reset();
        _gamePlayPanel.Reset();
        _audioPanel.Reset();
    }
}
