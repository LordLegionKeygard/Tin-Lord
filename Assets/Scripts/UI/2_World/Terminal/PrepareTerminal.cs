using FMODUnity;
using UnityEngine;

public class PrepareTerminal : MonoBehaviour
{
    [SerializeField] private GameObject[] _falseObjects;
    [SerializeField] private SetupRenderSettings _setupRenderSettings;

    private void OnEnable()
    {
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
