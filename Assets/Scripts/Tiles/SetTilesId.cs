using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTilesId : MonoBehaviour
{
    [SerializeField] private SetTileNeighbours _setTileNeighbours;
    public void SetId()
    {
        for (int i = 0; i < _setTileNeighbours.GroundTiles.Count; i++)
        {
            _setTileNeighbours.GroundTiles[i].SetId(i);
        }
    }
}
