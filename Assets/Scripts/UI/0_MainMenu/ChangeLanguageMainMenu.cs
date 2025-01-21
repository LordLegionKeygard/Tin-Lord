using TMPro;
using UnityEngine;

public class ChangeLanguageMainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _mainButtonsText;
    [SerializeField] private TextMeshProUGUI _areYouSureText;
    // [SerializeField] private TextMeshProUGUI _terminalContinueText;
    // [SerializeField] private TextMeshProUGUI _terminalHeaderText;
    // [SerializeField] private TextMeshProUGUI _icosaCorpText;
    // [SerializeField] private TextMeshProUGUI _sloganText;
    // [SerializeField] private TextMeshProUGUI _coordinatesHeaderText;
    // [SerializeField] private TextMeshProUGUI _signalHeaderText;
    // [SerializeField] private TextMeshProUGUI _diagramHeaderText;
    // [SerializeField] private TextMeshProUGUI _diagramText;



    private void Start()
    {
        _mainButtonsText[0].text = Language.TextStatic[26];
        _mainButtonsText[1].text = Language.TextStatic[27];
        _mainButtonsText[2].text = Language.TextStatic[28];
        _mainButtonsText[3].text = Language.TextStatic[29];

        _areYouSureText.text = Language.TextStatic[31];

        // _terminalContinueText.text = Language.TextStatic[33];
        // _terminalHeaderText.text = Language.TextStatic[38];
        // _icosaCorpText.text = Language.TextStatic[90];
        // _sloganText.text = Language.TextStatic[91];
        // _coordinatesHeaderText.text = Language.TextStatic[92];
        // _signalHeaderText.text = Language.TextStatic[93];
        // _diagramHeaderText.text = Language.TextStatic[94];
        // _diagramText.text = Language.TextStatic[95];
    }

}
