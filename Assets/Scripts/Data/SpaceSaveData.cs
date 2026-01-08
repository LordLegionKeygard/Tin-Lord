[System.Serializable]
public class SpaceSaveData
{
    public int Act;
    public int Quants;
    public int AiCores;
    public int PreviousActsShards;
    public bool PrologueCompleted;
    public bool EndGame;
    public HangarCommandCenterData HangarCommandCenterData;
    public bool[] BuildingsLearned;
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
    public WeaponData WeaponData;
    public bool[] OpenedSkills;
    public float[] MainResourcesData;
}

[System.Serializable]
public class WeaponData
{
    public int LeftWeapon;
    public int LeftWeaponLevel;
    public int RightWeapon;
    public int RightWeaponLevel;

}