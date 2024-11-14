using System.Collections.Generic;
using UnityEngine;

public class PlayerPatrolPath : MonoBehaviour
{
    [SerializeField] private PlayerPatrolState _playerPatrolState;

    public void InitializePatrolPoints(List<GameObject> roadTiles, int startIndex)
    {
        List<Vector3> patrolPoints = new List<Vector3>();

        foreach (var tile in roadTiles)
        {
            if (tile != null)
            {
                patrolPoints.Add(tile.transform.position);
            }
        }

        _playerPatrolState.InitializePatrol(startIndex, patrolPoints);
    }
}
