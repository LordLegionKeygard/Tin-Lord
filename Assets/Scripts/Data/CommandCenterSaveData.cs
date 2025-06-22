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
    public int NodeId;               // к какому узлу относится
    public int MissionDeckIndex;     // какой элемент MissionDeck
    public int LandscapeId;          // какой Landscape взяли
    public ObjectiveSave[] SavedObjectives; // цели с зафиксированным количеством
}