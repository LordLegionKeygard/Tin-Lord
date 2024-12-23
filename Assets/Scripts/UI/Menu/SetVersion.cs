using UnityEngine;
using TMPro;

public class SetVersion : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _versionText;

    private void Start()
    {
        _versionText.text = $"v{Application.version}";
    }
}
