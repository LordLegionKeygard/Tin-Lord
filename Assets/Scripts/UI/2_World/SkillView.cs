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
    private Tween _cooldownTween;
    public int GetCurrentCooldown() => _currentCooldown;
    public Skill GetSkill() => _skill;
    public bool IsOpen() => _isOpen;
    public bool IsCooldownNow() => _cooldownImage.fillAmount != 0;


    public void LoadSkill(int cooldown, int lastOpenedMissionId)
    {
        if (_skill == null) return;

        if (_skill.RequiredOpenedMission <= lastOpenedMissionId)
        {
            _isOpen = true;
            _currentCooldown = cooldown;
            _icon.sprite = _skill.Icon;
            _icon.enabled = true;
            _button.interactable = true;
            UpdateView(true);
            _closeTextObject.SetActive(false);
        }
    }

    public void StartSkillCooldown()
    {
        _currentCooldown = _skill.CooldownTicks;
        _cooldownImage.fillAmount = 1;
        UpdateView(false);
    }

    public void TimeTick()
    {
        if (!_isOpen || _currentCooldown == 0 || _skill == null) return;
        _currentCooldown--;
        UpdateView(false);
    }

    private void UpdateView(bool isLoad)
    {
        _cooldownTween?.Kill();

        float target = (float)_currentCooldown / _skill.CooldownTicks;
        float duration = WorldGameInfo.TickSpeed;

        if (isLoad)
        {
            _cooldownImage.fillAmount = target;
        }
        else
            _cooldownTween = _cooldownImage.DOFillAmount(target, duration).SetEase(Ease.Linear);
    }

    private void OnDestroy()
    {
        _cooldownTween?.Kill();
    }
}
