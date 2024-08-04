using System;

public class CustomEvents
{
    public static event Action OnPrepareRoads;
    public static void FirePrepareRoads()
    {
        OnPrepareRoads?.Invoke();
    }

    public static event Action OnTimeTick;
    public static void FireTimeTick()
    {
        OnTimeTick?.Invoke();
    }

    public static event Action<ResourceEnum, float, int> OnChangeResourceExtraction;
    public static void FireChangeResourceExtraction(ResourceEnum resourceEnum, float amount, int tileId)
    {
        OnChangeResourceExtraction?.Invoke(resourceEnum, amount, tileId);
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

    public static event Action<int, bool> OnHaveRequiredResource;
    public static void FireHaveRequiredResource(int id, bool state)
    {
        OnHaveRequiredResource?.Invoke(id, state);
    }
}
