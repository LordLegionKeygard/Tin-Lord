using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class TileMapBuilder : MonoBehaviour
{
    [Inject] private DiContainer _diContainer;
    [Inject] private TilesSystem _tilesSystem;


    [SerializeField] private AstarPath _astarPath;
    [SerializeField] private GameObject _tile;
    [SerializeField] private Transform _parentTransform;
    [SerializeField] private AllTileObjects _allTileObjects;
    [SerializeField] private GameObject[,] _tileObjects;

    [Header("Terrain")]
    [SerializeField] private GameObject[] _terrains;
    [SerializeField] private Transform _environmentParentTransform;

    [Header("Road")]
    [SerializeField] private List<TileObject> _roadTiles = new();
    private int _iterations = 0;
    private int _startX;
    private int _startY;
    public List<TileObject> GetRoadTiles() => _roadTiles;

    [Header("Map")]
    private int _mapWidth;
    private int _mapLength;
    private int _mapEdge;
    private int _startPosEdge;


    public int[] GetRoadTilesId()
    {
        var tilesId = new int[_roadTiles.Count];
        for (int i = 0; i < _roadTiles.Count; i++)
        {
            tilesId[i] = _roadTiles[i].GetId();
        }
        return tilesId;
    }

    public void LoadRoadTiles(int[] tilesId, bool isStartMission)
    {
        if (isStartMission) return;

        for (int i = 0; i < tilesId.Length; i++)
        {
            _roadTiles.Add(_allTileObjects.TileObjects[tilesId[i]]);
        }
    }

    public void BuildMap(bool isStartMission)
    {
        SetMapSize();
        SetTerrain();
        _astarPath.Scan();
        SetStartCoordinates();
        SpawnTiles();
        _allTileObjects.SetNeighbours(_mapLength);

        if (isStartMission) SpawnRoad();
    }

    private void SetMapSize()
    {
        var mission = CurrentMissionInfo.Instance.GetCurrentMission();
        _mapWidth = mission.MapWidth;
        _mapLength = mission.MapLength;
        _mapEdge = mission.MapEdge;
        _startPosEdge = mission.StartPosEdge;
        _tileObjects = new GameObject[_mapWidth, _mapLength];
    }

    private void SetTerrain()
    {
        var mission = CurrentMissionInfo.Instance.GetCurrentMission();
        _terrains[mission.MissionId].SetActive(true);
    }

    private void SetStartCoordinates()
    {
        _startX = _mapWidth / 2 - 1;
        _startY = Random.Range(1, _mapLength / 2 - _startPosEdge);
    }

    private void SpawnTiles()
    {
        for (int i = 0; i < _mapWidth; i++)
        {
            for (int k = 0; k < _mapLength; k++)
            {
                var newObject = _diContainer.InstantiatePrefab(
                    _tile,
                    new Vector3(k * 10, 0, i * 10),
                    Quaternion.identity,
                    null
                );

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
        // await Task.Delay(40);

        var tileObject = _tileObjects[nextX, nextY];
        tileObject.GetComponent<TileRoad>().SetRoadTile(
            _tilesSystem.GetGroundTileForEnum(GroundTileViewEnum.Road)
        );

        _roadTiles.Add(tileObject.GetComponent<TileObject>());

        if (_iterations > 100 || (nextY == _startY && _startX == nextX - 1))
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
            //       если достигли нижнего края  (основа)направо        (доп)вниз
            return (rnd == 0 || nextX <= _mapEdge) ? (nextX, nextY + 1) : (nextX - 1, nextY);
        }
        else if (IsBottomRight(nextX, nextY))
        {
            //                 если достигли правого края                                                  (основа)наверх        (доп)направо
            return ((rnd == 0 || nextY >= (_mapLength - _mapEdge - 1)) && nextY != _mapLength / 2 + 1) ? (nextX + 1, nextY) : (nextX, nextY + 1);
        }
        else if (IsTopRight(nextX, nextY))
        {
            //                если достигли верхнего края                                (основа)налево          (доп)вверх
            return ((rnd == 0 || nextX >= (_mapWidth - _mapEdge - 1)) && nextX != _mapWidth / 2 + 1) ? (nextX, nextY - 1) : (nextX + 1, nextY);
        }
        else if (IsTopLeft(nextX, nextY))
        {
            // если совпали по оси Y со стартовой точкой идем вниз до нее
            if (nextY == _startY) return (nextX - 1, nextY);

            // если дошли до середины, но не совпали с осью Y старта, то идем до нее налево
            if (nextX == (_mapWidth / 2) + 1) return (nextX, nextY - 1);

            // если почти достигли середины                        основа(вниз)         доп(налево)              
            return (rnd == 0 || nextY == _mapLength / 2 - 1) ? (nextX, nextY - 1) : (nextX - 1, nextY);
        }

        return (nextX, nextY);
    }

    private bool IsBottomLeft(int x, int y) => x < _mapWidth / 2 && y <= _mapLength / 2;
    private bool IsBottomRight(int x, int y) => x <= _mapWidth / 2 && y > _mapLength / 2;
    private bool IsTopRight(int x, int y) => x >= _mapWidth / 2 && y >= _mapLength / 2;
    private bool IsTopLeft(int x, int y) => x > _mapWidth / 2 && y < _mapLength / 2;
}
