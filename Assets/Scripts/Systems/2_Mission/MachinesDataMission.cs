using UnityEngine;

public class MachinesDataMission : MonoBehaviour
{
    public static MachinesDataMission Instance;
    [SerializeField] private int _machineLevel;
    [SerializeField] private int _machineExperience;
    [SerializeField] private MachineInfo[] _machineInfo;
    [SerializeField] private MachineExperienceInfo _experienceInfo;
    [SerializeField] private CurrentMachineSystem _currentMachineSystem;
    [SerializeField] private MachinePanel _robotPanel;
    [SerializeField] private TimeTickSystem _timeTickSystem;

    //Current
    public int GetCurrentLevel() => _machineLevel;
    public MachineInfo GetMachineInformation() => _machineInfo[(int)_currentMachineSystem.GetMachineType()];
    public int GetCurrentRangeDamage() => GetMachineInformation().GetRangeDamage(GetCurrentLevel());
    public float GetCurrentDurability() => GetMachineInformation().GetDurability(GetCurrentLevel());
    public float GetDetectionRadius() => GetMachineInformation().DetectionRadius;


    //Select
    public int GetMachineMaxExpForLevel() => _experienceInfo.NeedExperienceForNextLevel[_machineLevel];
    public int GetMachineExperience() => _machineExperience;

    private void Awake()
    {
        if (Instance != null) Debug.Log("More, than one instance MachineData");
        else Instance = this;
    }

    private void Start()
    {
        CustomEvents.OnTimeTick += ChangeExperience;
    }

    public void LoadMachineExperience()
    {
        int totalExp = _timeTickSystem.GetCurrentDay() * WorldGameInfo.OneDayTicksCount + _timeTickSystem.GetCurrentTick();

        AddExperience(totalExp);
    }


    private void AddExperience(int amount)
    {
        while (amount > 0 && _machineLevel < _experienceInfo.NeedExperienceForNextLevel.Length)
        {
            int need = _experienceInfo.NeedExperienceForNextLevel[_machineLevel] - _machineExperience;

            if (amount >= need)
            {
                amount -= need;
                NewLevel();
            }
            else
            {
                _machineExperience += amount;
                amount = 0;
            }
        }
        _robotPanel.UpdateLevelAndExperience();
    }

    public void ChangeExperience()
    {
        AddExperience(WorldGameInfo.MachineExperienceFromTick);
    }

    private void NewLevel()
    {
        _machineLevel++;
        _machineExperience = 0;
        if(_currentMachineSystem.IsHaveMachine()) _currentMachineSystem.GetMachineHealth().SetNewLevelHealth();
        _robotPanel.UpdateStatTexts();
    }

    private void OnDestroy()
    {
        CustomEvents.OnTimeTick -= ChangeExperience;
    }
}

[System.Serializable]
public enum MachineType
{
    None = -1,
    WarBallista = 0,
    Tank = 1,
    Mecha = 2,
}
