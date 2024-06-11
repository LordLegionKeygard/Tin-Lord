using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MapBuilder : MonoBehaviour
{
    [SerializeField] private GameObject _tile;
    [SerializeField] private Transform _parentTransform;
    [SerializeField] private SetTileNeighbours _setTileNeighbours;
    [SerializeField] private TileObject[,] _tileObjects = new TileObject[16, 20];
    [SerializeField] private List<TileObject> _roadTiles;

    [Header("Road")]
    private int _iterations = 0;
    private int _startX = 6;
    private int _startY = 4;


    private void Start()
    {
        SpawnTiles();
        _setTileNeighbours.SetNeighbours();
        SpawnRoad();
    }

    private void SpawnTiles()
    {
        for (int i = 0; i < 16; i++)
        {
            for (int k = 0; k < 20; k++)
            {
                var p = Instantiate(_tile, new Vector3(k * 10, 10.8f, i * 10), Quaternion.identity);
                _tileObjects[i, k] = p.GetComponent<TileObject>();
                _setTileNeighbours.TileObject.Add(_tileObjects[i, k]);
                p.transform.SetParent(_parentTransform);
            }
        }
    }

    private void SpawnRoad()
    {
        SetNextTile(_startX, _startY);
    }

    private async void SetNextTile(int nextX, int nextY)
    {
        await Task.Delay(100);

        _tileObjects[nextX, nextY].SetRoadTile(TilesSystem.Instance.TakeTile(TileView.Road));
        _roadTiles.Add(_tileObjects[nextX, nextY]);

        var rnd = Random.Range(0, 3);

        if (nextX < 7 && nextY < 10) //из нижнего левого угла идем в 
        {
            if (rnd == 0 || nextX <= 4) //право
            {
                SetNextTile(nextX, nextY + 1);
            }
            else //вниз
            {
                SetNextTile(nextX - 1, nextY);
            }
        }
        else if (nextX <= 7 && nextY >= 10) //из нижнего правого угла идем в 
        {
            if ((rnd == 0 || nextY >= 15) && nextY != 10) //вверх
            {
                SetNextTile(nextX + 1, nextY);
            }
            else //вправо
            {
                SetNextTile(nextX, nextY + 1);
            }
        }
        else if (nextX >= 7 && nextY >= 10) //из верхнего правого угла идем в 
        {
            if ((rnd == 0 || nextX >= 11) && nextX != 8) //влево
            {
                SetNextTile(nextX, nextY - 1);
            }
            else //вверх
            {
                SetNextTile(nextX + 1, nextY);
            }
        }
        else if (nextX > 7 && nextY < 10) //из верхнего левого идем в
        {
            if (nextY == _startY)
            {
                SetNextTile(nextX - 1, nextY);
            }
            else if (nextX == 8 && nextY >= 5) //проверка если уйдет резко вниз
            {
                SetNextTile(nextX, nextY - 1);
            }

            else if ((rnd == 0 || nextX >= 11) && (nextX != 7 && nextY >= 5)) //вниз
            {
                SetNextTile(nextX - 1, nextY);
            }
            else //влево
            {
                SetNextTile(nextX, nextY - 1);
            }
        }


        if (_iterations > 45 || (nextY == _startY && _startX == nextX - 1))
        {
            CustomEvents.FirePrepareRoads();
            return;
        }
        _iterations++;
    }
}
