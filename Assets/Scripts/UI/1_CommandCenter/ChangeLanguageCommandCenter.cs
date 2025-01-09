using TMPro;
using UnityEngine;

public class ChangeLanguageCommandCenter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _headerText;
    [SerializeField] private TextMeshProUGUI[] _buildingTypesNameTexts;
    [SerializeField] private Tile[] _buildingTypesTiles;
    [SerializeField] private TextMeshProUGUI[] _escapeTexts;
    [SerializeField] private TextMeshProUGUI _areYouSureText;


    private void Start()
    {
        _headerText.text = Language.TextStatic[32];

        for (int i = 0; i < _buildingTypesNameTexts.Length; i++)
        {
            _buildingTypesNameTexts[i].text = _buildingTypesTiles[i].Name[Language.LanguageNumber];
        }

        _escapeTexts[0].text = Language.TextStatic[44];
        _escapeTexts[1].text = Language.TextStatic[28];
        _escapeTexts[2].text = Language.TextStatic[47];
        _areYouSureText.text = Language.TextStatic[48];
    }
}
