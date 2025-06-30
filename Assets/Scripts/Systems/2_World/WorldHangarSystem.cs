using UnityEngine;

public class WorldHangarSystem : MonoBehaviour
{
    private int _currentRonot;
    public int GetCurrentRobot() => _currentRonot;
    public void LoadHangarData(HangarCommandCenterData data)
    {
        _currentRonot = data.Robot;
    }
}
