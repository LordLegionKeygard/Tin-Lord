using System.Collections.Generic;
using UnityEngine;

public class RadioWaveTrigger : MonoBehaviour
{
    [SerializeField] private float slowEffectMultiplier;
    [SerializeField] private SphereCollider _effectCollider;
    [SerializeField] private ParticleSystem _vfx;
    [SerializeField] private BuildingLevels _buildingLevels;
    private HashSet<EnemyDebuff> _enemiesInRange = new HashSet<EnemyDebuff>();

    private void Start()
    {
        CustomEvents.OnBuildingDestroyed += ClearAllEffects;
    }

    private void OnEnable()
    {
        _enemiesInRange.Clear();
        _vfx.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemySpeed = other.GetComponent<EnemyDebuff>();
        if (enemySpeed != null)
        {
            enemySpeed.ChangeSlowDebuff(-slowEffectMultiplier, SlowType.RadioWave);
            _enemiesInRange.Add(enemySpeed);
            UpdateVFX();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var enemySpeed = other.GetComponent<EnemyDebuff>();
        if (enemySpeed != null)
        {
            enemySpeed.ChangeSlowDebuff(+slowEffectMultiplier, SlowType.RadioWave);
            _enemiesInRange.Remove(enemySpeed);
            UpdateVFX();
        }
    }

    private void UpdateVFX()
    {
        if (_enemiesInRange.Count > 0)
        {
            if (!_vfx.isPlaying)
                _vfx.Play();
        }
        else
        {
            if (_vfx.isPlaying)
                _vfx.Stop();
        }
    }

    private void ClearAllEffects(int id)
    {
        _effectCollider.enabled = false;
        if (_buildingLevels.CurrentTileObject().GetId() != id) return;

        foreach (var enemy in _enemiesInRange)
        {
            if (enemy != null)
            {
                enemy.ChangeSlowDebuff(+slowEffectMultiplier, SlowType.RadioWave);
            }
        }
        _enemiesInRange.Clear();
        _vfx.Stop();
    }

    private void OnDestroy()
    {
        CustomEvents.OnBuildingDestroyed -= ClearAllEffects;
    }
}
