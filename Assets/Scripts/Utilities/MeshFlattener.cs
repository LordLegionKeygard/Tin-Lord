using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[AddComponentMenu("Tools/Mesh Flattener")]
[DisallowMultipleComponent]
public class MeshFlattener : MonoBehaviour
{
#if UNITY_EDITOR
    [ContextMenu("Flatten Mesh Renderers")]
    public void Flatten()
    {
        Undo.RegisterCompleteObjectUndo(gameObject, "Flatten Mesh Renderers");

        var stack          = new Stack<Transform>();
        var meshHolders    = new List<Transform>();
        var potentialEmpty = new List<Transform>();

        foreach (Transform child in transform)
            stack.Push(child);

        while (stack.Count > 0)
        {
            var t = stack.Pop();

            if (t.GetComponent<MeshRenderer>())
                meshHolders.Add(t);
            else
                potentialEmpty.Add(t);
            foreach (Transform c in t)
                stack.Push(c);
        }

        foreach (Transform t in meshHolders)
        {
            if (t.parent != transform)
            {
                Undo.SetTransformParent(t, transform, "Flatten Mesh Renderers");
                t.SetParent(transform, true);
            }
        }

        for (int i = potentialEmpty.Count - 1; i >= 0; --i)
        {
            var t = potentialEmpty[i];
            if (t == null) continue; 
            bool hasOnlyTransform = t.GetComponents<Component>().Length == 1;
            if (hasOnlyTransform && t.childCount == 0)
            {
                Undo.DestroyObjectImmediate(t.gameObject);
            }
        }
    }
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(MeshFlattener))]
public class MeshFlattenerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Flatten Children"))
        {
            ((MeshFlattener)target).Flatten();
        }
    }
}
#endif
