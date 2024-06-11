using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareTileRoad : MonoBehaviour
{
    [SerializeField] private GameObject[] _roads;

    public void SetRoad(int roadNumber, int rotation)
    {
        _roads[roadNumber].SetActive(true);
        _roads[roadNumber].transform.rotation = Quaternion.Euler(0, rotation, 0);
    }
}
