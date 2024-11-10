using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidRainDayEvent : BaseDayEvent
{
    public override void StartEvent()
    {
        base.StartEvent();
       
        CurrentPrefab().GetComponent<AcidRainDealDamage>().SetTile(TileObject());
    }
}
