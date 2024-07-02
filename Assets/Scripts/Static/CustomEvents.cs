using System;

public class CustomEvents
{
    public static event Action OnPrepareRoads;
    public static void FirePrepareRoads()
    {
        OnPrepareRoads?.Invoke();
    }

    public static event Action OnTheDayIsOver;
    public static void FireTheDayIsOver()
    {
        OnTheDayIsOver?.Invoke();
    }

    public static event Action<ResourceEnum, float, int> OnChangeResourceExtraction;
    public static void FireChangeResourceExtraction(ResourceEnum resourceEnum, float amount, int tileId)
    {
        OnChangeResourceExtraction?.Invoke(resourceEnum, amount, tileId);
    }

    public static event Action<bool> OnPauseChanged;
    public static void FirePauseChanged(bool isPause)
    {
        OnPauseChanged?.Invoke(isPause);
    }

    public static event Action<int> OnRefreshAnyTileInfo;
    public static void FireRefreshAnyTileInfo(int tileId)
    {
        OnRefreshAnyTileInfo?.Invoke(tileId);
    }
}
