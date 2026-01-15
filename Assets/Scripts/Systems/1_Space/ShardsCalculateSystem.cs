using System.Linq;
using UnityEngine;
using Zenject;

public class ShardsCalculateSystem : MonoBehaviour
{
    [Inject] private SpaceSaveGame _spaceSaveGame;
    private int _bossShardReward = 5;
    private int _missionsCompleted;
    private int _eventsCompleted;
    private int _tradersCompleted;
    private int _bossesCompleted;
    private int _calculatedShards;
    public int GetCalculatedShards() => _calculatedShards;

    public int CalculateShardsForThisAct()
    {
        var saveData = _spaceSaveGame.SpaceSaveData;

        _missionsCompleted = saveData.Map.Nodes.Count(n => n.IsCompleted && n.NodeType == NodeType.Mission);

        _eventsCompleted = saveData.Map.Nodes.Count(n => n.IsCompleted && (n.NodeType == NodeType.Event || n.NodeType == NodeType.RestEvent));

        _tradersCompleted = saveData.Map.Nodes.Count(n => n.IsCompleted && (n.NodeType is NodeType.ResourceTrader or NodeType.SkillTrader or NodeType.WeaponTrader));

        _bossesCompleted = saveData.Map.Nodes.Count(n => n.IsCompleted && n.NodeType == NodeType.Boss);

        var actNumber = Mathf.Max(1, saveData.Act + 1);
        var baseShards =
        _missionsCompleted * _missionsCompleted +
        _eventsCompleted +
        _tradersCompleted +
        _bossesCompleted * _bossShardReward;
        _calculatedShards = baseShards * actNumber;

        return _calculatedShards;
    }

    public void CalculateAllShards()
    {
        var saveData = _spaceSaveGame.SpaceSaveData;

        var shardsThisAct = CalculateShardsForThisAct();
        var shardsPreviousActs = saveData.PreviousActsShards;

        _calculatedShards = shardsPreviousActs + shardsThisAct;
    }
}
