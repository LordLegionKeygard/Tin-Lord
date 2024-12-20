using UnityEngine;

public class ConstructionBuildingView : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;

    private Material _material;
    private static readonly int ShaderDisplacement = Shader.PropertyToID("_ShaderDisplacement");

    private void Awake()
    {
        InitializeMaterial();
    }

    private void InitializeMaterial()
    {
        if (_meshRenderer == null)
        {
            Debug.LogError("MeshRenderer не назначен в " + gameObject.name);
            return;
        }

        _material = new Material(_meshRenderer.material);
        _meshRenderer.material = _material;

        if (_material == null)
        {
            Debug.LogError("Материал не найден у " + gameObject.name);
        }
    }

    public void UpdateShaderByHealth(float currentHealth, float maxHealth)
    {
        if (_material == null)
        {
            Debug.LogError("Материал отсутствует. Невозможно обновить значения шейдера.");
            return;
        }

        float progress = Mathf.Clamp01(currentHealth / maxHealth);
        _material.SetFloat(ShaderDisplacement, progress);
    }
}
