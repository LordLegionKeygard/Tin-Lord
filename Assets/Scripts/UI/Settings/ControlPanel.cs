using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Samples.RebindUI;
public class ControlPanel : MonoBehaviour
{
    [SerializeField] private InputActionAsset _inputActionAsset;
    [SerializeField] private RebindActionUI[] _rebindActions;
    private ApplySettings _applySettings;

    private void Awake()
    {
        _applySettings = GetComponent<ApplySettings>();
    }

    public void SetSettingsToData()
    {
        var rebinds = _inputActionAsset.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("rebinds", rebinds);
    }

    public void SetSettingsFromData()
    {
        var rebinds = PlayerPrefs.GetString("rebinds");
        if (!string.IsNullOrEmpty(rebinds))
        {
            _inputActionAsset.LoadBindingOverridesFromJson(rebinds);
        }
    }
    
    public void Reset()
    {
        // foreach (var rebind in _rebindActions)
        // {
        //     rebind.ResetToDefault();
        // }
        // _applySettings.ApplyToggle(true);
    }
}
