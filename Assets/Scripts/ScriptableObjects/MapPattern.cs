using UnityEngine;

[CreateAssetMenu(menuName = "TinLord/Missions/MapPattern")]
public class MapPattern : ScriptableObject
{
    public MapPatternEnum[] Sequence = { MapPatternEnum.NonMission,
                                         MapPatternEnum.NonMission,
                                         MapPatternEnum.Mission };
}

