using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBuildingAnimator : MonoBehaviour
{
    [SerializeField] private CharacterWorkType _characterWorkType;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        TriggerWorkAnimator();
    }

    public void TriggerWorkAnimator()
    {
        if (_animator == null) return;

        switch (_characterWorkType)
        {
            case CharacterWorkType.PickaxeMining:
                _animator.SetTrigger(AnimatorStrings.PickaxeMining);
                break;
            case CharacterWorkType.ShovelDig:
                _animator.SetTrigger(AnimatorStrings.ShovelDig);
                break;
            case CharacterWorkType.AxeChop:
                _animator.SetTrigger(AnimatorStrings.AxeChop);
                break;
            case CharacterWorkType.HoldPlank:
                _animator.SetTrigger(AnimatorStrings.HoldPlank);
                break;
        }
    }

    public void TriggerNotWorkAnimator()
    {
        if (_animator == null) return;
        _animator.SetTrigger(AnimatorStrings.Idle);
    }
}
