using UnityEngine;
using TMPro;

public class ChangeLanguage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _buildButtonText;
    [SerializeField] private TextMeshProUGUI _upgradeButtonText;

    [SerializeField] private TextMeshProUGUI[] _resourceTypesPanel;

    private void Start()
    {
        _buildButtonText.text = Language.TextStatic[4];
        _upgradeButtonText.text = Language.TextStatic[5];

        _resourceTypesPanel[0].text = Language.TextStatic[7];
        _resourceTypesPanel[1].text = Language.TextStatic[8];
        _resourceTypesPanel[2].text = Language.TextStatic[9];
    }
}
