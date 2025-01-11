using UnityEngine;

public class CurrentRobotSystem : MonoBehaviour
{
    private GameObject _currentRobot;
    private RobotHealth _robotHealth;
    private RobotPatrolPath _robotPatrolPath;
    private RobotType _currentRobotType = RobotType.None;
    public bool HaveRobot() => _currentRobot != null;
    public bool RobotDeath() => _robotHealth.IsDeath();
    public RobotHealth RobotHealth() => _robotHealth;
    public RobotPatrolPath RobotPatrolPath() => _robotPatrolPath;
    public RobotType GetRobotType() => _currentRobotType;

    public RobotData GetRobotData()
    {
        var data = new RobotData
        {
            IsHaveRobotNow = HaveRobot(),
            RobotType = (int)_currentRobotType,
            PositionX = HaveRobot() ? _currentRobot.transform.localPosition.x : 0,
            PositionY = HaveRobot() ? _currentRobot.transform.position.y : 0,
            PositionZ = HaveRobot() ? _currentRobot.transform.localPosition.z : 0,
            NextPatrolIndex = HaveRobot() ? _robotPatrolPath.RobotPatrolState().GetCurrentPatrolPointIndex() : 0,
        };

        return data;
    }

    public void SetNewRobot(GameObject newRobot, RobotType robotType)
    {
        _currentRobot = newRobot;
        _currentRobotType = robotType;
        _robotHealth = _currentRobot.GetComponent<RobotHealth>();
        _robotPatrolPath = _currentRobot.GetComponent<RobotPatrolPath>();
    }
}
