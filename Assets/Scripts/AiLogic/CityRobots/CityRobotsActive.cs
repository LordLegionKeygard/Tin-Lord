using UnityEngine;
using Zenject;

public class CityRobotsActive : MonoBehaviour
{
    [SerializeField] private GameObject[] _cityRobots;
    [Inject] private readonly MissionHangarSystem _missionHangarSystem;

    private void Start()
    {
        _cityRobots[_missionHangarSystem.GetCurrentRobot()].SetActive(true);
    }
}
