using UnityEngine;

public class BuildingGateView : MonoBehaviour
{
    [SerializeField] private GameObject[] _gates;
    [SerializeField] private Animator[] _animators;

    public void SetBuildingGate(int rotation)
    {
        foreach (var item in _gates)
        {
            item.transform.rotation = Quaternion.Euler(0, rotation, 0);
        }
    }

    public void ControlGateView(bool open)
    {
        var number = open ? 1 : -1;

        for (int i = 0; i < _animators.Length; i++)
        {
            if (_animators[i].gameObject.activeInHierarchy) _animators[i].SetInteger(AnimatorStrings.GateState, number);
        }
    }
}
