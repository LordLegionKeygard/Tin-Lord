using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ConfigLoaderBuildings))]
public class BuildingsConfigLoaderEditorButton : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ConfigLoaderBuildings buildings = (ConfigLoaderBuildings)target;
        if (GUILayout.Button("Load Buildings")) buildings.Load();
    }
}
