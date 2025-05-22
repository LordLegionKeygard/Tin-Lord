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
    [SerializeField] private TutorialTexts[] _tutorialTexts;
    [SerializeField] private TextMeshProUGUI[] _terminalTexts;

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
        _escapeTexts[4].text = Language.TextStatic[237];

        SetTutorialTexts();
        SetTerminalTexts();
    }

    private void SetTerminalTexts()
    {
        _terminalTexts[0].text = Language.TextStatic[239];
        _terminalTexts[1].text = Language.TextStatic[240];
        _terminalTexts[2].text = Language.TextStatic[241];
        _terminalTexts[3].text = Language.TextStatic[242];
        _terminalTexts[4].text = Language.TextStatic[243];
        _terminalTexts[5].text = Language.TextStatic[244];
        _terminalTexts[6].text = Language.TextStatic[245];
        _terminalTexts[7].text = Language.TextStatic[33];
    }

    private void SetTutorialTexts()
    {
        //Interface
        _tutorialTexts[0].ButtonText.text = Language.TextStatic[183];
        _tutorialTexts[0].MainHeaderText.text = Language.TextStatic[183];

        _tutorialTexts[0].Chapters[0].ChapterHeaderText.text = Language.TextStatic[190];
        _tutorialTexts[0].Chapters[1].ChapterHeaderText.text = Language.TextStatic[191];
        _tutorialTexts[0].Chapters[2].ChapterHeaderText.text = Language.TextStatic[192];
        _tutorialTexts[0].Chapters[3].ChapterHeaderText.text = Language.TextStatic[193];
        _tutorialTexts[0].Chapters[4].ChapterHeaderText.text = Language.TextStatic[194];
        _tutorialTexts[0].Chapters[5].ChapterHeaderText.text = Language.TextStatic[141];

        _tutorialTexts[0].Chapters[0].ChapterText.text = Language.TextStatic[195];
        _tutorialTexts[0].Chapters[1].ChapterText.text = Language.TextStatic[196];
        _tutorialTexts[0].Chapters[2].ChapterText.text = Language.TextStatic[197];
        _tutorialTexts[0].Chapters[3].ChapterText.text = Language.TextStatic[198];
        _tutorialTexts[0].Chapters[4].ChapterText.text = Language.TextStatic[199];
        _tutorialTexts[0].Chapters[5].ChapterText.text = Language.TextStatic[186];


        //Resources
        _tutorialTexts[1].ButtonText.text = Language.TextStatic[7];
        _tutorialTexts[1].MainHeaderText.text = Language.TextStatic[7];

        _tutorialTexts[1].Chapters[0].ChapterHeaderText.text = Language.TextStatic[200];
        _tutorialTexts[1].Chapters[1].ChapterHeaderText.text = Language.TextStatic[201];
        _tutorialTexts[1].Chapters[2].ChapterHeaderText.text = Language.TextStatic[202];

        _tutorialTexts[1].Chapters[0].ChapterText.text = Language.TextStatic[203];
        _tutorialTexts[1].Chapters[1].ChapterText.text = Language.TextStatic[204];
        _tutorialTexts[1].Chapters[2].ChapterText.text = Language.TextStatic[205];


        //Construction
        _tutorialTexts[2].ButtonText.text = Language.TextStatic[184];
        _tutorialTexts[2].MainHeaderText.text = Language.TextStatic[184];

        _tutorialTexts[2].Chapters[0].ChapterHeaderText.text = Language.TextStatic[206];
        _tutorialTexts[2].Chapters[1].ChapterHeaderText.text = Language.TextStatic[207];
        _tutorialTexts[2].Chapters[2].ChapterHeaderText.text = Language.TextStatic[208];
        _tutorialTexts[2].Chapters[3].ChapterHeaderText.text = Language.TextStatic[209];
        _tutorialTexts[2].Chapters[4].ChapterHeaderText.text = Language.TextStatic[210];
        _tutorialTexts[2].Chapters[5].ChapterHeaderText.text = Language.TextStatic[211];

        _tutorialTexts[2].Chapters[0].ChapterText.text = Language.TextStatic[212];
        _tutorialTexts[2].Chapters[1].ChapterText.text = Language.TextStatic[213];
        _tutorialTexts[2].Chapters[2].ChapterText.text = Language.TextStatic[214];
        _tutorialTexts[2].Chapters[3].ChapterText.text = Language.TextStatic[215];
        _tutorialTexts[2].Chapters[4].ChapterText.text = Language.TextStatic[216];
        _tutorialTexts[2].Chapters[5].ChapterText.text = Language.TextStatic[217];


        //WhereStart
        _tutorialTexts[3].ButtonText.text = Language.TextStatic[185];
        _tutorialTexts[3].MainHeaderText.text = Language.TextStatic[185];

        _tutorialTexts[3].Chapters[0].ChapterHeaderText.text = Language.TextStatic[218];
        _tutorialTexts[3].Chapters[1].ChapterHeaderText.text = Language.TextStatic[219];
        _tutorialTexts[3].Chapters[2].ChapterHeaderText.text = Language.TextStatic[220];
        _tutorialTexts[3].Chapters[3].ChapterHeaderText.text = Language.TextStatic[221];


        _tutorialTexts[3].Chapters[0].ChapterText.text = Language.TextStatic[222];
        _tutorialTexts[3].Chapters[1].ChapterText.text = Language.TextStatic[223];
        _tutorialTexts[3].Chapters[2].ChapterText.text = Language.TextStatic[224];
        _tutorialTexts[3].Chapters[3].ChapterText.text = Language.TextStatic[225];


        //TileCombinations
        _tutorialTexts[4].ButtonText.text = Language.TextStatic[187];
        _tutorialTexts[4].MainHeaderText.text = Language.TextStatic[187];

        _tutorialTexts[4].Chapters[0].ChapterHeaderText.text = Language.TextStatic[226];
        _tutorialTexts[4].Chapters[1].ChapterHeaderText.text = Language.TextStatic[227];


        _tutorialTexts[4].Chapters[0].ChapterText.text = Language.TextStatic[228];
        _tutorialTexts[4].Chapters[1].ChapterText.text = Language.TextStatic[229];

        //Research
        _tutorialTexts[5].ButtonText.text = Language.TextStatic[188];
        _tutorialTexts[5].MainHeaderText.text = Language.TextStatic[188];

        _tutorialTexts[5].Chapters[0].ChapterHeaderText.text = Language.TextStatic[230];

        _tutorialTexts[5].Chapters[0].ChapterText.text = Language.TextStatic[231];


        //Missions
        _tutorialTexts[6].ButtonText.text = Language.TextStatic[189];
        _tutorialTexts[6].MainHeaderText.text = Language.TextStatic[189];

        _tutorialTexts[6].Chapters[0].ChapterHeaderText.text = Language.TextStatic[232];
        _tutorialTexts[6].Chapters[1].ChapterHeaderText.text = Language.TextStatic[233];


        _tutorialTexts[6].Chapters[0].ChapterText.text = Language.TextStatic[234];
        _tutorialTexts[6].Chapters[1].ChapterText.text = Language.TextStatic[235];
    }
}

[System.Serializable]
public class TutorialTexts
{
    public TutorialEnum TutorialEnum;
    public TextMeshProUGUI ButtonText;
    public TextMeshProUGUI MainHeaderText;
    public TutorialChapters[] Chapters;
}

[System.Serializable]
public class TutorialChapters
{
    public TextMeshProUGUI ChapterHeaderText;
    public TextMeshProUGUI ChapterText;
}

public enum TutorialEnum
{
    Interface = 0,
    Resources = 1,
    Construction = 2,
    WhereStart = 3,
    TileCombinations = 4,
    Research = 5,
    Missions = 6
}
