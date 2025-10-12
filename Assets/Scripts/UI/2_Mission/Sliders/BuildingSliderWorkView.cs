using UnityEngine;

public class BuildingSliderWorkView : MonoBehaviour
{
    [SerializeField] private GameObject _notWorkView;
    [SerializeField] private GameObject _needResourceView;

    public void SetIsBuildingWorkView(bool isWork)
    {
        _notWorkView.SetActive(!isWork);
    }
    public void SetIsHaveRequiredResourceView(bool isHaveRequiredResource)
    {
        _needResourceView.SetActive(!isHaveRequiredResource);
    }


}