using TMPro;
using UnityEngine;

public class SkillPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _skillsPanelHeaderText;
    [SerializeField] private TextMeshProUGUI _descriptionText;

    private void Start()
    {
        _skillsPanelHeaderText.text = Language.TextStatic[179];
        ResetText();
    }

    public void SetText(string newText)
    {
        _descriptionText.text = newText;
    }

    public void ResetText()
    {
        _descriptionText.text = $"{Language.TextStatic[180]}: -";
    }
}
