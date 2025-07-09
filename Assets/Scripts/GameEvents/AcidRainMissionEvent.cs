using UnityEngine;

public class AcidRainMissionEvent : BaseMissionEvent
{
    [SerializeField] private GameObject _spawnPrefab;
    private GameObject _currentPrefab;
    public override void StartEvent()
    {
        base.StartEvent();
        _currentPrefab = Instantiate(_spawnPrefab, GetTileObject().transform.position, Quaternion.identity);
        _currentPrefab.GetComponent<OnTriggerStayDealDamage>().SetInfo(WorldGameInfo.AcidRainDuration, WorldGameInfo.AcidRainTriggetStayDamageFactor);
    }
}
