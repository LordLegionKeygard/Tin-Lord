
public class AcidRainMissionEvent : BaseMissionEvent
{
    public override void StartEvent()
    {
        base.StartEvent();
       
        GetCurrentPrefab().GetComponent<AcidRainDealDamage>().SetTile(GetTileObject());
    }
}
