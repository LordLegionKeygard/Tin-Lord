using UnityEngine;

public class EnemyDebuff : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer[] _meshRenderers;
    [SerializeField] private bool _isHaveEmission;
    [ColorUsage(true, true)][SerializeField] private Color _emissionColor;
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
        CachedEmissionTextures();
    }

    private void CachedEmissionTextures()
    {
        if (_isHaveEmission)
        {
            // Инициализируем массив для хранения ссылок на оригинальные карты эмиссии.
            _cachedEmissionTextures = new Texture[_meshRenderers.Length];

            for (int i = 0; i < _meshRenderers.Length; i++)
            {
                // Берём texture из "_EmissionMap" и сохраняем
                _cachedEmissionTextures[i] = _meshRenderers[i].material.GetTexture("_EmissionMap");
            }
        }
    }

    public void ChangeSlow(float slowAmount)
    {
        _speedFactor = Mathf.Clamp(_speedFactor + slowAmount, 0.1f, 1.0f); // Минимум 10% от базовой скорости
        UpdateSlowViewEmission();
        _enemySpeed.CanRun();
    }

    private void UpdateSlowViewEmission()
    {
        // Если объект не замедлен (speedFactor == 1), возвращаемся к обычному виду
        if (_speedFactor == 1.0f)
        {
            for (int i = 0; i < _meshRenderers.Length; i++)
            {
                if (_isHaveEmission)
                {
                    // Восстанавливаем обычный цвет эмиссии
                    _meshRenderers[i].material.EnableKeyword("_EMISSION");
                    _meshRenderers[i].material.SetColor("_EmissionColor", _emissionColor);

                    // Если у нас есть исходная эмиссия, то возвращаем её текстуру
                    if (_isHaveEmission && _cachedEmissionTextures != null && _cachedEmissionTextures.Length > i)
                    {
                        _meshRenderers[i].material.SetTexture("_EmissionMap", _cachedEmissionTextures[i]);
                    }
                }
                else
                {
                    // Если эмиссии изначально не было, полностью выключаем
                    _meshRenderers[i].material.DisableKeyword("_EMISSION");
                }
            }
        }
        // Иначе (когда объект замедлён) включаем замедление
        else
        {
            for (int i = 0; i < _meshRenderers.Length; i++)
            {
                // В любом случае включаем эмиссию
                _meshRenderers[i].material.EnableKeyword("_EMISSION");

                // Ставим цвет для slow-режима
                _meshRenderers[i].material.SetColor("_EmissionColor", Colors.SlowEmission);

                // Ставим null в _EmissionMap
                _meshRenderers[i].material.SetTexture("_EmissionMap", null);
            }
        }
    }
}
