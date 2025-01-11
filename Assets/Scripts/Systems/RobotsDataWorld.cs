using UnityEngine;

public class RobotsDataWorld : MonoBehaviour
{
    public static RobotsDataWorld Instance;
    [SerializeField] private int[] _robotsLevel;
    [SerializeField] private int[] _robotsExperience;
    [SerializeField] private RobotInformation[] _robotsInformation;
    [SerializeField] private RobotExperienceInfo _experienceInfo;
    [SerializeField] private CurrentRobotSystem _currentRobotSystem;
    [SerializeField] private RobotPanel _robotPanel;

    //Current
    public int CurrentLevel() => _robotsLevel[(int)_currentRobotSystem.GetRobotType()];
    public int GetCurrentMeleeDamage() => _robotsInformation[(int)_currentRobotSystem.GetRobotType()].MeleeDamage[CurrentLevel()];
    public int GetCurrentRangeDamage() => _robotsInformation[(int)_currentRobotSystem.GetRobotType()].RangeDamage[CurrentLevel()];
    public float GetCurrentDurability() => _robotsInformation[(int)_currentRobotSystem.GetRobotType()].Durability[CurrentLevel()];
    public float GetDetectionRadius() => _robotsInformation[(int)_currentRobotSystem.GetRobotType()].DetectionRadius;


    //Select
    public int GetSelectRobotDataLevel(RobotType selectRobotType) => _robotsLevel[(int)selectRobotType];
    public int GetSelectRobotMaxExpForLevel(RobotType selectRobotType) => _experienceInfo.NeedExperienceForNextLevel[_robotsLevel[(int)selectRobotType]];
    public int GetSelectRobotExperience(RobotType selectRobotType) => _robotsExperience[(int)selectRobotType];

    //Save
    public RobotsExperienceData[] GetAllRobotsExperience()
    {
        var data = new RobotsExperienceData[_robotsInformation.Length];

        for (int i = 0; i < _robotsInformation.Length; i++)
        {
            data[i] = new RobotsExperienceData
            {
                Level = _robotsLevel[i],
                Experience = _robotsExperience[i]
            };
        }

        return data;
    }

    public void LoadRobotsExperience(RobotsExperienceData[] data, bool isStartMission)
    {
        if(isStartMission) return;

        for (int i = 0; i < _robotsInformation.Length; i++)
        {
            _robotsLevel[i] = data[i].Level;
            _robotsExperience[i] = data[i].Experience;
        }    
    }

    private void Awake()
    {
        if (Instance != null) Debug.Log("More, than one instance RobotsData");
        else Instance = this;
    }

    private void Start()
    {
        CustomEvents.OnChangeExperience += ChangeExperience;
    }

    public void ChangeExperience(int experience)
    {
        if (!_currentRobotSystem.HaveRobot() || _currentRobotSystem.RobotDeath()) return;

        var maxExp = _experienceInfo.NeedExperienceForNextLevel[CurrentLevel()];
        var currentExp = _robotsExperience[(int)_currentRobotSystem.GetRobotType()];

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

        _robotsExperience[(int)_currentRobotSystem.GetRobotType()] = currentExp;

        if (_currentRobotSystem.GetRobotType() == _robotPanel.GetCurrentRobotType())
        {
            _robotPanel.UpdateLevelAndExperience();
        }
    }

    private void NewLevel()
    {
        _robotsLevel[(int)_currentRobotSystem.GetRobotType()]++;

        if (_currentRobotSystem.GetRobotType() == _robotPanel.GetCurrentRobotType())
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
public enum RobotType
{
    None = -1,
    Tank = 0,
    Sniper = 1,
    Engineer = 2,
}
