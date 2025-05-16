/// <summary>
/// Хранит дату о глобальных ресурсах и об открытых зданиях
/// </summary>
[System.Serializable]
public class CommandCenterSaveData
{
    public float MemoryFragments;
    public int LastOpenedMissionId;
    public bool PrologueCompleted;
    public bool TutorialCompleted;
    public bool[] BuildingsLearned;
}


