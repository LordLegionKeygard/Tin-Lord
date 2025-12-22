using TMPro;
using UnityEngine;

public class ChangeLanguageHangar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _mainButtonsText;
    [SerializeField] private TextMeshProUGUI _areYouSureText;
    [SerializeField] private TextMeshProUGUI[] _hangarButtonsText;
    [SerializeField] private TextMeshProUGUI[] _buyTexts;

    private void Start()
    {
        _mainButtonsText[0].text = Language.TextStatic[26];
        _mainButtonsText[1].text = Language.TextStatic[27];
        _mainButtonsText[2].text = Language.TextStatic[28];
        _mainButtonsText[3].text = Language.TextStatic[29];

        _areYouSureText.text = Language.TextStatic[31];

        _hangarButtonsText[0].text = Language.TextStatic[77];
        _hangarButtonsText[1].text = Language.TextStatic[78];

        foreach (var item in _buyTexts)
        {
            item.text = Language.TextStatic[289];
        }
    }
}
