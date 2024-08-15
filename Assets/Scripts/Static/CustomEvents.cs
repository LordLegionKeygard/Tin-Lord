using System;

public class CustomEvents
{
    public static event Action OnPrepareRoads;
    public static void FirePrepareRoads()
    {
        OnPrepareRoads?.Invoke();
    }

    public static event Action OnTimeTickAfterResourcesChanged;
    public static void FireTickAfterResourcesChanged()
    {
        OnTimeTickAfterResourcesChanged?.Invoke();
    }

    public static event Action<ResourceEnum, float, int, bool> OnChangeResourceProduction;
    public static void FireChangeResourceProduction(ResourceEnum resourceEnum, float amount, int tileId, bool remove)
    {
        OnChangeResourceProduction?.Invoke(resourceEnum, amount, tileId, remove);
    }

    public static event Action<TileObject, Resource, float> OnChangeResourceRequired;
    public static void FireChangeResourceRequired(TileObject tileObject, Resource resource, float amount)
    {
        OnChangeResourceRequired?.Invoke(tileObject, resource, amount);
    }

    public static event Action<int, int, bool> OnChangeEcology;
    public static void FireChangeEcology(int amount, int tileId, bool remove)
    {
        OnChangeEcology?.Invoke(amount, tileId, remove);
    }

    public static event Action<bool> OnPauseChanged;
    public static void FirePauseChanged(bool isPause)
    {
        OnPauseChanged?.Invoke(isPause);
    }
}
