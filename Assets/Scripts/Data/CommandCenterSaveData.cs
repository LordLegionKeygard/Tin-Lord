/// <summary>
/// Хранит дату о глобальных ресурсах и об открытых зданиях
/// </summary>
[System.Serializable]
public class CommandCenterSaveData
{
    public int AiCores;
    public float MemoryFragments;
    public bool PrologueCompleted;
    public bool TutorialCompleted;
    public bool[] BuildingsLearned;

    public SavedMapData Map;
    public SelectedMissionData CurrentMission;
}

[System.Serializable]
public class SelectedMissionData
{
    public int NodeId;
    public int LandscapeId;
    public int ObjectiveId;
    public int SpawnerId;
}


