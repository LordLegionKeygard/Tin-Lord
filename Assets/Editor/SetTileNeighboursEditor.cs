using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SetTileNeighbours))]
public class SetTileNeighboursEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SetTileNeighbours t = (SetTileNeighbours)target;
        if (GUILayout.Button("Set Tile Neighbours")) t.SetNeighbours();
    }
}

