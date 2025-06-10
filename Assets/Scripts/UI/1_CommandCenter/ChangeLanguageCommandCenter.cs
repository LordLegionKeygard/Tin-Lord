using TMPro;
using UnityEngine;

public class ChangeLanguageCommandCenter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _mapText;
    [SerializeField] private TextMeshProUGUI _learningText;
    [SerializeField] private TextMeshProUGUI[] _buildingTypesNameTexts;
    [SerializeField] private Tile[] _buildingTypesTiles;
    [SerializeField] private TextMeshProUGUI[] _escapeTexts;
    [SerializeField] private TextMeshProUGUI _areYouSureText;

    [Header("SelectBuildingInfoPanel")]
    [SerializeField] private TextMeshProUGUI _selectBuildingText;
    [SerializeField] private TextMeshProUGUI _buildingHealthText;
    [SerializeField] private TextMeshProUGUI _buildingEcologyText;
    [SerializeField] private TextMeshProUGUI _buildingLevelText;



    private void Start()
    {
        _mapText.text = Language.TextStatic[271];
        _learningText.text = Language.TextStatic[272];

        for (int i = 0; i < _buildingTypesNameTexts.Length; i++)
        {
            _buildingTypesNameTexts[i].text = _buildingTypesTiles[i].Name[Language.LanguageNumber];
        }

        _escapeTexts[0].text = Language.TextStatic[33];
        _escapeTexts[1].text = Language.TextStatic[28];
        _escapeTexts[2].text = Language.TextStatic[47];
        _areYouSureText.text = Language.TextStatic[48];

        _selectBuildingText.text = Language.TextStatic[12];
        _buildingHealthText.text =  $"{Language.TextStatic[97]}: -";
        _buildingEcologyText.text = $"{Language.TextStatic[16]}: -";
        _buildingLevelText.text = $"{Language.TextStatic[3]}: -";
    }
}
