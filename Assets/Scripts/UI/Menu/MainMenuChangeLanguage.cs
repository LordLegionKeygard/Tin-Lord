using TMPro;
using UnityEngine;

public class MainMenuChangeLanguage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _mainButtonsText;

    private void Start()
    {
        _mainButtonsText[0].text = Language.TextStatic[26];
        _mainButtonsText[1].text = Language.TextStatic[27];
        _mainButtonsText[2].text = Language.TextStatic[28];
        _mainButtonsText[3].text = Language.TextStatic[29];
    }

}
