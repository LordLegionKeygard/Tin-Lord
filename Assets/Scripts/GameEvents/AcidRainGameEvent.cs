
public class AcidRainGameEvent : BaseGameEvent
{
    public override void StartEvent()
    {
        base.StartEvent();
       
        CurrentPrefab().GetComponent<AcidRainDealDamage>().SetTile(TileObject());
    }
}
