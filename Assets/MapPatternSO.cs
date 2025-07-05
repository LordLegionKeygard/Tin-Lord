using UnityEngine;

[CreateAssetMenu(menuName = "TinLord/Missions/MapPattern")]
public class MapPatternSO : ScriptableObject
{
    public MapPatternEnum[] Sequence = { MapPatternEnum.NonMission,
                                         MapPatternEnum.NonMission,
                                         MapPatternEnum.Mission };
}

