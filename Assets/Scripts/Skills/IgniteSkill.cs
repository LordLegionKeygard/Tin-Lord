using UnityEngine;
using Zenject;

public class IgniteSkill : BaseSkill
{
    [Inject] private readonly SpawnedHazardSystem _spawnedHazardSystem;
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
        AudioManager.Instance.PlayerOneShot(GetSkill().Sound, transform.position);
        var _currentPrefab = Instantiate(_skillPrefab, _skillTargetSystem.GetTargetTransform().position, Quaternion.identity);
        _currentPrefab.GetComponent<OnTriggerStayDealDamage>().SetInfo(GetSkill().DurationTicks, GetSkill().TriggerStayDamageFactor);
        _spawnedHazardSystem.RegisterHazard((int)HazardEnum.IgniteSkill, _currentPrefab, GetSkill().DurationTicks, GetSkill().TriggerStayDamageFactor);
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
