using Pathfinding;
using UnityEngine;

public class CityRobotStop : MonoBehaviour
{
    private AIPath _aIPath;
    private AIDestinationSetter _aIDestinationSetter;
    private Animator _animator;
    private CityRobotStateChanger _cityRobotStateChanger;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _aIPath = GetComponent<AIPath>();
        _aIDestinationSetter = GetComponent<AIDestinationSetter>();
        _cityRobotStateChanger = GetComponent<CityRobotStateChanger>();
    }

    private void Start()
    {
        CustomEvents.OnMissionEnd += StopTurretLogic;
    }

    public void StopTurretLogic(MissionEndEnum _)
    {
        _animator.enabled = false;
        _aIPath.enabled = false;
        _aIDestinationSetter.enabled = false;
        _cityRobotStateChanger.enabled = false;
    }

    private void OnDestroy()
    {
        CustomEvents.OnMissionEnd -= StopTurretLogic;
    }
}
