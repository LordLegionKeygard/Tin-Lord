using UnityEngine;
using Zenject;

public class PlayerSpawnerSystem : MonoBehaviour
{
    [Inject] DiContainer _diContainer;
    [SerializeField] private MapBuilder _mapBuilder;
    [SerializeField] private GameObject _playerPrefab;

    private void Start()
    {
        CustomEvents.OnSpawnRoadComplete += SpawnPlayer;
    }

    private void OnDestroy()
    {
        CustomEvents.OnSpawnRoadComplete -= SpawnPlayer;
    }

    private void SpawnPlayer()
    {
        var roadTiles = _mapBuilder.GetRoadTiles();


        int randomIndex = Random.Range(0, roadTiles.Count);
        var randomTile = roadTiles[randomIndex];

        Vector3 spawnPosition = randomTile.transform.position;

        var player = _diContainer.InstantiatePrefab(_playerPrefab, spawnPosition, Quaternion.identity, null);

        var patrolPath = player.GetComponent<PlayerPatrolPath>();

        patrolPath.InitializePatrolPoints(roadTiles, randomIndex);
    }
}
