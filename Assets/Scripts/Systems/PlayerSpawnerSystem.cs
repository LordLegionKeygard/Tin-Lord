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
        // Получаем список тайлов дороги из MapBuilder
        var roadTiles = _mapBuilder.GetRoadTiles();


        int randomIndex = Random.Range(0, roadTiles.Count);
        GameObject randomTile = roadTiles[randomIndex];

        Vector3 spawnPosition = randomTile.transform.position;

        var player = _diContainer.InstantiatePrefab(_playerPrefab, spawnPosition, Quaternion.identity, null);

        // Передаём список точек патруля в PlayerPatrolPath
        var patrolPath = player.GetComponent<PlayerPatrolPath>();

        patrolPath.InitializePatrolPoints(roadTiles, randomIndex);
    }
}
