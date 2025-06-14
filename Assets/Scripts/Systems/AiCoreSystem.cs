using UnityEngine;

public class AiCoreSystem : MonoBehaviour
{
    [SerializeField] private int _aiCore;
    [SerializeField] private CellsView _cellsView;

    public int GetAiCores() => _aiCore;

    public void LoadAiCore(int core)
    {
        _aiCore = core;
        _cellsView.UpdateCellSlotsView(_aiCore);
        CheckAiDeath();
    }

    public void ChangeAiCores(int value)
    {
        _aiCore += value;
        _cellsView.UpdateCellSlotsView(_aiCore);
        CheckAiDeath();
    }

    public void CheckAiDeath()
    {
        if (_aiCore <= 0)
        {
            Debug.Log("AiDeath");
            // конец игры
        }
    }
}
