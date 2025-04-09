using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SkillView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Image _cooldownImage;
    [SerializeField] private GameObject _closeTextObject;
    [SerializeField] private Button _button;
    [SerializeField] private Skill _skill;
    private int _currentCooldown;
    private bool _isOpen;
    public int GetCurrentCooldown() => _currentCooldown;
    public Skill GetSkill() => _skill;
    public bool IsOpen() => _isOpen;
    public bool IsCooldownNow() => _currentCooldown != 0;

    public void LoadSkill(int cooldown, int lastOpenedMissionId)
    {
        if (_skill == null) return;

        if (_skill.RequiredOpenedMission <= lastOpenedMissionId)
        {
            _isOpen = true;
            _currentCooldown = cooldown;
            _icon.sprite = _skill.Icon;
            _icon.enabled = true;
            UpdateView();
            _closeTextObject.SetActive(false);
        }
    }

    public void StartSkillCooldown()
    {
        _currentCooldown = _skill.CooldownTicks;
        _cooldownImage.fillAmount = 1;
        UpdateView();
    }

    public void TimeTick()
    {
        if (!_isOpen || _currentCooldown == 0 || _skill == null) return;
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

            var value = 1f / _skill.CooldownTicks;

            _cooldownImage.DOFillAmount((float)(value * _currentCooldown), 2 * Time.timeScale);
        }
    }
}
