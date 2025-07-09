using UnityEngine;

public class AcidRainMissionEvent : BaseMissionEvent
{
    [SerializeField] private SpawnedHazardSystem _spawnedHazardSystem;
    [SerializeField] private GameObject _spawnPrefab;
    private GameObject _currentPrefab;
    public override void StartEvent()
    {
        base.StartEvent();
        _currentPrefab = Instantiate(_spawnPrefab, GetTileObject().transform.position, Quaternion.identity);
        _currentPrefab.GetComponent<OnTriggerStayDealDamage>().SetInfo(WorldGameInfo.AcidRainDuration, WorldGameInfo.AcidRainTriggetStayDamageFactor);
        _spawnedHazardSystem.RegisterHazard((int)HazardEnum.AcidRain, _currentPrefab, WorldGameInfo.AcidRainDuration, WorldGameInfo.AcidRainTriggetStayDamageFactor);
    }
}
