using UnityEngine;

public class RobotsData : MonoBehaviour
{
    [SerializeField] private int[] _robotsLevel;
    public int GetRobotDataLevel(RobotType robotType) => _robotsLevel[(int)robotType];

    public void SetNewRobotLevel(RobotType robotType, int level)
    {
        _robotsLevel[(int)robotType] = level;
    }
}

[System.Serializable]
public enum RobotType
{
    Tank = 0,
    Sniper = 1,
    Engineer = 2,
}
