using UnityEngine;
using TMPro;

public class SettingsLanguage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _mainButtonsTexts;
    [SerializeField] private TextMeshProUGUI[] _videoTexts;
    [SerializeField] private TMP_Dropdown[] _videoDropDowns;
    [SerializeField] private TextMeshProUGUI[] _controlTexts;
    [SerializeField] private TextMeshProUGUI[] _gameplayTexts;
    [SerializeField] private TextMeshProUGUI[] _audioTexts;



    private void Awake()
    {
        _mainButtonsTexts[0].text = Language.TextStatic[110];
        _mainButtonsTexts[1].text = Language.TextStatic[111];
        _mainButtonsTexts[2].text = Language.TextStatic[112];
        _mainButtonsTexts[3].text = Language.TextStatic[113];
        _mainButtonsTexts[4].text = Language.TextStatic[121];
        _mainButtonsTexts[5].text = Language.TextStatic[122];
        _mainButtonsTexts[6].text = Language.TextStatic[123];

        _videoTexts[0].text = Language.TextStatic[114];
        _videoTexts[1].text = Language.TextStatic[115];
        _videoTexts[2].text = Language.TextStatic[116];
        _videoTexts[3].text = Language.TextStatic[117];
        _videoTexts[4].text = Language.TextStatic[118];
        _videoTexts[5].text = Language.TextStatic[119];
        _videoTexts[6].text = Language.TextStatic[120];

        _videoDropDowns[0].options[0].text = Language.TextStatic[124];
        _videoDropDowns[0].options[1].text = Language.TextStatic[125];
        _videoDropDowns[0].options[2].text = Language.TextStatic[103];

        _videoDropDowns[1].options[0].text = Language.TextStatic[126];
        _videoDropDowns[1].options[1].text = Language.TextStatic[127];
        _videoDropDowns[1].options[2].text = Language.TextStatic[128];
        _videoDropDowns[1].options[3].text = Language.TextStatic[129];

        _videoDropDowns[2].options[0].text = Language.TextStatic[130];

        _videoDropDowns[3].options[0].text = Language.TextStatic[131];
        _videoDropDowns[3].options[1].text = Language.TextStatic[132];

        _controlTexts[0].text = Language.TextStatic[133];
        _controlTexts[1].text = Language.TextStatic[134];
        _controlTexts[2].text = Language.TextStatic[135];
        _controlTexts[3].text = Language.TextStatic[136];
        _controlTexts[4].text = Language.TextStatic[137];
        _controlTexts[5].text = Language.TextStatic[138];
        _controlTexts[6].text = Language.TextStatic[139];
        _controlTexts[7].text = Language.TextStatic[140];
        _controlTexts[8].text = Language.TextStatic[141];
        _controlTexts[9].text = Language.TextStatic[142];
        _controlTexts[10].text = Language.TextStatic[143];
        _controlTexts[11].text = Language.TextStatic[144];
        _controlTexts[12].text = Language.TextStatic[145];
        _controlTexts[13].text = Language.TextStatic[146];
        _controlTexts[14].text = Language.TextStatic[178];
        _controlTexts[15].text = Language.TextStatic[238];
        _controlTexts[16].text = Language.TextStatic[234];

        _gameplayTexts[0].text = Language.TextStatic[104];
        _gameplayTexts[1].text = Language.TextStatic[109];

        _audioTexts[0].text = Language.TextStatic[105];
        _audioTexts[1].text = Language.TextStatic[106];
        _audioTexts[2].text = Language.TextStatic[107];
        _audioTexts[3].text = Language.TextStatic[108];
    }
}
