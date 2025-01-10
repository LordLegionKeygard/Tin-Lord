using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class TileMapBuilder : MonoBehaviour
{
    [Inject] private DiContainer _diContainer;
    [Inject] private TilesSystem _tilesSystem;
    [SerializeField] private GameObject _tile;
    [SerializeField] private Transform _parentTransform;
    [SerializeField] private AllTileObjects _allTileObjects;
    [SerializeField] private GameObject[,] _tileObjects = new GameObject[WorldGameInfo.MapWidth, WorldGameInfo.MapLength];
    [SerializeField] private GameObject[] _terrains;

    [Header("Road")]
    private int _iterations = 0;
    private int _startX = 6;
    private int _startY = 4;
    [SerializeField] private List<TileObject> _roadTiles = new(); // Список тайлов дороги в правильном порядке
    public List<TileObject> GetRoadTiles() => _roadTiles;

    public int[] GetRoadTilesId()
    {
        var tilesId = new int[_roadTiles.Count];

        for (int i = 0; i < _roadTiles.Count; i++)
        {
            tilesId[i] = _roadTiles[i].GetId();
        }

        return tilesId;
    }

    public void LoadRoadTiles(int[] tilesId)
    {
        for (int i = 0; i < tilesId.Length; i++)
        {
            _roadTiles.Add(_allTileObjects.TileObjects[tilesId[i]]);
        }
    }

    public void BuildMap(bool isStartMission)
    {
        _terrains[(int)CurrentMissionInfo.Instance.CurrentMission().TerrainEnum].SetActive(true);
        SpawnTiles();
        _allTileObjects.SetNeighbours();
        //показать название уровня?
        if (isStartMission)
        {
            SpawnRoad();
        }
    }

    private void SpawnTiles()
    {
        for (int i = 0; i < 16; i++)
        {
            for (int k = 0; k < 20; k++)
            {
                var newObject = _diContainer.InstantiatePrefab(_tile, new Vector3(k * 10, 10.8f, i * 10), Quaternion.identity, null);
                _tileObjects[i, k] = newObject;
                _allTileObjects.TileObjects.Add(_tileObjects[i, k].GetComponent<TileObject>());
                newObject.transform.SetParent(_parentTransform);
            }
        }
    }

    private void SpawnRoad()
    {
        SetNextTile(_startX, _startY);
    }

    private async void SetNextTile(int nextX, int nextY)
    {
        await Task.Delay(40);

        var tileObject = _tileObjects[nextX, nextY];
        tileObject.GetComponent<TileRoad>().SetRoadTile(_tilesSystem.GetGroundTileForEnum(GroundTileViewEnum.Road));

        _roadTiles.Add(tileObject.GetComponent<TileObject>()); // Добавляем тайл дороги в список

        if (_iterations > 45 || (nextY == _startY && _startX == nextX - 1))
        {
            await Task.Delay(200);
            CustomEvents.FireSpawnRoadComplete();
            return;
        }

        (int newNextX, int newNextY) = GetNextCoordinates(nextX, nextY);

        _iterations++;
        SetNextTile(newNextX, newNextY);
    }

    private (int, int) GetNextCoordinates(int nextX, int nextY)
    {
        var rnd = Random.Range(0, 3);

        if (IsBottomLeft(nextX, nextY))
        {
            return (rnd == 0 || nextX <= 4) ? (nextX, nextY + 1) : (nextX - 1, nextY);
        }
        else if (IsBottomRight(nextX, nextY))
        {
            return ((rnd == 0 || nextY >= 15) && nextY != 10) ? (nextX + 1, nextY) : (nextX, nextY + 1);
        }
        else if (IsTopRight(nextX, nextY))
        {
            return ((rnd == 0 || nextX >= 11) && nextX != 8) ? (nextX, nextY - 1) : (nextX + 1, nextY);
        }
        else if (IsTopLeft(nextX, nextY))
        {
            if (nextY == _startY) return (nextX - 1, nextY);
            if (nextX == 8 && nextY >= 5) return (nextX, nextY - 1);
            return ((rnd == 0 || nextX >= 11) && nextY >= 5 && nextY != 9) ? (nextX - 1, nextY) : (nextX, nextY - 1);
        }

        return (nextX, nextY); // На случай, если ни одно из условий не выполнится.
    }

    private bool IsBottomLeft(int x, int y) => x < 7 && y < 10;
    private bool IsBottomRight(int x, int y) => x <= 7 && y >= 10;
    private bool IsTopRight(int x, int y) => x >= 7 && y >= 10;
    private bool IsTopLeft(int x, int y) => x > 7 && y < 10;
}

