using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TileObject))]
public class TileObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TileObject t = (TileObject)target;
        if (GUILayout.Button("Spawn Tile")) t.SpawnTile();
    }
}
