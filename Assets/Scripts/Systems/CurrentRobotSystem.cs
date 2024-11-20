using UnityEngine;

public class CurrentRobotSystem : MonoBehaviour
{
    private GameObject _currentRobot;
    private RobotHealth _robotHealth;
    private RobotPatrolPath _robotPatrolPath;
    public bool HaveRobot() => _currentRobot != null;
    public bool RobotDeath() => _robotHealth.IsDeath();
    public RobotHealth RobotHealth() => _robotHealth;
    public RobotPatrolPath RobotPatrolPath() => _robotPatrolPath;



    public void SetNewRobot(GameObject newRobot, RobotType robotType)
    {
        _currentRobot = newRobot;
        _robotHealth = _currentRobot.GetComponent<RobotHealth>();
        _robotPatrolPath = _currentRobot.GetComponent<RobotPatrolPath>();
        RobotsData.Instance.SetNewRobotType(robotType);
    }
}
