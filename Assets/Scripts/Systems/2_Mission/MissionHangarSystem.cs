using UnityEngine;

public class MissionHangarSystem : MonoBehaviour
{
    private int _currentRobot;
    public int GetCurrentRobot() => _currentRobot;

    public void LoadHangarData(HangarCommandCenterData data)
    {
        _currentRobot = data.Robot;
    }

    public string GetRepairText()
    {
        return GetArbalesterRepairBonus() != 1 ? $"{Language.TextStatic[4]}\n<color={Colors.HexLightGreen}>-{WorldGameInfo.PatchPassiveAbility}%</color>" : Language.TextStatic[4];
    }

    public float GetArbalesterRepairBonus()
    {
        return _currentRobot == (int)HangarRobotType.Arbalester ? 1f - WorldGameInfo.PatchPassiveAbility * 0.01f : 1;
    }

    public float GetTitanBuildingHealthBonus()
    {
        return _currentRobot == (int)HangarRobotType.Titan ? 1f + WorldGameInfo.TitanPassiveAbility * 0.01f : 1;
    }

    public float GetSniperDamageBonus()
    {
        return _currentRobot == (int)HangarRobotType.Sniper ? 1f + WorldGameInfo.AimBotPassiveAbility * 0.01f : 1;
    }
}
