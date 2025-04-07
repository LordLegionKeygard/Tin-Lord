using UnityEngine;
using TMPro;

public class ChangeLanguageWorld : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _buildingsText;
    [SerializeField] private TextMeshProUGUI[] _resourceTypesPanel;
    [SerializeField] private TextMeshProUGUI _receptText;
    [SerializeField] private TextMeshProUGUI _machinePanelText;
    [SerializeField] private TextMeshProUGUI _skillsPanelHeaderText;
    [SerializeField] private TextMeshProUGUI _continueButtonText;
    
    [SerializeField] private TextMeshProUGUI[] _escapeTexts;

    private void Start()
    {
        _buildingsText.text = Language.TextStatic[13];

        _resourceTypesPanel[0].text = Language.TextStatic[7];
        _resourceTypesPanel[1].text = Language.TextStatic[8];
        _resourceTypesPanel[2].text = Language.TextStatic[9];
        _resourceTypesPanel[3].text = Language.TextStatic[17];

        _receptText.text = $"{Language.TextStatic[1]}:";
        _machinePanelText.text = Language.TextStatic[21];
        _continueButtonText.text = Language.TextStatic[33];
        _skillsPanelHeaderText.text = Language.TextStatic[179];

        _escapeTexts[0].text = Language.TextStatic[45];
        _escapeTexts[1].text = Language.TextStatic[44];
        _escapeTexts[2].text = Language.TextStatic[28];
        _escapeTexts[3].text = Language.TextStatic[46];
    }
}
