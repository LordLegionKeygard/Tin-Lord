using UnityEngine;

public class CurrentRobotSystem : MonoBehaviour
{
    [SerializeField] private RobotsData _robotsData;
    private GameObject _currentRobot;
    private RobotHealth _currentRobotHealth;
    private RobotLevel _currentRobotLevel;
    private RobotPatrolPath _currentRobotPatrolPath;
    public bool HaveRobot() => _currentRobot != null;
    public bool RobotDeath() => _currentRobotHealth.IsDeath();
    public int RobotLevel() => _currentRobotLevel.GetLevel();
    public RobotHealth RobotHealth() => _currentRobotHealth;
    public RobotPatrolPath RobotPatrolPath() => _currentRobotPatrolPath;



    public void SetNewRobot(GameObject newRobot, RobotType robotType)
    {
        _currentRobot = newRobot;
        _currentRobotHealth = _currentRobot.GetComponent<RobotHealth>();
        _currentRobotLevel = _currentRobot.GetComponent<RobotLevel>();
        _currentRobotPatrolPath = _currentRobot.GetComponent<RobotPatrolPath>();
        _robotsData.SetNewRobot(robotType, RobotLevel());
    }
}
