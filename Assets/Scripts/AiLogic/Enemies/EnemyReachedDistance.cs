using UnityEngine;
using Pathfinding;

public class EnemyReachedDistance : MonoBehaviour
{
    private AIPath _aiPath;


    private void Awake()
    {
        _aiPath = GetComponent<AIPath>();
    }
    public void UpdateAiEndReachedDistance(Tile tile)
    {
        if (tile != null)
        {
            _aiPath.endReachedDistance = tile.IsFourTile ? WorldGameInfo.EnemyReachedFourTileDistance : WorldGameInfo.EnemyReachedTileDistance;
        }
        else
        {
            _aiPath.endReachedDistance = WorldGameInfo.EnemyReachedRobotDistance;
        }
    }
}
