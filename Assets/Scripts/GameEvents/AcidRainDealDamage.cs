using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidRainDealDamage : MonoBehaviour
{
    [SerializeField] private TileObject _mainTileObject;
    [SerializeField] private List<BuildingHealth> _allBuildingHealths;
    [SerializeField] private ParticleSystem[] _ps;
    private int _repeatCount = 30;
    private float _waitSeconds = 2;
    private float _damage = 1f;

    public void SetTile(TileObject tileObject)
    {
        _mainTileObject = tileObject;

        _allBuildingHealths.AddRange(_mainTileObject.GetNeighbourBulidingHealthArray());
        _allBuildingHealths.Add(_mainTileObject.BuildingHealth());

        StartCoroutine(nameof(DealDamageCoroutine));
    }

    public IEnumerator DealDamageCoroutine()
    {
        yield return new WaitForSeconds(_waitSeconds);

        _repeatCount--;

        foreach (var buildingHealth in _allBuildingHealths)
        {
            buildingHealth.CalculateDamage(_damage);
        }

        if (_repeatCount == 0)
        {
            var main0 = _ps[0].main;
            main0.maxParticles = 0;

            var main1 = _ps[1].main;
            main1.maxParticles = 0;

            Destroy(gameObject, 3f);
        }
        else
        {
            StartCoroutine(nameof(DealDamageCoroutine));
        }
    }
}
