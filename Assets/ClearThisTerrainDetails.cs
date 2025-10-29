using UnityEngine;

[DisallowMultipleComponent]
[ExecuteAlways]
public class ClearThisTerrainDetails : MonoBehaviour
{
    [Tooltip("Если включить — после очистки удалит сами DetailPrototype-ы (слои травы) из TerrainData.")]
    public bool removeDetailPrototypes = false;

    [Tooltip("Снизить detail resolution до безопасного (уменьшает память у битого террейна).")]
    public bool shrinkDetailResolution = true;

    [Tooltip("Безопасное целевое разрешение карты деталей.")]
    public int safeDetailResolution = 256;

    Terrain _terrain;

    void OnEnable()
    {
        _terrain = GetComponent<Terrain>();
    }

    [ContextMenu("Clear Details (this Terrain)")]
    public void ClearDetails()
    {
        if (_terrain == null || _terrain.terrainData == null)
        {
            Debug.LogWarning("[Terrain] Не найден Terrain на этом объекте.", this);
            return;
        }

        var td = _terrain.terrainData;

        // Временно отключим отрисовку, чтобы редактор не падал
        _terrain.detailObjectDistance = 0f;
        _terrain.detailObjectDensity  = 0f;

        int w = td.detailWidth;
        int h = td.detailHeight;

        // Обнуляем карты плотности для всех слоёв деталей
        var zeros = new int[h, w];
        for (int layer = 0; layer < td.detailPrototypes.Length; layer++)
        {
            td.SetDetailLayer(0, 0, layer, zeros);
        }

        // Опционально уменьшаем resolution (лечит раздутые карты)
        if (shrinkDetailResolution)
        {
            int target = Mathf.Max(32, Mathf.Min(safeDetailResolution, Mathf.Min(w, h)));
            // patchSize = 8 — безопасное дефолтное значение
            td.SetDetailResolution(target, 8);
        }

        // Опционально удаляем сами слои травы
        if (removeDetailPrototypes)
        {
            td.detailPrototypes = System.Array.Empty<DetailPrototype>();
        }

        Debug.Log($"[Terrain] Details очищены на '{_terrain.name}'. " +
                  $"Layers: {td.detailPrototypes.Length}, Res: {td.detailWidth}x{td.detailHeight}", this);
    }

    // Удобная кнопка для рантайма (можно вызывать из кода)
    public void ClearNowRuntime() => ClearDetails();
}
