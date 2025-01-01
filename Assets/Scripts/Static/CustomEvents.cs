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

    public static event Action<int> OnBuildingDestroyed;
    public static void FireBuildingDestroyed(int tileId)
    {
        OnBuildingDestroyed?.Invoke(tileId);
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

    public static event Action OnRepairRobot;
    public static void FireRepairRobot()
    {
        OnRepairRobot?.Invoke();
    }

    public static event Action OnRobotDie;
    public static void FireRobotDie()
    {
        OnRobotDie?.Invoke();
    }

    public static event Action OnRobotTakeDamage;
    public static void FireRobotTakeDamage()
    {
        OnRobotTakeDamage?.Invoke();
    }

    public static Action<int> OnChangeExperience;
    public static void FireChangeExperience(int value)
    {
        OnChangeExperience?.Invoke(value);
    }

    public static Action<int> OnBuildingTakeDamage;
    public static void FireBuildingTakeDamage(int id)
    {
        OnBuildingTakeDamage?.Invoke(id);
    }

    public static Action<int> OnRobotFullRepairBuilding;
    public static void FireRobotFullRepairBuilding(int id)
    {
        OnRobotFullRepairBuilding?.Invoke(id);
    }

    public static Action OnDataLoad;
    public static void FireDataLoad()
    {
        OnDataLoad?.Invoke();
    }

    public static Action<SceneEnum, float, bool> OnLoadScene;
    public static void FireLoadScene(SceneEnum sceneEnum, float timeInSec, bool isLoadData)
    {
        OnLoadScene?.Invoke(sceneEnum, timeInSec, isLoadData);
    }

    public static Action<FadeType> OnFade;
    public static void FireFade(FadeType fadeType)
    {
        OnFade?.Invoke(fadeType);
    }

    public static Action<bool> OnLoadingScreenToggle;
    public static void FireLoadingScreenToggle(bool state)
    {
        OnLoadingScreenToggle?.Invoke(state);
    }

    public static Action<bool, int> OnTooltipToggle;
    public static void FireTooltipToggle(bool state, int toolTipNumer)
    {
        OnTooltipToggle?.Invoke(state, toolTipNumer);
    }

    public static Action OnCloseTooltips;
    public static void FireCloseTooltips()
    {
        OnCloseTooltips?.Invoke();
    }

    public static Action<float, float, string> OnUpdateToolTip;
    public static void FireUpdateToolTipTransform(float x, float y, string text)
    {
        OnUpdateToolTip.Invoke(x, y, text);
    }
}
