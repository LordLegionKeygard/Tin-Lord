/// <summary>
/// Хранит дату о глобальных ресурсах и об открытых зданиях
/// </summary>
[System.Serializable]
public class CommandCenterSaveData
{
    public float MemoryFragments;
    public bool[] BuildingsLearned;
    public int LastOpenedMissionId;
}


