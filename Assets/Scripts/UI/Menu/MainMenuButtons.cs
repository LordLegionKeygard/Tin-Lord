using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject _settingsPanel;
    public void OpenSettings()
    {
        _settingsPanel.SetActive(true);
        // AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.DefaultClick, transform.position);
    }


    public void QuitButton()
    {
        // AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.DefaultClick, transform.position);
        Application.Quit();
    }
}
