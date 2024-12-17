using UnityEngine;

public class ConstructionBuildingView : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] _meshRenderers;

    private Material[] _materials;
    private static readonly int ShaderDisplacement = Shader.PropertyToID("_ShaderDisplacement");
    private static readonly int ShaderHologramDisplacement = Shader.PropertyToID("_ShaderHologramDisplacement");
    private static readonly int ShaderDissolve = Shader.PropertyToID("_ShaderDissolve");

    private void Awake()
    {
        InitializeMaterials();
    }

    private void InitializeMaterials()
    {
        if (_meshRenderers == null || _meshRenderers.Length == 0)
        {
            Debug.LogError("MeshRenderers array is null or empty on " + gameObject.name);
            return;
        }

        int totalMaterials = 0;
        foreach (var meshRenderer in _meshRenderers)
        {
            if (meshRenderer == null)
            {
                Debug.LogError("Null MeshRenderer detected in " + gameObject.name);
                continue;
            }
            totalMaterials += meshRenderer.materials.Length;
        }

        _materials = new Material[totalMaterials];
        int index = 0;

        foreach (var meshRenderer in _meshRenderers)
        {
            if (meshRenderer == null) continue;

            foreach (var material in meshRenderer.materials)
            {
                if (material == null)
                {
                    Debug.LogError("Material is null in " + meshRenderer.gameObject.name);
                    continue;
                }
                _materials[index] = material;
                index++;
            }
        }

        if (_materials.Length == 0)
        {
            Debug.LogError("No materials found in MeshRenderers for " + gameObject.name);
        }
    }

    public void UpdateShaderByHealth(float currentHealth, float maxHealth)
    {
        if (_materials == null || _materials.Length == 0)
        {
            Debug.LogError("Materials array is null or empty. Cannot update shader values.");
            return;
        }

        float progress = Mathf.Clamp01(currentHealth / maxHealth);

        foreach (var material in _materials)
        {
            if (material == null) continue;

            material.SetFloat(ShaderDisplacement, 1 - progress);
            material.SetFloat(ShaderHologramDisplacement, progress);
            material.SetFloat(ShaderDissolve, progress);
        }
    }
}

