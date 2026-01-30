using TMPro;
using UnityEngine;

public class ChangeLanguageSpace : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _tutorialContinue;
    [SerializeField] private TextMeshProUGUI _tutorialClose;
    [SerializeField] private TextMeshProUGUI[] _resourceTypesPanel;
    [SerializeField] private TextMeshProUGUI _mapText;
    [SerializeField] private TextMeshProUGUI _learningText;
    [SerializeField] private TextMeshProUGUI[] _buildingTypesNameTexts;
    [SerializeField] private Tile[] _buildingTypesTiles;
    [SerializeField] private TextMeshProUGUI[] _escapeTexts;

    [Header("SelectBuildingInfoPanel")]
    [SerializeField] private TextMeshProUGUI _selectBuildingText;

    [Header("ResourceTraderPanel")]
    [SerializeField] private TextMeshProUGUI _resourceTraderHeaderText;
    [SerializeField] private TextMeshProUGUI _resourceTraderPriceText;
    [SerializeField] private TextMeshProUGUI _resourceTradeBuyText;

    [Header("SkillTraderPanel")]
    [SerializeField] private TextMeshProUGUI _skillTraderHeaderText;
    [SerializeField] private TextMeshProUGUI _skillTraderPriceText;
    [SerializeField] private TextMeshProUGUI _skillTraderBuyText;

    [Header("WeaponEngineerPanel")]
    [SerializeField] private TextMeshProUGUI _weaponEngineerHeaderText;
    [SerializeField] private TextMeshProUGUI _weaponEngineerPriceText;
    [SerializeField] private TextMeshProUGUI _weaponEngineerUpgradeText;


    private void Start()
    {
        _tutorialContinue.text = Language.TextStatic[33];
        _tutorialClose.text = Language.TextStatic[121];

        _resourceTraderHeaderText.text = Language.TextStatic[287];
        _resourceTraderPriceText.text = Language.TextStatic[288];
        _resourceTradeBuyText.text = Language.TextStatic[289];

        _skillTraderHeaderText.text = Language.TextStatic[291];
        _skillTraderPriceText.text = Language.TextStatic[288];
        _skillTraderBuyText.text = Language.TextStatic[289];

        _weaponEngineerHeaderText.text = Language.TextStatic[296];
        _weaponEngineerPriceText.text = Language.TextStatic[288];
        _weaponEngineerUpgradeText.text = Language.TextStatic[232];

        _resourceTypesPanel[0].text = Language.TextStatic[7];
        _resourceTypesPanel[1].text = Language.TextStatic[8];
        _resourceTypesPanel[2].text = Language.TextStatic[9];
        _resourceTypesPanel[3].text = Language.TextStatic[17];
        _mapText.text = Language.TextStatic[295];
        _learningText.text = Language.TextStatic[294];

        for (int i = 0; i < _buildingTypesNameTexts.Length; i++)
        {
            _buildingTypesNameTexts[i].text = Language.TextStatic[_buildingTypesTiles[i].NameLanguageNumber];
        }

        _escapeTexts[0].text = Language.TextStatic[33];
        _escapeTexts[1].text = Language.TextStatic[28];
        _escapeTexts[2].text = Language.TextStatic[47];

        _selectBuildingText.text = Language.TextStatic[12];
    }
}
