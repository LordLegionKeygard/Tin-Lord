using System;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyDebuff : MonoBehaviour
{
    [Header("Slow")]
    [SerializeField] private SlowType[] _resistTypes;
    [SerializeField] private bool _isHaveEmission;
    [ColorUsage(true, true)][SerializeField] private Color _emissionColor;
    [SerializeField] private SkinnedMeshRenderer[] _skinnedMeshRenderers;
    [SerializeField] private MeshRenderer[] _meshRenderers;
    private float _radioWaveSlowAmount;
    private float _riverSlowAmount;
    private float _speedFactor = 1;
    private EnemySpeed _enemySpeed;
    private Texture[] _cachedSkinnedMeshRendEmissionTextures;
    private Texture[] _cachedMeshRendEmissionTextures;
    public float GetSpeedFactor() => _speedFactor;


    private void Awake()
    {
        _enemySpeed = GetComponent<EnemySpeed>();
    }

    private void Start()
    {
        CachedSkinnedMeshRendEmissionTextures();
        CachedMeshRendEmissionTextures();
    }

    private void CachedSkinnedMeshRendEmissionTextures()
    {
        if (_isHaveEmission)
        {
            _cachedSkinnedMeshRendEmissionTextures = new Texture[_skinnedMeshRenderers.Length];

            for (int i = 0; i < _skinnedMeshRenderers.Length; i++)
            {
                _cachedSkinnedMeshRendEmissionTextures[i] = _skinnedMeshRenderers[i].material.GetTexture("_EmissionMap");
            }
        }
    }

    private void CachedMeshRendEmissionTextures()
    {
        if (_isHaveEmission)
        {
            _cachedMeshRendEmissionTextures = new Texture[_meshRenderers.Length];

            for (int i = 0; i < _meshRenderers.Length; i++)
            {
                _cachedMeshRendEmissionTextures[i] = _meshRenderers[i].material.GetTexture("_EmissionMap");
            }
        }
    }

    public void ChangeSlowDebuff(float slowAmount, SlowType slowType)
    {
        if (Array.Exists(_resistTypes, t => t == slowType)) return;

        switch (slowType)
        {
            case SlowType.RadioWave:
                _radioWaveSlowAmount = slowAmount;
                break;
            case SlowType.River:
                _riverSlowAmount = slowAmount;
                break;
        }

        _speedFactor = Mathf.Clamp(_speedFactor + _radioWaveSlowAmount + _riverSlowAmount, 0.1f, 1.0f);
        if (_speedFactor < 0.2f) _speedFactor = 0.2f;
        UpdateSlowViewEmission();
        _enemySpeed.CanRun();
    }

    private void UpdateSlowViewEmission()
    {
        if (_speedFactor == 1.0f)
        {
            ActiveSkinnedMeshrendSlowView();
            ActiveMeshrendSlowView();
        }
        else
        {
            UnactiveSkinnedMeshrendSlowView();
            UnactiveMeshrendSlowView();
        }
    }

    private void ActiveSkinnedMeshrendSlowView()
    {
        for (int i = 0; i < _skinnedMeshRenderers.Length; i++)
        {
            if (_isHaveEmission)
            {
                _skinnedMeshRenderers[i].material.EnableKeyword("_EMISSION");
                _skinnedMeshRenderers[i].material.SetColor("_EmissionColor", _emissionColor);

                if (_isHaveEmission && _cachedSkinnedMeshRendEmissionTextures != null && _cachedSkinnedMeshRendEmissionTextures.Length > i)
                {
                    _skinnedMeshRenderers[i].material.SetTexture("_EmissionMap", _cachedSkinnedMeshRendEmissionTextures[i]);
                }
            }
            else
            {
                _skinnedMeshRenderers[i].material.DisableKeyword("_EMISSION");
            }
        }
    }

    private void UnactiveSkinnedMeshrendSlowView()
    {
        for (int i = 0; i < _skinnedMeshRenderers.Length; i++)
        {
            _skinnedMeshRenderers[i].material.EnableKeyword("_EMISSION");
            _skinnedMeshRenderers[i].material.SetColor("_EmissionColor", Colors.SlowEmission);
            _skinnedMeshRenderers[i].material.SetTexture("_EmissionMap", null);
        }
    }

    private void ActiveMeshrendSlowView()
    {
        for (int i = 0; i < _meshRenderers.Length; i++)
        {
            if (_isHaveEmission)
            {
                _meshRenderers[i].material.EnableKeyword("_EMISSION");
                _meshRenderers[i].material.SetColor("_EmissionColor", _emissionColor);

                if (_isHaveEmission && _cachedMeshRendEmissionTextures != null && _cachedMeshRendEmissionTextures.Length > i)
                {
                    _meshRenderers[i].material.SetTexture("_EmissionMap", _cachedMeshRendEmissionTextures[i]);
                }
            }
            else
            {
                _meshRenderers[i].material.DisableKeyword("_EMISSION");
            }
        }
    }

    private void UnactiveMeshrendSlowView()
    {
        for (int i = 0; i < _meshRenderers.Length; i++)
        {
            _meshRenderers[i].material.EnableKeyword("_EMISSION");
            _meshRenderers[i].material.SetColor("_EmissionColor", Colors.SlowEmission);
            _meshRenderers[i].material.SetTexture("_EmissionMap", null);
        }
    }
}

public enum SlowType
{
    RadioWave = 0,
    River = 1,
}
