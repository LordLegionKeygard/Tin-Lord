using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RadioWaveTrigger : MonoBehaviour
{
    [SerializeField] private float slowEffectMultiplier;
    [SerializeField] private SphereCollider _effectCollider;
    [SerializeField] private ParticleSystem _vfx;
    [SerializeField] private BuildingLevels _buildingLevels;
    private HashSet<EnemySpeed> _enemiesInRange = new HashSet<EnemySpeed>();

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
        var enemySpeed = other.GetComponent<EnemySpeed>();
        if (enemySpeed != null)
        {
            enemySpeed.ChangeSlow(-slowEffectMultiplier);
            _enemiesInRange.Add(enemySpeed);
            UpdateVFX();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var enemySpeed = other.GetComponent<EnemySpeed>();
        if (enemySpeed != null)
        {
            enemySpeed.ChangeSlow(+slowEffectMultiplier);
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
        if (_buildingLevels.CurrentTileObject().GetId() != id) return;

        foreach (var enemy in _enemiesInRange)
        {
            if (enemy != null)
            {
                enemy.ChangeSlow(+slowEffectMultiplier); // Снимаем эффект
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
