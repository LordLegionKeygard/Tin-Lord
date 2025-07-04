using System.Linq;
using UnityEngine;
using Zenject;

public class ShardsCalculateSystem : MonoBehaviour
{
    [Inject] private SpaceSaveGame _spaceSaveGame;
    private int _missionsCompleted;
    private int _eventsCompleted;
    private int _tradersCompleted;
    private int _calculatedShards;
    public int GetCalculatedShards() => _calculatedShards;

    public void Calculate()
    {
        var saveData = _spaceSaveGame.SpaceSaveData;

        _missionsCompleted = saveData.Map.Nodes.Count(n => n.IsCompleted && n.NodeType == NodeType.Mission);

        _eventsCompleted = saveData.Map.Nodes.Count(n => n.IsCompleted && (n.NodeType == NodeType.Event || n.NodeType == NodeType.RewardEvent));

        _tradersCompleted = saveData.Map.Nodes.Count(n => n.IsCompleted && (n.NodeType is NodeType.ResourceTrader or NodeType.SkillTrader));

        _calculatedShards =
        _missionsCompleted * _missionsCompleted +
        _eventsCompleted +
        _tradersCompleted;
    }
}
