using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgniteSkill : BaseSkill
{
    [SerializeField] private SkillTargetSystem _skillTargetSystem;
    [SerializeField] private GameObject _skillPrefab;
    private bool _isPrepareUseSkill;

    private void Start()
    {
        CustomEvents.OnUseTargetSkill += UseTargetSkill;
        CustomEvents.OnCancelTargetSkill += CancelSkill;
    }

    public override void UseSkill()
    {
        if (CantUseSkill())
        {
            AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Error], transform.position);
            return;
        }

        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);

        CustomEvents.FireActiveTargetSkill();
        _isPrepareUseSkill = true;
    }

    public void UseTargetSkill()
    {
        if (!_isPrepareUseSkill) return;
        _isPrepareUseSkill = false;
        // AudioManager.Instance.PlayerOneShot(GetSkill().Sound, transform.position);
        Instantiate(_skillPrefab, _skillTargetSystem.GetTargetTransform().position, Quaternion.identity);
        UseResources();
        SkillView.StartSkillCooldown();
        CheckDuration(GetSkill().DurationTicks);
    }

    private void CancelSkill()
    {
        _isPrepareUseSkill = false;
    }

    private void OnDestroy()
    {
        CustomEvents.OnUseTargetSkill -= UseTargetSkill;
        CustomEvents.OnCancelTargetSkill -= CancelSkill;
    }
}
