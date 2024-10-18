using System;

public class CustomEvents
{
    public static event Action OnBaseDestroy;
    public static void FireBaseDestroy()
    {
        OnBaseDestroy?.Invoke();
    }
    public static event Action OnSpawnRoadComplete;
    public static void FireSpawnRoadComplete()
    {
        OnSpawnRoadComplete?.Invoke();
    }

    public static event Action<GameEventType> OnGameEventStart;
    public static void FireGameEventStart(GameEventType gameEventType)
    {
        OnGameEventStart?.Invoke(gameEventType);
    }

    public static event Action<int> OnBuildingDestroyedNow;
    public static void FireBuildingDestroyedNow(int tileId)
    {
        OnBuildingDestroyedNow?.Invoke(tileId);
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

    public static event Action<TileObject, Resource, float, ResourceRecept[]> OnChangeResourceRequired;
    public static void FireChangeResourceRequired(TileObject tileObject, Resource resource, float amount, ResourceRecept[] resourceRecepts)
    {
        OnChangeResourceRequired?.Invoke(tileObject, resource, amount, resourceRecepts);
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

    public static event Action<int> OnDayEnd;
    public static void FireDayEnd(int day)
    {
        OnDayEnd?.Invoke(day);
    }

    public static event Action OnSetBase;
    public static void FireSetBase()
    {
        OnSetBase?.Invoke();
    }
}
