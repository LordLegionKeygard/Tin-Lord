using UnityEngine;

public class RobotLevel : MonoBehaviour
{
    [SerializeField] private RobotInformation _robotInformation;
    public RobotInformation GetRobotInformation() => _robotInformation;
    [SerializeField] private int _level;
    public int GetLevel() => _level;

    public void SetLevel(int level)
    {
        
    }
}
