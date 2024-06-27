using UnityEngine;
using TMPro;

public class ChangeLanguage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _buildButtonText;
    [SerializeField] private TextMeshProUGUI _upgradeButtonText;

    private void Start()
    {
        _buildButtonText.text = Language.TextStatic[4];
        _upgradeButtonText.text = Language.TextStatic[5];
    }
}
