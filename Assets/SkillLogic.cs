using UnityEngine;
using UnityEngine.UI;

public class SkillLogic : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Image _cooldownImage;
    [SerializeField] private GameObject _closeTextObject;
    [SerializeField] private Button _button;
    [SerializeField] private bool _isOpen;
    [SerializeField] private Skill _skill;
    [SerializeField] private int _currentCooldown;
    public int GetCurrentCooldown() => _currentCooldown;
    public Skill GetSkill() => _skill;
    public bool IsOpen() => _isOpen;
    public bool IsCooldownNow() => _currentCooldown != 0;

    public void LoadSkill(int cooldown, int lastOpenedMissionId)
    {
        if (_skill.RequiredOpenedMission <= lastOpenedMissionId)
        {
            _isOpen = true;
            _currentCooldown = cooldown;
            _icon.sprite = _skill.Icon;
            _icon.enabled = true;
            UpdateView();

        }
        else
        {
            _closeTextObject.SetActive(true);
        }
    }

    public void StartSkillCooldown()
    {
        _currentCooldown = _skill.CooldownTicks;
        UpdateView();
    }

    public void TimeTick()
    {
        if (!_isOpen || _currentCooldown == 0) return;
        _currentCooldown--;
        UpdateView();
    }

    private void UpdateView()
    {
        if (_currentCooldown == 0)
        {
            _button.interactable = true;
            _cooldownImage.fillAmount = 0;
        }
        else
        {
            _button.interactable = false;
            _cooldownImage.fillAmount = 1f - (float)(1f / _currentCooldown);
        }
    }
}
