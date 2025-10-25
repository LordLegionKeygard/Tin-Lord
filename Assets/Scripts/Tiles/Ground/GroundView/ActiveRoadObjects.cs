
public class ActiveRoadObjects : ActiveObjects
{
    public override void Refresh()
    {
        var landscape = CurrentMissionInfo.Instance.GetCurrentLandscape();
        if (landscape == null) return;
        if (landscape.MissionView.RockTexture == null) return;
        base.Refresh();
    }
}
