#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ColliderRemover))]
public class ColliderRemoverEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Рисуем стандартные поля (на случай, если вы захотите что-то добавить)
        DrawDefaultInspector();
        EditorGUILayout.Space();

        // Большая кнопка
        if (GUILayout.Button("Remove All Colliders", GUILayout.Height(40)))
        {
            // Вызов метода компонента
            ColliderRemover remover = (ColliderRemover)target;
            remover.RemoveAllColliders();
        }
    }
}
#endif
