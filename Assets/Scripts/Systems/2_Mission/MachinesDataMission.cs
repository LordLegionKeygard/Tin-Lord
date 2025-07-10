using UnityEngine;

public class MachinesDataMission : MonoBehaviour
{
    public static MachinesDataMission Instance;
    [SerializeField] private int[] _machinesLevel;
    [SerializeField] private int[] _machinesExperience;
    [SerializeField] private MachineInformation[] _machineInformation;
    [SerializeField] private MachineExperienceInfo _experienceInfo;
    [SerializeField] private CurrentMachineSystem _currentMachineSystem;
    [SerializeField] private MachinePanel _robotPanel;

    //Current
    public int CurrentLevel() => _machinesLevel[(int)_currentMachineSystem.GetMachineType()];
    public int GetCurrentMeleeDamage() => _machineInformation[(int)_currentMachineSystem.GetMachineType()].GetMeleeDamage(CurrentLevel());
    public int GetCurrentRangeDamage() => _machineInformation[(int)_currentMachineSystem.GetMachineType()].GetRangeDamage(CurrentLevel());
    public float GetCurrentDurability() => _machineInformation[(int)_currentMachineSystem.GetMachineType()].GetDurability(CurrentLevel());
    public float GetDetectionRadius() => _machineInformation[(int)_currentMachineSystem.GetMachineType()].DetectionRadius;


    //Select
    public int GetSelectMachineDataLevel(MachineType selectMachineType) => _machinesLevel[(int)selectMachineType];
    public int GetSelectMachineMaxExpForLevel(MachineType selectMachineType) => _experienceInfo.NeedExperienceForNextLevel[_machinesLevel[(int)selectMachineType]];
    public int GetSelectMachineExperience(MachineType selectMachineType) => _machinesExperience[(int)selectMachineType];

    //Save
    public MachinesExperienceData[] GetAllMachinesExperience()
    {
        var data = new MachinesExperienceData[_machineInformation.Length];

        for (int i = 0; i < _machineInformation.Length; i++)
        {
            data[i] = new MachinesExperienceData
            {
                Level = _machinesLevel[i],
                Experience = _machinesExperience[i]
            };
        }

        return data;
    }

    public void LoadMachinesExperience(MachinesExperienceData[] data, bool isStartMission)
    {
        if(isStartMission) return;

        for (int i = 0; i < _machineInformation.Length; i++)
        {
            _machinesLevel[i] = data[i].Level;
            _machinesExperience[i] = data[i].Experience;
        }    
    }

    private void Awake()
    {
        if (Instance != null) Debug.Log("More, than one instance MachineData");
        else Instance = this;
    }

    private void Start()
    {
        CustomEvents.OnChangeExperience += ChangeExperience;
    }

    public void ChangeExperience(int experience)
    {
        if (!_currentMachineSystem.IsHaveMachine() || _currentMachineSystem.IsMachineDeath()) return;

        var maxExp = _experienceInfo.NeedExperienceForNextLevel[CurrentLevel()];
        var currentExp = _machinesExperience[(int)_currentMachineSystem.GetMachineType()];

        if (experience >= maxExp - currentExp)
        {
            var surplus = experience - (maxExp - currentExp);
            NewLevel();
            currentExp = surplus;

            if (surplus >= maxExp)
            {
                NewLevel();
                currentExp = surplus - maxExp;
            }
        }
        else
        {
            currentExp += experience;
        }

        _machinesExperience[(int)_currentMachineSystem.GetMachineType()] = currentExp;

        if (_currentMachineSystem.GetMachineType() == _robotPanel.GetCurrentMachineType())
        {
            _robotPanel.UpdateLevelAndExperience();
        }
    }

    private void NewLevel()
    {
        _machinesLevel[(int)_currentMachineSystem.GetMachineType()]++;

        if (_currentMachineSystem.GetMachineType() == _robotPanel.GetCurrentMachineType())
        {
            _robotPanel.UpdateStatTexts();
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnChangeExperience -= ChangeExperience;
    }
}

[System.Serializable]
public enum MachineType
{
    None = -1,
    WarBallista = 0,
    Tank = 1,
    Unknown = 2,
}
