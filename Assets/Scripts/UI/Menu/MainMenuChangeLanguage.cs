using TMPro;
using UnityEngine;

public class MainMenuChangeLanguage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _mainButtonsText;
    [SerializeField] private TextMeshProUGUI _areYouSuretext;

    private void Start()
    {
        _mainButtonsText[0].text = Language.TextStatic[26];
        _mainButtonsText[1].text = Language.TextStatic[27];
        _mainButtonsText[2].text = Language.TextStatic[28];
        _mainButtonsText[3].text = Language.TextStatic[29];
        _areYouSuretext.text = Language.TextStatic[31];
    }

}
