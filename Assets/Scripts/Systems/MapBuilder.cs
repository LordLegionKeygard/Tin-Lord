using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class MapBuilder : MonoBehaviour
{
    [Inject] private DiContainer _diContainer;
    [Inject] private TilesSystem _tilesSystem;
    [SerializeField] private GameObject _tile;
    [SerializeField] private Transform _parentTransform;
    [SerializeField] private SetTileNeighbours _setTileNeighbours;
    [SerializeField] private SetTilesId _setTilesId;
    [SerializeField] private GameObject[,] _tileObjects = new GameObject[16, 20];

    [Header("Road")]
    private int _iterations = 0;
    private int _startX = 6;
    private int _startY = 4;


    private void Start()
    {
        SpawnTiles();
        _setTileNeighbours.SetNeighbours();
        _setTilesId.SetId();
        SpawnRoad();
    }

    private void SpawnTiles()
    {
        for (int i = 0; i < 16; i++)
        {
            for (int k = 0; k < 20; k++)
            {
                var newObject = _diContainer.InstantiatePrefab(_tile, new Vector3(k * 10, 10.8f, i * 10), Quaternion.identity, null);
                _tileObjects[i, k] = newObject;
                _setTileNeighbours.GroundTiles.Add(_tileObjects[i, k].GetComponent<GroundTile>());
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

        _tileObjects[nextX, nextY].GetComponent<TileRoad>().SetRoadTile(_tilesSystem.TakeGroundTile(GroundTileViewEnum.Road));

        if (_iterations > 45 || (nextY == _startY && _startX == nextX - 1))
        {
            await Task.Delay(200);
            CustomEvents.FirePrepareRoads();
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

