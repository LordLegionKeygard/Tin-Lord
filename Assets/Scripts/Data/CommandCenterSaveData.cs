/// <summary>
/// Хранит дату о глобальных ресурсах и об открытых зданиях
/// </summary>
[System.Serializable]
public class CommandCenterSaveData
{
    public float MemoryFragments;
    public bool PrologueCompleted;
    public bool TutorialCompleted;
    public bool[] BuildingsLearned;

    public SavedMapData Map;
    public SelectedMissionData CurrentMission; 
}

[System.Serializable]
public class SelectedMissionData          // ← новое
{
    public int NodeId;        // индекс узла на карте (для сейва/лоада)
    public int LandscapeId;   // индекс в AllMissionsInfo.Landscapes
    public int ObjectiveId;   // индекс в AllMissionsInfo.Objectives
    public int SpawnerId;     // индекс в AllMissionsInfo.EnemiesSpawnerInformation
}


