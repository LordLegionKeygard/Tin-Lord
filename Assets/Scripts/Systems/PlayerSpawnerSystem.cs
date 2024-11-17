using UnityEngine;
using Zenject;

public class PlayerSpawnerSystem : MonoBehaviour
{
    [Inject] DiContainer _diContainer;
    [SerializeField] private MapBuilder _mapBuilder;
    [SerializeField] private GameObject[] _playerPrefabs;
    [SerializeField] private Transform _parent;

    private void Start()
    {

    }

    private void OnDestroy()
    {

    }

    private void SpawnPlayer(PlayerType playerType)
    {
        var roadTiles = _mapBuilder.GetRoadTiles();


        int randomIndex = Random.Range(0, roadTiles.Count);
        var randomTile = roadTiles[randomIndex];

        Vector3 spawnPosition = randomTile.transform.position;

        var player = _diContainer.InstantiatePrefab(_playerPrefabs[(int)playerType], spawnPosition, Quaternion.identity, _parent);

        var patrolPath = player.GetComponent<PlayerPatrolPath>();

        patrolPath.InitializePatrolPoints(roadTiles, randomIndex);
    }
}

[System.Serializable]
public enum PlayerType
{
    Tank = 0,
    Sniper = 1,
    Engineer = 2,
}
