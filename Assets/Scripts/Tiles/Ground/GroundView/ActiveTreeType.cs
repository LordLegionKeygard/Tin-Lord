using UnityEngine;

public class ActiveTreeType : MonoBehaviour
{ 
    [SerializeField] private GameObject[] _treesType;
    private void Start()
    {
        ActiveType();
    }

    private void ActiveType()
    {
        var number = (int)CurrentMissionInfo.Instance.GetCurrentMission().MissionView.TreeType;
        Debug.Log(number);
        _treesType[number].SetActive(true);
    }
}
