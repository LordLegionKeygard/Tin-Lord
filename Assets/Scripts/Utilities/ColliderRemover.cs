using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Компонент, который по кнопке удаляет все коллайдеры у самого объекта и его детей.
/// </summary>
[ExecuteAlways]           // работает и в Play Mode, и прямо в редакторе
public class ColliderRemover : MonoBehaviour
{
#if UNITY_EDITOR
    /// <summary>
    /// Собственно логика удаления.
    /// </summary>
    public void RemoveAllColliders()
    {
        // true — включаем поиск и в неактивных (hideFlags/disabled) объектах
        Collider[] colliders = GetComponentsInChildren<Collider>(true);
        int removedCount = 0;

        foreach (Collider col in colliders)
        {
            // Записываем в Undo, чтобы можно было откатить Ctrl + Z
            Undo.RecordObject(col, "Remove Collider");
            // DestroyImmediate с флагом allowDestroyingAssets = true,
            // чтобы удалять коллайдер прямо из префаба при необходимости
            Object.DestroyImmediate(col, true);
            removedCount++;
        }

        Debug.Log($"ColliderRemover: удалено {removedCount} коллайдер(ов) с объекта “{name}”.");
    }
#endif
}
