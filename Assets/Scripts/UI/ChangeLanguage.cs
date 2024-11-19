using UnityEngine;
using TMPro;

public class ChangeLanguage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _buildingsText;
    [SerializeField] private TextMeshProUGUI[] _resourceTypesPanel;
    [SerializeField] private TextMeshProUGUI _receptText;
    [SerializeField] private TextMeshProUGUI _robotPanelText;

    private void Start()
    {
        _buildingsText.text = Language.TextStatic[13];

        _resourceTypesPanel[0].text = Language.TextStatic[7];
        _resourceTypesPanel[1].text = Language.TextStatic[8];
        _resourceTypesPanel[2].text = Language.TextStatic[9];

        _receptText.text = Language.TextStatic[1];
        _robotPanelText.text = Language.TextStatic[21];

    }
}
