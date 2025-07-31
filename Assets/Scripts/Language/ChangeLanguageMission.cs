using UnityEngine;
using TMPro;

public class ChangeLanguageMission : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _tutorialContinue;
    [SerializeField] private TextMeshProUGUI _buildingsText;
    [SerializeField] private TextMeshProUGUI[] _resourceTypesPanel;
    [SerializeField] private TextMeshProUGUI _receptText;
    [SerializeField] private TextMeshProUGUI _machinePanelText;
    [SerializeField] private TextMeshProUGUI _skillsPanelHeaderText;
    [SerializeField] private TextMeshProUGUI _continueButtonText;
    [SerializeField] private TextMeshProUGUI[] _escapeTexts;
    [SerializeField] private TextMeshProUGUI[] _terminalTexts;

    private void Start()
    {
        _tutorialContinue.text = Language.TextStatic[33];
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

        SetTerminalTexts();
    }

    private void SetTerminalTexts()
    {
        _terminalTexts[0].text = Language.TextStatic[278];
        _terminalTexts[1].text = Language.TextStatic[279];
        _terminalTexts[2].text = Language.TextStatic[280];
        _terminalTexts[3].text = Language.TextStatic[281];
        _terminalTexts[4].text = Language.TextStatic[282];
        _terminalTexts[5].text = Language.TextStatic[283];
        _terminalTexts[6].text = Language.TextStatic[284];
        _terminalTexts[7].text = Language.TextStatic[33];
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
