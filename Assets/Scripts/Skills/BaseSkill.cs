using UnityEngine;
using Zenject;

public class BaseSkill : MonoBehaviour
{
    [Inject] protected PlayerResources PlayerResources;
    public SkillView SkillView;
    [SerializeField] private Skill _skill;
    [SerializeField] private bool _isActive;
    [SerializeField] private int _currentDurationTick;
    [SerializeField] private bool _isOpen;
    public bool IsActive() => _isActive;
    public Skill GetSkill() => _skill;
    public bool IsOpen() => _isOpen;
    public int GetCurrentDurationTick() => _currentDurationTick;

    public void LoadSkill(int cooldown, int duration, int lastOpenedMissionId)
    {
        if (_skill == null) return;

        if (_skill.RequiredOpenedMission <= lastOpenedMissionId)
        {
            _isOpen = true;
            SkillView.SetupSkill(cooldown);
            CheckDuration(duration);
        }
    }

    public virtual void UseSkill()
    {
        if (CanUseSkill())
        {
            AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Error], transform.position);
            return;
        }

        AudioManager.Instance.PlayerOneShot(_skill.Sound, transform.position);

        UseResources();
        SkillView.StartSkillCooldown();
        CheckDuration(_skill.DurationTicks);
    }

    public virtual bool CanUseSkill() => SkillView.IsCooldownNow() || !_isOpen || !ResourceEnough();

    public virtual void UseResources()
    {
        PlayerResources.ChangeResource(_skill.RequiredResource.Resource.ResourceEnum, -_skill.RequiredResource.RecourceAmount);
    }

    public void CheckDuration(int newDuration)
    {
        if (_skill.DurationTicks != 0 && newDuration != 0)
        {
            _isActive = true;
            _currentDurationTick = newDuration;
            CustomEvents.FireUseSkill(_skill);
        }
    }

    public bool ResourceEnough() => _skill.RequiredResource.Resource == null || PlayerResources.ResourceEnough(_skill.RequiredResource.Resource.ResourceEnum, _skill.RequiredResource.RecourceAmount);

    public void TimeTick()
    {
        if (!_isOpen) return;
        SkillView.CooldownTick();
        SkillDurationTick();
    }

    public void SkillDurationTick()
    {
        if (_currentDurationTick == 0)
        {
            CustomEvents.FireEndSkill(_skill);
            _isActive = false;
            return;
        }

        _currentDurationTick--;
    }
}
