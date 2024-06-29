using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTileNeighbours : MonoBehaviour
{
    public List<GroundTile> GroundTiles;

    public void SetNeighbours()
    {
        for (int i = 0; i < GroundTiles.Count; i++)
        {
            GroundTiles[i].SetNeighbourTiles(new GroundTile[] {(i + 20 > GroundTiles.Count - 1) ? null : GroundTiles[i + 20],
                                                            (i + 21 > GroundTiles.Count - 1) ? null : GroundTiles[i].transform.position.x == 190 ? null : GroundTiles[i + 21],
                                                            (i + 1 > GroundTiles.Count - 1) ? null : GroundTiles[i].transform.position.x == 190 ? null : GroundTiles[i + 1],
                                                            (i - 19 < 0) ? null : GroundTiles[i].transform.position.x == 190 ? null : GroundTiles[i - 19],
                                                            (i - 20 < 0) ? null : GroundTiles[i - 20],
                                                            (i - 21 < 0) ? null : GroundTiles[i].transform.position.x == 0 ? null : GroundTiles[i - 21],
                                                            (i - 1 < 0) ? null : GroundTiles[i].transform.position.x == 0 ? null : GroundTiles[i - 1],
                                                            (i + 19 > GroundTiles.Count - 1) ? null : GroundTiles[i].transform.position.x == 0 ? null : GroundTiles[i + 19],  });
        }
    }
}
