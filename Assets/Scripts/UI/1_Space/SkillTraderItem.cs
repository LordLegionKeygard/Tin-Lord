using UnityEngine;
using UnityEngine.UI;

public class SkillTraderItem : MonoBehaviour
{
    [SerializeField] private SkillTraderPanel _skillTraderPanel;
    [SerializeField] private SkillInfo _skillInfo;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _iconView;
    [SerializeField] private GameObject _selectView;
    [SerializeField] private GameObject _closeTextView;
    [SerializeField] private Image _icon;
    public SkillInfo GetSkillInfo() => _skillInfo;

    public void Select()
    {
        _skillTraderPanel.SelectSkill(_skillInfo);
    }

    public void SelectToggle(bool state)
    {
        _selectView.SetActive(state);
    }

    public void SetSkillOpen(SkillInfo skillInfo, bool skillOpenedNow, int currentAct)
    {
        var haveRequiredAct = skillInfo != null ? _skillInfo.SkillTraderRequiredAct <= currentAct : false;

        _button.interactable = haveRequiredAct && !skillOpenedNow;
        _closeTextView.SetActive(!haveRequiredAct);
        _iconView.SetActive(haveRequiredAct || skillOpenedNow);
        _icon.color = skillOpenedNow ? Colors.AlphaGreySeven : Color.white;
        if (skillInfo != null) _icon.sprite = skillInfo.Icon;
    }
}
