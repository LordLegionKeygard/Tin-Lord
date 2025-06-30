using UnityEngine;

public class RobotBuildingAnimator : MonoBehaviour
{
    [SerializeField] private CharacterWorkType _characterWorkType;
    private Animator _animator;
    public int GetRobotWorkTypeView() => (int)_characterWorkType - 1;

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
