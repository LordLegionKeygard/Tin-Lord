using UnityEngine;

public class AiCoreSystem : MonoBehaviour
{
    [SerializeField] private int _aiCore;
    [SerializeField] private CellsView _cellsView;
    [SerializeField] private DialogueSequence _endGameDialogue;
    [SerializeField] private CompleteGameSystem _spaceEndGameSystem;

    public int GetAiCores() => _aiCore;

    public void LoadAiCore(int core)
    {
        _aiCore = core;
        _cellsView.UpdateCellSlotsView(_aiCore);
        CheckAiDeath();
    }

    public void ChangeAiCores(int value)
    {
        _aiCore += value > WorldGameInfo.MaxAiCores ? WorldGameInfo.MaxAiCores : value;
        _cellsView.UpdateCellSlotsView(_aiCore);
        CheckAiDeath();
    }

    public void CheckAiDeath()
    {
        if (_aiCore > 0) return;

        _spaceEndGameSystem.ShowEndGamePanel(_endGameDialogue);
    }
}
