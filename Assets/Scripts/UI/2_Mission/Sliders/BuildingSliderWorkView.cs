using UnityEngine;

public class BuildingSliderWorkView : MonoBehaviour
{
    [SerializeField] private GameObject _notWorkView;
    [SerializeField] private GameObject _needResourceView;
    [SerializeField] private GameObject _cantShootView;

    public void SetIsBuildingWorkView(bool isWork)
    {
        _notWorkView.SetActive(!isWork);
    }
    public void SetIsHaveRequiredResourceView(bool isHaveRequiredResource)
    {
        _needResourceView.SetActive(!isHaveRequiredResource);
    }

    public void SetlIsBuildingTurrentCantShootView(bool isCantShoot)
    {
        _cantShootView.SetActive(isCantShoot);
    }
}