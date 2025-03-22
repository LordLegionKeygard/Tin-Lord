using UnityEngine;

public class CurrentMachineSystem : MonoBehaviour
{
    private GameObject _currentMachine;
    private MachineHealth _machineHealth;
    private MachinePatrolPath _machinePatrolPath;
    private MachineType _currentMachineType = MachineType.None;
    public bool HaveMachine() => _currentMachine != null;
    public bool MachineDeath() => _machineHealth.IsDeath();
    public MachineHealth MachineHealth() => _machineHealth;
    public MachinePatrolPath RobotPatrolPath() => _machinePatrolPath;
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
            NextPatrolIndex = HaveMachine() ? _machinePatrolPath.RobotPatrolState().GetCurrentPatrolPointIndex() : 0,
        };

        return data;
    }

    public void SetNewRobot(GameObject newRobot, MachineType robotType)
    {
        _currentMachine = newRobot;
        _currentMachineType = robotType;
        _machineHealth = _currentMachine.GetComponent<MachineHealth>();
        _machinePatrolPath = _currentMachine.GetComponent<MachinePatrolPath>();
    }
}
