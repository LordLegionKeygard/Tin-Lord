using UnityEngine;


public class CityRobotInformation : MonoBehaviour
{
    [SerializeField] private CityRobotInfo cityRobotInfo;

    public CityRobotInfo GetCityRobotInfo() => cityRobotInfo;
}
