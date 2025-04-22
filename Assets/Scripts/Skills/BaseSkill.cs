using UnityEngine;

public class BaseSkill : MonoBehaviour
{
    public SkillView SkillView;
    [SerializeField] private Skill _skill;
    [SerializeField] private bool _isActive;
    [SerializeField] private int _currentDurationTick;
    private bool _isOpen;
    public bool IsActive() => _isActive;
    public Skill GetSkill() => _skill;
    public bool IsOpen() => _isOpen;

    public void LoadSkill(int cooldown, int lastOpenedMissionId)
    {
        if (_skill == null) return;

        if (_skill.RequiredOpenedMission <= lastOpenedMissionId)
        {
            _isOpen = true;
            SkillView.SetupSkill(cooldown);
        }
    }

    public virtual void UseSkill()
    {
        if (SkillView.IsCooldownNow())
        {
            AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Error], transform.position);
            return;
        }

        SkillView.StartSkillCooldown();
        
        if (_skill.DurationTicks != 0)
        {
            _isActive = true;
            _currentDurationTick = _skill.DurationTicks;
            CustomEvents.FireUseSkill(_skill);
        }
    }

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
