using UnityEngine;

public class CurrentMachineSystem : MonoBehaviour
{
    private GameObject _currentMachine;
    private RobotHealth _robotHealth;
    private RobotPatrolPath _robotPatrolPath;
    private MachineType _currentMachineType = MachineType.None;
    public bool HaveMachine() => _currentMachine != null;
    public bool MachineDeath() => _robotHealth.IsDeath();
    public RobotHealth MachineHealth() => _robotHealth;
    public RobotPatrolPath RobotPatrolPath() => _robotPatrolPath;
    public MachineType GetMachineType() => _currentMachineType;

    public MachineData GetRobotData()
    {
        var data = new MachineData
        {
            IsHaveMachineNow = HaveMachine(),
            MachineType = (int)_currentMachineType,
            PositionX = HaveMachine() ? _currentMachine.transform.localPosition.x : 0,
            PositionY = HaveMachine() ? _currentMachine.transform.position.y : 0,
            PositionZ = HaveMachine() ? _currentMachine.transform.localPosition.z : 0,
            Rotation = HaveMachine() ? _currentMachine.transform.eulerAngles.y : 0,
            NextPatrolIndex = HaveMachine() ? _robotPatrolPath.RobotPatrolState().GetCurrentPatrolPointIndex() : 0,
        };

        return data;
    }

    public void SetNewRobot(GameObject newRobot, MachineType robotType)
    {
        _currentMachine = newRobot;
        _currentMachineType = robotType;
        _robotHealth = _currentMachine.GetComponent<RobotHealth>();
        _robotPatrolPath = _currentMachine.GetComponent<RobotPatrolPath>();
    }
}
