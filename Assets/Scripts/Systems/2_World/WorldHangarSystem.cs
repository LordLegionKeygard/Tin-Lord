using UnityEngine;

public class WorldHangarSystem : MonoBehaviour
{
    private int _currentRobot;
    public int GetCurrentRobot() => _currentRobot;

    public void LoadHangarData(HangarCommandCenterData data)
    {
        _currentRobot = data.Robot;
    }

    public string GetRepairText()
    {
        return GetPatchRepairBonus() != 1 ? $"{Language.TextStatic[4]}\n<color={Colors.HexColorLightGreen}>-{WorldGameInfo.PatchPassiveAbility}%</color>" : Language.TextStatic[4];
    }

    public float GetPatchRepairBonus()
    {
        return _currentRobot == (int)HangarRobotType.Patch ? 1f - WorldGameInfo.PatchPassiveAbility * 0.01f : 1;
    }

    public float GetTitanBuildingHealthBonus()
    {
        return _currentRobot == (int)HangarRobotType.Titan ? 1f + WorldGameInfo.TitanPassiveAbility * 0.01f : 1;
    }

    public float GetAimBotDamageBonus()
    {
        return _currentRobot == (int)HangarRobotType.AimBot ? 1f + WorldGameInfo.AimBotPassiveAbility * 0.01f : 1;
    }
}
