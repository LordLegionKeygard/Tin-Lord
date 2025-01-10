
public class AcidRainGameEvent : BaseGameEvent
{
    public override void StartEvent()
    {
        base.StartEvent();
       
        GetCurrentPrefab().GetComponent<AcidRainDealDamage>().SetTile(GetTileObject());
    }
}
