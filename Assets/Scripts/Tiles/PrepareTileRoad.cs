using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareTileRoad : MonoBehaviour
{
    [SerializeField] private GameObject[] _roads;
    [SerializeField] private GameObject _ground;

    public void SetRoad(int number, int rotation)
    {
        _ground.SetActive(false);
        _roads[number].SetActive(true);
        _roads[number].transform.rotation = Quaternion.Euler(0, rotation, 0);
    }
}
