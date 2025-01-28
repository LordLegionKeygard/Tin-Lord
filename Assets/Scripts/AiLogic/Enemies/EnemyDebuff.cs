using UnityEngine;

public class EnemyDebuff : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer[] _meshRenderers;
    [SerializeField] private bool _isHaveEmission;
    [ColorUsage(true, true)] [SerializeField] private Color _emissionColor;
    private float _speedFactor = 1;
    private EnemySpeed _enemySpeed;
    public float GetSpeedFactor() => _speedFactor;

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
            for (int i = 0; i < _meshRenderers.Length; i++)
            {
                if(_isHaveEmission)
                {
                    _meshRenderers[i].material.SetColor("_EmissionColor", _emissionColor);
                }
                else _meshRenderers[i].material.DisableKeyword("_EMISSION");
            }
        }
        else
        {
            for (int i = 0; i < _meshRenderers.Length; i++)
            {
                _meshRenderers[i].material.EnableKeyword("_EMISSION");
                _meshRenderers[i].material.SetColor("_EmissionColor", Colors.SlowEmission);
            }
        }
    }
}
