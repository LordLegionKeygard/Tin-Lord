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
        StartWork();
    }

    public void StartWork()
    {
        if (_animator == null) return;

        if (AnimatorStrings.WorkTriggers.TryGetValue(_characterWorkType, out int trigger))
        {
            _animator.SetTrigger(trigger);
        }
    }

    public void ToggleWorkView(bool state)
    {
        if (_animator == null) return;
        _animator.SetBool(AnimatorStrings.Idle, !state);
    }
}
