using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ErrorDetector : MonoBehaviour
{
    [SerializeField] private GameObject _errorDetecingUI;
    [SerializeField] private TextMeshProUGUI _errorText;
    [SerializeField] private Button _copyButton;
    private List<string> _ignoreErrorStringList = new List<string>();

    private void Awake()
    {
        _copyButton.onClick.AddListener(() => { GUIUtility.systemCopyBuffer = _errorText.text; });
    }

    private void Start()
    {
        Application.logMessageReceived += LogReceived;
    }

    private void LogReceived(string condition, string stacktrace, LogType type)
    {
        if (type is LogType.Error or LogType.Exception)
        {
            _errorText.text = $"{type}: {condition} | {stacktrace}\n";

            if (_ignoreErrorStringList.Contains(_errorText.text)) return;
            _ignoreErrorStringList.Add(_errorText.text);

            TogglePanel(true);
        }
    }

    public void Copy()
    {
        GUIUtility.systemCopyBuffer = _errorText.text;
    }

    public void TogglePanel(bool state)
    {
        _errorDetecingUI.SetActive(state);
    }

    private void OnDestroy()
    {
        Application.logMessageReceived -= LogReceived;
    }
}
