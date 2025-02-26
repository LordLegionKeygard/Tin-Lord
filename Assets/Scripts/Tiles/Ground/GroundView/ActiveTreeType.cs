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
        var biomEnum = CurrentMissionInfo.Instance.GetCurrentMission().MissionView.BiomEnum;
        var treeNumber = biomEnum == BiomEnum.Winter ? 1 : 0;
        _treesType[treeNumber].SetActive(true);
    }
}
