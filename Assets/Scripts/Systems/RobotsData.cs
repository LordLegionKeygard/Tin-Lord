using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RobotsData : MonoBehaviour
{
    public static RobotsData Instance;
    [SerializeField] private int[] _robotsLevel;
    [SerializeField] private int[] _robotsExperience;
    [SerializeField] private RobotInformation[] _robotsInformation;
    [SerializeField] private RobotExperienceInfo _experienceInfo;
    [SerializeField] private CurrentRobotSystem _currentRobotSystem;
    [SerializeField] private RobotPanel _robotPanel;
    private RobotType _robotType = RobotType.None;
    public RobotType GetRobotType() => _robotType;


    //Current
    public int CurrentLevel() => _robotsLevel[(int)_robotType];
    public int GetCurrentMeleeDamage() => _robotsInformation[(int)_robotType].MeleeDamage[CurrentLevel()];
    public int GetCurrentRangeDamage() => _robotsInformation[(int)_robotType].RangeDamage[CurrentLevel()];
    public float GetCurrentDurability() => _robotsInformation[(int)_robotType].Durability[CurrentLevel()];
    public float GetDetectionRadius() => _robotsInformation[(int)_robotType].DetectionRadius;


    //Select
    public int GetSelectRobotDataLevel(RobotType selectRobotType) => _robotsLevel[(int)selectRobotType];
    public int GetSelectRobotMaxExpForLevel(RobotType selectRobotType) => _experienceInfo.NeedExperienceForNextLevel[_robotsLevel[(int)selectRobotType]];
    public int GetSelectRobotExperience(RobotType selectRobotType) => _robotsExperience[(int)selectRobotType];

    private void Awake()
    {
        if (Instance != null) Debug.Log("More, than one instance RobotsData");
        else Instance = this;
    }

    private void Start()
    {
        CustomEvents.OnChangeExperience += ChangeExperience;
    }

    public void SetNewRobotType(RobotType robotType)
    {
        _robotType = robotType;
    }

    public void ChangeExperience(int experience)
    {
        if (!_currentRobotSystem.HaveRobot() || _currentRobotSystem.RobotDeath()) return;

        var maxExp = _experienceInfo.NeedExperienceForNextLevel[CurrentLevel()];
        var currentExp = _robotsExperience[(int)_robotType];

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

        _robotsExperience[(int)_robotType] = currentExp;

        if (_robotType == _robotPanel.GetCurrentRobotType())
        {
            _robotPanel.UpdateLevelAndExperience();
        }
    }

    private void NewLevel()
    {
        _robotsLevel[(int)_robotType]++;

        if (_robotType == _robotPanel.GetCurrentRobotType())
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
