using UnityEngine;

public class ActiveBiomObjectType : MonoBehaviour
{
    [SerializeField] private GameObject[] _objectsType;
    private void Start()
    {
        ActiveType();
    }

    private void ActiveType()
    {
        var biomEnum = CurrentMissionInfo.Instance.GetCurrentMission().MissionView.BiomEnum;
        var objectNumber = biomEnum == BiomEnum.Winter ? 1 : 0;
        if (_objectsType[objectNumber] != null) _objectsType[objectNumber].SetActive(true);
    }
}
