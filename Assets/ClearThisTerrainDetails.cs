#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Linq;

public static class RebuildTerrainWithoutDetails
{
    // Меню: Tools → Terrain (Safe) → Rebuild Without Details (Selected)
    [MenuItem("Tools/Terrain (Safe)/Rebuild Without Details (Selected)")]
    public static void RebuildSelected()
    {
        var terrains = Selection.gameObjects
            .Select(go => go.GetComponent<Terrain>())
            .Where(t => t && t.terrainData != null)
            .Distinct()
            .ToArray();

        if (terrains.Length == 0)
        {
            Debug.LogWarning("[TerrainSafe] Выдели объект(ы) с Terrain.");
            return;
        }

        foreach (var t in terrains)
        {
            try { RebuildOne(t); }
            catch (System.Exception e)
            {
                Debug.LogError($"[TerrainSafe] Ошибка на '{t.name}': {e}");
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log($"[TerrainSafe] Готово. Пересобрано террейнов: {terrains.Length} (детали удалены).");
    }

    private static void RebuildOne(Terrain terrain)
    {
        var src = terrain.terrainData;
        string srcPath = AssetDatabase.GetAssetPath(src);

        // Куда положим новый asset
        string dir = string.IsNullOrEmpty(srcPath) ? "Assets" : Path.GetDirectoryName(srcPath);
        string baseName = string.IsNullOrEmpty(srcPath) ? terrain.name : Path.GetFileNameWithoutExtension(srcPath);
        string newPath = AssetDatabase.GenerateUniqueAssetPath(Path.Combine(dir, baseName + "_NoDetails.asset"));

        // 1) Создаём пустой TerrainData
        var dst = new TerrainData();

        // Размеры террейна (мир)
        dst.size = src.size;

        // --- HEIGHTMAP ---
        // Совпадающее разрешение карты высот
        int hRes = src.heightmapResolution;
        dst.heightmapResolution = hRes;
        CopyHeightsChunked(src, dst, 512);

        // --- SPLAT/TERRAIN LAYERS ---
        // В URP/HDRP используются TerrainLayer-ы — просто копируем ссылки
        dst.terrainLayers = src.terrainLayers;

        // Совпадающее разрешение альфа-карт
        dst.alphamapResolution = src.alphamapResolution;
        CopyAlphamapsChunked(src, dst, 256);

        // --- TREES ---
        dst.treePrototypes = src.treePrototypes;      // типы деревьев
        dst.treeInstances  = src.treeInstances;       // инстансы деревьев

        // --- DETAILS: НЕ КОПИРУЕМ ---
        // Ставим маленькое разрешение и пустые слои, чтобы гарантированно не падать
        dst.SetDetailResolution(64, 8);
        dst.detailPrototypes = System.Array.Empty<DetailPrototype>();

        // Создаём asset и привязываем
        AssetDatabase.CreateAsset(dst, newPath);
        AssetDatabase.ImportAsset(newPath);

        Undo.RecordObject(terrain, "Assign TerrainData (No Details)");
        terrain.terrainData = dst;

        // Безопасно: отключим расстояние/плотность деталей на объекте
        terrain.detailObjectDistance = 0f;
        terrain.detailObjectDensity  = 0f;

        Debug.Log($"[TerrainSafe] '{terrain.name}': создан {newPath} без деталей.");
    }

    // ------- helpers -------

    private static void CopyHeightsChunked(TerrainData src, TerrainData dst, int chunk)
    {
        int res = src.heightmapResolution; // квадрат
        var tmp = new float[chunk, chunk];

        for (int y = 0; y < res; y += chunk)
        {
            int hh = Mathf.Min(chunk, res - y);
            for (int x = 0; x < res; x += chunk)
            {
                int ww = Mathf.Min(chunk, res - x);
                var block = src.GetHeights(x, y, ww, hh);
                // У GetHeights/SetHeights индексы (y,x)
                dst.SetHeightsDelayLOD(x, y, block);
            }
        }
        dst.SyncHeightmap(); // применить отложенные изменения
    }

    #if UNITY_2019_3_OR_NEWER
    private static void CopyHolesChunked(TerrainData src, TerrainData dst, int chunk)
    {
        int w = src.holesResolution;
        int h = src.holesResolution;
        for (int y = 0; y < h; y += chunk)
        {
            int hh = Mathf.Min(chunk, h - y);
            for (int x = 0; x < w; x += chunk)
            {
                int ww = Mathf.Min(chunk, w - x);
                var block = src.GetHoles(x, y, ww, hh);
                dst.SetHoles(x, y, block);
            }
        }
    }
    #endif

    private static void CopyAlphamapsChunked(TerrainData src, TerrainData dst, int chunk)
    {
        int w = src.alphamapWidth;
        int h = src.alphamapHeight;
        int layers = src.alphamapLayers;

        for (int y = 0; y < h; y += chunk)
        {
            int hh = Mathf.Min(chunk, h - y);
            for (int x = 0; x < w; x += chunk)
            {
                int ww = Mathf.Min(chunk, w - x);
                var block = src.GetAlphamaps(x, y, ww, hh); // [hh, ww, layers]
                dst.SetAlphamaps(x, y, block);
            }
        }
    }
}
#endif
