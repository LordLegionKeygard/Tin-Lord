using UnityEngine;

public class BuildingGateView : MonoBehaviour
{
    [SerializeField] private Animator[] _animators;
    public void ControlGateView(bool open)
    {
        var number = open ? 1 : -1;

        for (int i = 0; i < _animators.Length; i++)
        {
            if(_animators[i].gameObject.activeInHierarchy) _animators[i].SetInteger(AnimatorStrings.GateState, number);
        }
    }
}
