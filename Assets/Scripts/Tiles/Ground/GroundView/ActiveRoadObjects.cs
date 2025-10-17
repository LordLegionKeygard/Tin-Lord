
public class ActiveRoadObjects : ActiveObjects
{
    public override void Refresh()
    {
        if (CurrentMissionInfo.Instance.GetCurrentLandscape().MissionView.RockTexture == null) return;
        base.Refresh();
    }
}
