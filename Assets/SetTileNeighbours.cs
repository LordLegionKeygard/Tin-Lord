using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTileNeighbours : MonoBehaviour
{
    public List<TileObject> TileObject;

    public void SetNeighbours()
    {
        for (int i = 0; i < TileObject.Count; i++)
        {
            TileObject[i].NeighbourTiles(new TileObject[] {(i + 20 > TileObject.Count - 1) ? null : TileObject[i + 20],
                                                            (i + 21 > TileObject.Count - 1) ? null : TileObject[i + 21],
                                                            (i + 1 > TileObject.Count - 1) ? null : TileObject[i + 1],
                                                            (i - 20 < 0) ? null : TileObject[i - 20],
                                                            (i - 21 < 0) ? null : TileObject[i - 21],
                                                            (i - 1 < 0) ? null : TileObject[i - 1],
                                                            (i + 19 > TileObject.Count - 1) ? null :TileObject[i + 19],  });

#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(TileObject[i]);
#endif
        }
    }
}
