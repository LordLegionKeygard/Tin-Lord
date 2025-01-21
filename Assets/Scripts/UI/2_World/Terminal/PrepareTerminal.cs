using FMODUnity;
using UnityEngine;

public class PrepareTerminal : MonoBehaviour
{
    [SerializeField] private GameObject[] _falseObjects;
    [SerializeField] private SetupRenderSettings _setupRenderSettings;
    [SerializeField] private StudioEventEmitter _terminalMusic;
    [SerializeField] private CameraMoveMainMenu _cameraMoveMainMenu;
    private void OnEnable()
    {
        _cameraMoveMainMenu.enabled = false;
        // _terminalMusic.Play();
        UnactiveObjects();
        ActiveRender();
    }

    private void UnactiveObjects()
    {
        foreach (var item in _falseObjects)
        {
            item.SetActive(false);
        }
    }

    private void ActiveRender()
    {
        _setupRenderSettings.SetTerminalRender();
    }
}
