using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Хранит дату о глобальных ресурсах и об открытых зданиях
/// </summary>
[System.Serializable]
public class CommandCenterSaveData
{
    public int MemoryFragment;
    public bool[] BuildingsLearned;
}


