using UnityEngine;

public class EnemyDebuff : MonoBehaviour
{
    [Header("Slow")]
    [SerializeField] private bool _resistSlow;
    [SerializeField] private bool _isHaveEmission;
    [ColorUsage(true, true)][SerializeField] private Color _emissionColor;
    [SerializeField] private SkinnedMeshRenderer[] _meshRenderers;
    private float _speedFactor = 1;
    private EnemySpeed _enemySpeed;
    private Texture[] _cachedEmissionTextures;
    public float GetSpeedFactor() => _speedFactor;


    private void Awake()
    {
        _enemySpeed = GetComponent<EnemySpeed>();
    }

    private void Start()
    {
        if (!_resistSlow) CachedEmissionTextures();
    }

    private void CachedEmissionTextures()
    {
        if (_isHaveEmission)
        {
            _cachedEmissionTextures = new Texture[_meshRenderers.Length];

            for (int i = 0; i < _meshRenderers.Length; i++)
            {
                _cachedEmissionTextures[i] = _meshRenderers[i].material.GetTexture("_EmissionMap");
            }
        }
    }

    public void ChangeSlow(float slowAmount)
    {
        if (_resistSlow) return;
        _speedFactor = Mathf.Clamp(_speedFactor + slowAmount, 0.1f, 1.0f);
        UpdateSlowViewEmission();
        _enemySpeed.CanRun();
    }

    private void UpdateSlowViewEmission()
    {
        if (_speedFactor == 1.0f)
        {
            for (int i = 0; i < _meshRenderers.Length; i++)
            {
                if (_isHaveEmission)
                {
                    _meshRenderers[i].material.EnableKeyword("_EMISSION");
                    _meshRenderers[i].material.SetColor("_EmissionColor", _emissionColor);

                    if (_isHaveEmission && _cachedEmissionTextures != null && _cachedEmissionTextures.Length > i)
                    {
                        _meshRenderers[i].material.SetTexture("_EmissionMap", _cachedEmissionTextures[i]);
                    }
                }
                else
                {
                    _meshRenderers[i].material.DisableKeyword("_EMISSION");
                }
            }
        }
        else
        {
            for (int i = 0; i < _meshRenderers.Length; i++)
            {
                _meshRenderers[i].material.EnableKeyword("_EMISSION");
                _meshRenderers[i].material.SetColor("_EmissionColor", Colors.SlowEmission);
                _meshRenderers[i].material.SetTexture("_EmissionMap", null);
            }
        }
    }
}
