using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SkillView : MonoBehaviour
{
    [SerializeField] private BaseSkill _baseSkill;
    [SerializeField] private Image _icon;
    [SerializeField] private Image _cooldownImage;
    [SerializeField] private GameObject _closeTextObject;
    [SerializeField] private Button _button;
    private int _currentCooldown;
    private Tween _cooldownTween;
    public int GetCurrentCooldown() => _currentCooldown;
    public bool IsCooldownNow() => _cooldownImage.fillAmount != 0;


    public void SetupSkill(int cooldown)
    {
        _currentCooldown = cooldown;
        _icon.sprite = _baseSkill.GetSkill().Icon;
        _icon.enabled = true;
        _button.interactable = true;
        UpdateView(true);
        _closeTextObject.SetActive(false);
    }

    public void StartSkillCooldown()
    {
        _currentCooldown = _baseSkill.GetSkill().CooldownTicks;
        _cooldownImage.fillAmount = 1;
        UpdateView(false);
    }

    public void CooldownTick()
    {
        if (_currentCooldown == 0) return;
        _currentCooldown--;
        UpdateView(false);
    }

    private void UpdateView(bool isLoad)
    {
        _cooldownTween?.Kill();

        float target = (float)_currentCooldown / _baseSkill.GetSkill().CooldownTicks;
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
