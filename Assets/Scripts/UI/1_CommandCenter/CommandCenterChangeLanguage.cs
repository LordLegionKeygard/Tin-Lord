using TMPro;
using UnityEngine;

public class CommandCenterChangeLanguage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _headerText;
    [SerializeField] private TextMeshProUGUI[] _buildingTypesNameTexts;
    [SerializeField] private Tile[] _buildingTypesTiles;


    private void Start()
    {
        _headerText.text = Language.TextStatic[32];

        for (int i = 0; i < _buildingTypesNameTexts.Length; i++)
        {
            _buildingTypesNameTexts[i].text = _buildingTypesTiles[i].Name[Language.LanguageNumber];
        }
    }
}
