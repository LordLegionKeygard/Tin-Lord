using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectSkill : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Image _skillImage;
    [SerializeField] private Image _resourceImage;
    [SerializeField] private TextMeshProUGUI _headerText;
    [SerializeField] private TextMeshProUGUI _infoText;
    [SerializeField] private TextMeshProUGUI _requiredText;
    [SerializeField] private TextMeshProUGUI _inputText;
    [SerializeField] private GameObject _resourceView;

    private float _offsetY = 10f;

    private void Awake()
    {
        CustomEvents.OnUpdateSkillToolTip += UpdateView;
    }

    private void UpdateView(float x, float y, Skill skill, bool resourceEnough)
    {
        var isHaveResource = skill.RequiredResource.Resource != null;

        _skillImage.sprite = skill.Icon;
        _headerText.text = skill.Name[Language.LanguageNumber];
        _infoText.text = skill.Info[Language.LanguageNumber];
        _requiredText.text = !isHaveResource ? Language.TextStatic[181]
        : $"{Language.TextStatic[182]}: {skill.RequiredResource.RecourceAmount} {Language.TextStatic[skill.RequiredResource.Resource.NameNumber]}";

        _requiredText.color = resourceEnough ? Color.white : Colors.WarningYellow;

        _resourceView.SetActive(isHaveResource);
        if (isHaveResource) _resourceImage.sprite = skill.RequiredResource.Resource.Icon;

        _inputText.text = skill.Input;

        LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform);
        transform.position = new Vector2(x, y - _offsetY);
    }

    private void OnDestroy()
    {
        CustomEvents.OnUpdateSkillToolTip -= UpdateView;
    }
}
