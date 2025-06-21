[System.Serializable]
public class CommandCenterSaveData
{
    public int Quants;
    public int AiCores;
    public float[] MainResourcesData;
    public bool PrologueCompleted;
    public bool TutorialCompleted;
    public bool[] BuildingsLearned;
    public bool[] OpenedSkills;
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