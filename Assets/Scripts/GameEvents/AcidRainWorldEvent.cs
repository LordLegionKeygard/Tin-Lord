
public class AcidRainWorldEvent : BaseWorldEvent
{
    public override void StartEvent()
    {
        base.StartEvent();
       
        GetCurrentPrefab().GetComponent<AcidRainDealDamage>().SetTile(GetTileObject());
    }
}
