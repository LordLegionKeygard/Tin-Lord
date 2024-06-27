using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBuildingAnimator : MonoBehaviour
{
    [SerializeField] private CharacterWorkType _characterBuildType;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        TriggerAnimator();
    }

    private void TriggerAnimator()
    {
        switch (_characterBuildType)
        {
            case CharacterWorkType.PickaxeMining:
                _animator.SetTrigger(AnimatorStrings.PickaxeMining);
                break;
        }
    }
}
