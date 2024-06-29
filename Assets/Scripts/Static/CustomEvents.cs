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

    public static event Action<ResourceEnum, int, int> OnChangeResourceExtraction;
    public static void FireChangeResourceExtraction(ResourceEnum resourceEnum, int amount, int tileId)
    {
        OnChangeResourceExtraction?.Invoke(resourceEnum, amount, tileId);
    }


}
