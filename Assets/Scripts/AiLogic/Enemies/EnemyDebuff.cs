using UnityEngine;

public class EnemyDebuff : MonoBehaviour
{
    [Header("Slow")]
    [SerializeField] private SkinnedMeshRenderer meshRenderer;
    private float _speedFactor = 1;
    public float GetSpeedFactor() => _speedFactor;
    private EnemySpeed _enemySpeed;

    private void Awake()
    {
        _enemySpeed = GetComponent<EnemySpeed>();
    }

    public void ChangeSlow(float slowAmount)
    {
        _speedFactor = Mathf.Clamp(_speedFactor + slowAmount, 0.1f, 1.0f); // Минимум 10% от базовой скорости
        UpdateSlowViewEmission();
        _enemySpeed.CanRun();
    }

    private void UpdateSlowViewEmission()
    {
        if (_speedFactor == 1.0f)
        {
            meshRenderer.material.DisableKeyword("_EMISSION");
        }
        else
        {
            meshRenderer.material.EnableKeyword("_EMISSION");
            meshRenderer.material.SetColor("_EmissionColor", Colors.SlowEmission);
        }
    }
}
