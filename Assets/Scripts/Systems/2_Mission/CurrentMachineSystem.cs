using UnityEngine;

public class CurrentMachineSystem : MonoBehaviour
{
    private GameObject _currentMachine;
    private MachineHealth _machineHealth;
    private MachinePatrolPath _machinePatrolPath;
    private MachineDamage _machineDamage;
    private MachineType _currentMachineType = MachineType.None;
    public bool IsHaveMachine() => _currentMachine != null;
    public bool IsMachineDeath() => _machineHealth.IsDeath();
    public MachineHealth GetMachineHealth() => _machineHealth;
    public MachinePatrolPath GetMachinePatrolPath() => _machinePatrolPath;
    public MachineType GetMachineType() => _currentMachineType;
    public MachineDamage GetMachineDamage() => _machineDamage;

    public MachineData GetMachineData()
    {
        var data = new MachineData
        {
            IsHaveMachineNow = IsHaveMachine(),
            MachineType = (int)_currentMachineType,
            PositionX = IsHaveMachine() ? _currentMachine.transform.localPosition.x : 0,
            PositionY = IsHaveMachine() ? _currentMachine.transform.position.y : 0,
            PositionZ = IsHaveMachine() ? _currentMachine.transform.localPosition.z : 0,
            Rotation = IsHaveMachine() ? _currentMachine.transform.eulerAngles.y : 0,
            NextPatrolIndex = IsHaveMachine() ? _machinePatrolPath.RobotPatrolState().GetCurrentPatrolPointIndex() : 0,
            MachineHealth = IsHaveMachine() ? GetMachineHealth().GetCurrentHealth() : 0
        };

        return data;
    }

    public void SetNewRobot(GameObject newRobot, MachineType robotType)
    {
        _currentMachine = newRobot;
        _currentMachineType = robotType;
        _machineHealth = _currentMachine.GetComponent<MachineHealth>();
        _machinePatrolPath = _currentMachine.GetComponent<MachinePatrolPath>();
        _machineDamage = _currentMachine.GetComponent<MachineDamage>();
    }
}
