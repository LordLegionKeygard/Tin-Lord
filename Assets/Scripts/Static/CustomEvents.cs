using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomEvents
{
    public static event Action OnTutorialSelectCard;
    public static void FireTutorialSelectCard()
    {
        OnTutorialSelectCard?.Invoke();
    }

    public static event Action<TutorialStepEnum> OnForceRunStep;
    public static void FireForceRunStep(TutorialStepEnum tutorialStepEnum)
    {
        OnForceRunStep?.Invoke(tutorialStepEnum);
    }
    public static event Action<TutorialStepEnum> OnRunStepAfterWait;
    public static void FireRunStepAfterWait(TutorialStepEnum tutorialStepEnum)
    {
        OnRunStepAfterWait?.Invoke(tutorialStepEnum);
    }

    public static event Action<TutorialStepEnum> OnStartTutorialStep;
    public static void FireStartTutorialStep(TutorialStepEnum tutorialStepEnum)
    {
        OnStartTutorialStep?.Invoke(tutorialStepEnum);
    }

    public static event Action<TutorialStepEnum> OnCompleteTutorialStep;
    public static void FireCompleteTutorialStep(TutorialStepEnum tutorialStepEnum)
    {
        OnCompleteTutorialStep?.Invoke(tutorialStepEnum);
    }

    public static event Action<TileObject> OnChangeGeneralRepairTileObject;
    public static void FireChangeGeneralRepairTileObject(TileObject tileObject)
    {
        OnChangeGeneralRepairTileObject?.Invoke(tileObject);
    }
    public static Action<string, InputActionReference> OnUpdateBindingText;
    public static void FireUpdateBindingText(string text, InputActionReference inputActionReference)
    {
        OnUpdateBindingText?.Invoke(text, inputActionReference);
    }

    public static event Action<SkillInfo> OnUseSkill;
    public static void FireUseSkill(SkillInfo skill)
    {
        OnUseSkill?.Invoke(skill);
    }

    public static event Action<SkillInfo> OnEndSkill;
    public static void FireEndSkill(SkillInfo skill)
    {
        OnEndSkill?.Invoke(skill);
    }

    public static event Action OnSpawnRoadComplete;
    public static void FireSpawnRoadComplete()
    {
        OnSpawnRoadComplete?.Invoke();
    }

    public static event Action OnCompleteLoadTiles;
    public static void FireCompleteLoadTiles()
    {
        OnCompleteLoadTiles?.Invoke();
    }

    public static event Action<GameEventType, int> OnGameEventStart;
    public static void FireGameEventStart(GameEventType gameEventType, int eventNumber)
    {
        OnGameEventStart?.Invoke(gameEventType, eventNumber);
    }

    public static event Action<int> OnBuildingDestroyed;
    public static void FireBuildingDestroyed(int tileId)
    {
        OnBuildingDestroyed?.Invoke(tileId);
    }

    public static event Action OnTimeTick;
    public static void FireTimeTick()
    {
        OnTimeTick?.Invoke();
    }

    public static event Action<ResourceEnum, float, int, bool> OnChangeResourceProduction;
    public static void FireChangeResourceProduction(ResourceEnum resourceEnum, float amount, int tileId, bool remove)
    {
        OnChangeResourceProduction?.Invoke(resourceEnum, amount, tileId, remove);
    }

    public static event Action<TileObject, Resource, float, ResourceRecept[]> OnChangeResourceForWork;
    public static void FireChangeResourceForWork(TileObject tileObject, Resource resource, float amount, ResourceRecept[] resourceRecepts)
    {
        OnChangeResourceForWork?.Invoke(tileObject, resource, amount, resourceRecepts);
    }

    public static event Action<float, int, bool> OnChangeEcology;
    public static void FireChangeEcology(float amount, int tileId, bool remove)
    {
        OnChangeEcology?.Invoke(amount, tileId, remove);
    }

    public static event Action<bool> OnCheckPause;
    public static void FireCheckPause(bool isPause)
    {
        OnCheckPause?.Invoke(isPause);
    }

    public static event Action<int> OnDayEnd;
    public static void FireDayEnd(int day)
    {
        OnDayEnd?.Invoke(day);
    }

    public static event Action<int> OnSetBase;
    public static void FireSetBase(int baseLevel)
    {
        OnSetBase?.Invoke(baseLevel);
    }

    public static event Action OnRepairMachine;
    public static void FireRepairMachine()
    {
        OnRepairMachine?.Invoke();
    }

    public static event Action OnMachineDie;
    public static void FireMachineDie()
    {
        OnMachineDie?.Invoke();
    }

    public static event Action OnMachineTakeDamage;
    public static void FireMachineTakeDamage()
    {
        OnMachineTakeDamage?.Invoke();
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

    public static Action OnDestroyMachineProductionBuilding;
    public static void FireDestroyMachineProductionBuilding()
    {
        OnDestroyMachineProductionBuilding?.Invoke();
    }

    public static Action<SceneEnum, float, Sprite> OnLoadScene;
    public static void FireLoadScene(SceneEnum sceneEnum, float timeInSec, Sprite sprite)
    {
        OnLoadScene?.Invoke(sceneEnum, timeInSec, sprite);
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

    public static Action OnActiveTargetSkill;
    public static void FireActiveTargetSkill()
    {
        OnActiveTargetSkill?.Invoke();
    }

    public static Action OnCancelTargetSkill;
    public static void FireCancelTargetSkill()
    {
        OnCancelTargetSkill?.Invoke();
    }

    public static Action OnUseTargetSkill;
    public static void FireUseTargetSkill()
    {
        OnUseTargetSkill?.Invoke();
    }

    public static Action OnCloseTooltips;
    public static void FireCloseTooltips()
    {
        OnCloseTooltips?.Invoke();
    }

    public static Action<float, float, string, float, float> OnUpdateToolTip;
    public static void FireUpdateToolTipTransform(float x, float y, string text, float xPivot, float yPivot)
    {
        OnUpdateToolTip.Invoke(x, y, text, xPivot, yPivot);
    }

    public static Action<float, float, SkillInfo, bool, float, float> OnUpdateSkillToolTip;
    public static void FireUpdateSkillToolTipTransform(float x, float y, SkillInfo skill, bool resourceEnough, float xPivot, float yPivot)
    {
        OnUpdateSkillToolTip.Invoke(x, y, skill, resourceEnough, xPivot, yPivot);
    }

    public static Action<int> OnEnemyDeath;
    public static void FireEnemyDeath(int enemyNumber)
    {
        OnEnemyDeath?.Invoke(enemyNumber);
    }

    public static Action<ObjectiveEnum, int> OnObjectiveAmountChange;
    public static void FireObjectiveAmountChange(ObjectiveEnum objectiveEnum, int value)
    {
        OnObjectiveAmountChange?.Invoke(objectiveEnum, value);
    }


    public static Action<int> OnUpdateEnemySliderDefence;
    public static void FireUpdateEnemySliderDefence(int value)
    {
        OnUpdateEnemySliderDefence?.Invoke(value);
    }

    public static Action<MissionEndEnum> OnMissionEnd;
    public static void FireMissionEnd(MissionEndEnum missionEndEnum)
    {
        OnMissionEnd?.Invoke(missionEndEnum);
    }

    public static Action<bool, MusicType> OnControlFadeMusic;
    public static void FireControlFadeMusic(bool state, MusicType musicType)
    {
        OnControlFadeMusic?.Invoke(state, musicType);
    }

    public static Action OnLearnBuilding;
    public static void FireLearnBuilding()
    {
        OnLearnBuilding?.Invoke();
    }

    public static Action<bool> OnToggleCheckTags;
    public static void FireToggleCheckTags(bool state)
    {
        OnToggleCheckTags?.Invoke(state);
    }
}
