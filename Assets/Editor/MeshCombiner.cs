using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class MeshCombiner : EditorWindow
{
    [MenuItem("Tools/Combine Mesh Renderers")]
    public static void ShowWindow()
    {
        GetWindow<MeshCombiner>("Combine Mesh Renderers");
    }

    private void OnGUI()
    {
        GUILayout.Label("Объединение MeshRenderers", EditorStyles.boldLabel);

        if (GUILayout.Button("Объединить и сохранить Mesh"))
        {
            CombineSelectedMeshes();
        }
    }

    private void CombineSelectedMeshes()
    {
        GameObject[] selectedObjects = Selection.gameObjects;
        if (selectedObjects.Length == 0)
        {
            Debug.LogWarning("Не выбрано ни одного объекта!");
            return;
        }

        List<MeshFilter> meshFilters = new List<MeshFilter>();
        foreach (GameObject obj in selectedObjects)
        {
            MeshFilter mf = obj.GetComponent<MeshFilter>();
            MeshRenderer mr = obj.GetComponent<MeshRenderer>();

            if (mf != null && mr != null && mf.sharedMesh != null)
            {
                meshFilters.Add(mf);
            }
        }

        if (meshFilters.Count == 0)
        {
            Debug.LogWarning("Выбранные объекты не содержат подходящих MeshRenderer и MeshFilter.");
            return;
        }

        // Собираем все сабмеши в один список CombineInstance
        List<CombineInstance> combineList = new List<CombineInstance>();
        foreach (var mf in meshFilters)
        {
            Mesh mesh = mf.sharedMesh;
            if (mesh == null) continue;

            for (int sub = 0; sub < mesh.subMeshCount; sub++)
            {
                CombineInstance ci = new CombineInstance
                {
                    mesh = mesh,
                    subMeshIndex = sub,
                    transform = mf.transform.localToWorldMatrix
                };
                combineList.Add(ci);
            }
        }

        // Создаём объединённый Mesh
        Mesh combinedMesh = new Mesh
        {
            indexFormat = UnityEngine.Rendering.IndexFormat.UInt32
        };
        combinedMesh.CombineMeshes(combineList.ToArray(), true, true);

        // Сохраняем Mesh в проекте без создания объекта в сцене
        string path = EditorUtility.SaveFilePanelInProject("Сохранить Mesh", "CombinedMesh", "asset", "Выберите путь для сохранения Mesh");
        if (!string.IsNullOrEmpty(path))
        {
            AssetDatabase.CreateAsset(combinedMesh, path);
            AssetDatabase.SaveAssets();
            Debug.Log("Mesh сохранён по пути: " + path);
        }
        else
        {
            Debug.LogWarning("Сохранение Mesh отменено.");
        }

        Debug.Log("Объединение завершено, объект в сцене не создан.");
    }
}
