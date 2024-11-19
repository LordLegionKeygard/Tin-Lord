using UnityEngine;

public class RobotsData : MonoBehaviour
{
    [SerializeField] private int[] _robotsLevel;
    [SerializeField] private RobotType _robotType = RobotType.None;
    public int GetRobotDataLevel(RobotType robotType) => _robotsLevel[(int)robotType];
    public RobotType GetRobotType() => _robotType;

    public void SetNewRobot(RobotType robotType, int level)
    {
        _robotsLevel[(int)robotType] = level;
        _robotType = robotType;
    }
}

[System.Serializable]
public enum RobotType
{
    None = -1,
    Tank = 0,
    Sniper = 1,
    Engineer = 2,
}
