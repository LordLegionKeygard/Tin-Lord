#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Linq;
public static class ClearThisTerrainDetails
{
 // Tools → Terrain (Safe) → Clear Details (Selected)
    [MenuItem("Tools/Terrain (Safe)/Clear Details (Selected)")]
    public static void ClearDetailsSelected()
    {
        var terrains = Selection.gameObjects
            .Select(go => go.GetComponent<Terrain>())
            .Where(t => t && t.terrainData != null)
            .Distinct()
            .ToArray();

        if (terrains.Length == 0)
        {
            Debug.LogWarning("[TerrainSafe] Выдели хотя бы один объект с Terrain.");
            return;
        }

        foreach (var t in terrains)
            ClearOneTerrain(t);

        AssetDatabase.SaveAssets();
        Debug.Log($"[TerrainSafe] Готово. Очищено террейнов: {terrains.Length}.");
    }

    private static void ClearOneTerrain(Terrain t)
    {
        var td = t.terrainData;

        // на время очистки выключаем визуализацию деталей
        t.detailObjectDensity  = 0f;
        t.detailObjectDistance = 0f;

        int w = td.detailWidth;
        int h = td.detailHeight;
        int layers = td.detailPrototypes != null ? td.detailPrototypes.Length : 0;

        Debug.Log($"[TerrainSafe] Очищаю '{t.name}'. Layers:{layers}, Map:{w}x{h}");

        if (layers == 0 || w == 0 || h == 0) return;

        const int CHUNK = 256; // размер плитки очистки, безопасно по памяти

        for (int layer = 0; layer < layers; layer++)
        {
            for (int y = 0; y < h; y += CHUNK)
            {
                int hh = Mathf.Min(CHUNK, h - y);
                for (int x = 0; x < w; x += CHUNK)
                {
                    int ww = Mathf.Min(CHUNK, w - x);
                    var zeros = new int[hh, ww];
                    td.SetDetailLayer(x, y, layer, zeros);
                }
            }
        }

        EditorUtility.SetDirty(td);
        Debug.Log($"[TerrainSafe] '{t.name}' очищен.");
    }
}
#endif
