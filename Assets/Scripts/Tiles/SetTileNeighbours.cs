using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTileNeighbours : MonoBehaviour
{
    public List<TileObject> TileObjects;

    public void SetNeighbours()
    {
        for (int i = 0; i < TileObjects.Count; i++)
        {
            TileObjects[i].SetNeighbourTiles(new TileObject[] {(i + 20 > TileObjects.Count - 1) ? null : TileObjects[i + 20],
                                                            (i + 21 > TileObjects.Count - 1) ? null : TileObjects[i].transform.position.x == 190 ? null : TileObjects[i + 21],
                                                            (i + 1 > TileObjects.Count - 1) ? null : TileObjects[i].transform.position.x == 190 ? null : TileObjects[i + 1],
                                                            (i - 19 < 0) ? null : TileObjects[i].transform.position.x == 190 ? null : TileObjects[i - 19],
                                                            (i - 20 < 0) ? null : TileObjects[i - 20],
                                                            (i - 21 < 0) ? null : TileObjects[i].transform.position.x == 0 ? null : TileObjects[i - 21],
                                                            (i - 1 < 0) ? null : TileObjects[i].transform.position.x == 0 ? null : TileObjects[i - 1],
                                                            (i + 19 > TileObjects.Count - 1) ? null : TileObjects[i].transform.position.x == 0 ? null : TileObjects[i + 19],  });
        }
    }
}
