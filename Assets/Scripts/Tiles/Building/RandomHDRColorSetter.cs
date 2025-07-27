using UnityEngine;

public class RandomHDRColorSetter : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [ColorUsage(true, true)] [SerializeField] private Color[] _сolors;

    private void Start()
    {
        Material material = _meshRenderer.material;
        Color randomColor = _сolors[Random.Range(0, _сolors.Length)];
        material.SetColor("_Color", randomColor);
    }
}
