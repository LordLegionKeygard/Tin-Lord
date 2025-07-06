[System.Serializable]
public class SpaceSaveData
{
    public int Quants;
    public int AiCores;
    public HangarCommandCenterData HangarCommandCenterData;
    public float[] MainResourcesData;
    public bool[] BuildingsLearned;
    public bool[] OpenedSkills;
    public bool PrologueCompleted;
    public bool TutorialCompleted;
    public SelectedMissionData CurrentMission;
    public SavedMapData Map;
}

[System.Serializable]
public class SelectedMissionData
{
    public int NodeId;               // к какому узлу относится
    public int MissionDeckIndex;     // какой элемент MissionDeck
    public int LandscapeId;          // какой Landscape взяли
    public ObjectiveSave[] SavedObjectives; // цели с зафиксированным количеством
}

[System.Serializable]
public class HangarCommandCenterData
{
    public int Robot;
    public int Drone;
}