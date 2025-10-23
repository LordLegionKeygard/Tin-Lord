using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class TutorialSystem : MonoBehaviour
{
    [Inject] private HangarSaveGame _hangarSaveGame;
    [SerializeField] private GameSpeedSystem _gameSpeedSystem;
    [SerializeField] private AllTileObjects _allTileObjects;
    [SerializeField] private GameObject _tutorialPanel;
    [SerializeField] private TextMeshProUGUI _tutorialPanelText;
    [SerializeField] private GameObject _justContinueButton;
    [SerializeField] private GameObject _justCloseButton;
    [SerializeField] private CanvasGroup _tutorialCanvasGroup;
    [SerializeField] private TutorialArrowWorld _tutorialArrowWorld;
    [SerializeField] private Building[] _tutorialBuildings;
    [SerializeField] private List<TutorialStep> _steps;
    [SerializeField] private TutorialStep _currentStep;
    private int _currentStepIndex = -1;
    private bool _currentStepInProcess;
    public bool IsStartTutorial() => _currentStep.TutorialStepEnum == TutorialStepEnum.SpaceHangarWelcome_0;
    public TutorialStepEnum GetTutorialStepEnum() => IsCompleteAllTutorial() ? TutorialStepEnum.CompleteAllTutorials_72 : _currentStep.TutorialStepEnum;
    private bool _isCompleteAllTutorials;
    public Building GetTutorialBuilding(int number) => _tutorialBuildings[number];
    public bool IsCompleteMissionTutorial() => IsCompleteAllTutorial() || _currentStep.TutorialStepEnum >= TutorialStepEnum.MissionGoodLuckDescription_66;
    public bool IsCompleteAllTutorial() => _isCompleteAllTutorials;
    public bool PanelIsActive() => _tutorialPanel.activeInHierarchy;
    public AllTileObjects GetAllTileObjects() => _allTileObjects;
    public bool IsCurrentInProcess() => _currentStepInProcess;
    public bool CanUseSkill() => IsCompleteMissionTutorial() || GetTutorialStepEnum() >= TutorialStepEnum.MissionOpenSkillsPanel_51;

    private static readonly Dictionary<TutorialTextPanelPos, (Vector2 anchor, Vector2 offset)> PanelLayout =
        new()
        {
            {TutorialTextPanelPos.Center,       (new Vector2(0.5f, 0.5f), Vector2.zero)},
            {TutorialTextPanelPos.Bottom,       (new Vector2(0.5f, 0f),   new Vector2(0,  186.5f))},
            {TutorialTextPanelPos.Top,          (new Vector2(0.5f, 1f),   new Vector2(0, -186.5f))},
            {TutorialTextPanelPos.Left,         (new Vector2(0f,   0.5f), new Vector2( 328.5f, 0))},
            {TutorialTextPanelPos.Right,        (new Vector2(1f,   0.5f), new Vector2(-328.5f, 0))},
            {TutorialTextPanelPos.TopLeft,      (new Vector2(0f,   1f),   new Vector2( 328.5f,-186.5f))},
            {TutorialTextPanelPos.TopRight,     (new Vector2(1f,   1f),   new Vector2(-328.5f,-186.5f))},
            {TutorialTextPanelPos.BottomLeft,   (new Vector2(0f,   0f),   new Vector2( 328.5f, 186.5f))},
            {TutorialTextPanelPos.BottomRight,  (new Vector2(1f,   0f),   new Vector2(-328.5f, 186.5f))}
        };

    private void Start()
    {
        CustomEvents.OnCompleteTutorialStep += CompleteStep;
        CustomEvents.OnRunStepAfterWait += RunStepAfterWait;
        CustomEvents.OnForceRunStep += ForceRunStep;
    }

    public void SetCurrentStepInProccess(BuildingTileViewEnum buildingTileViewEnum)
    {
        switch (buildingTileViewEnum)
        {
            case BuildingTileViewEnum.StoneMining:
                _currentStepInProcess = _currentStep.TutorialStepEnum == TutorialStepEnum.MissionConstructionStoneMining_39;
                break;
            case BuildingTileViewEnum.AttackingStructures:
                _currentStepInProcess = _currentStep.TutorialStepEnum == TutorialStepEnum.MissionConstructionBallista_41;
                break;
        }
    }


    public void LoadTutorial(int tutorialStepIndex, bool prologueCompleted)
    {
        if (tutorialStepIndex >= (int)TutorialStepEnum.CompleteAllTutorials_72)
        {
            _isCompleteAllTutorials = true;
            return;
        }

        if (!prologueCompleted) return;

        // ищем нужный шаг и при необходимости «откатываемся» по цепочке
        _currentStepIndex = _steps.FindIndex(s => (int)s.TutorialStepEnum == tutorialStepIndex);
        if (_currentStepIndex < 0) return;

        while (_currentStepIndex > 0 && _steps[_currentStepIndex].RequirePreviousStep)
        {
            _currentStepIndex--;
        }

        RunStep(false);
    }

    private void RunStep(bool runStepAfterWait)
    {
        _currentStep = _steps[_currentStepIndex];

        if (_currentStep.WaitRunStep && !runStepAfterWait) return;

        if (_currentStep.ClickView != null) _currentStep.ClickView.SetActive(true);
        _tutorialPanelText.text = Language.TextStatic[_currentStep.TextNumber];

        // поставить якорь и позицию
        var (anchor, offset) = PanelLayout[_currentStep.TutorialTextPanelPos];
        var rect = _tutorialPanel.GetComponent<RectTransform>();
        rect.anchorMin = rect.anchorMax = anchor;
        rect.anchoredPosition = offset;

        // включаем‑выключаем UI‑элементы
        _tutorialPanel.SetActive(true);
        _justContinueButton.SetActive(_currentStep.JustContinue);
        _justCloseButton.SetActive(_currentStep.JustClose);

        // активируем TutorialArrowWorld
        ActivateWorldArrow();

        // меняем скорость игры
        if (GetTutorialStepEnum() is TutorialStepEnum.MissionBallistaDescription_42 or TutorialStepEnum.MissionBuildingTakeDamage_56)
        {
            _gameSpeedSystem.ChangeGameSpeed((int)GameSpeedEnum.Pause, true);
        }

        CustomEvents.FireStartTutorialStep(_currentStep.TutorialStepEnum);
    }

    private void ActivateWorldArrow()
    {
        if (_currentStep.ArrowObject == TutorialArrowObjectEnum.None) return;

        Transform target;
        switch (_currentStep.ArrowObject)
        {
            case TutorialArrowObjectEnum.BaseFoundation:
                target = _allTileObjects.FindGroundTileObject(GroundTileViewEnum.BaseFoundation).transform;
                _tutorialArrowWorld.SetObjectTransform(target);
                break;
            case TutorialArrowObjectEnum.Forest:
                target = _allTileObjects.FindGroundTileObject(GroundTileViewEnum.Forest).transform;
                _tutorialArrowWorld.SetObjectTransform(target);
                break;
            case TutorialArrowObjectEnum.WoodExtraction:
                // Игрок стоял афк и ничего не делал 6 дней после установки добычи дерева и камня, добычи дерева нету,
                // поэтому мы ищем добычу камня, если ее тоже нету, то указываем на базу
                target = _allTileObjects.FindBuildingOnTileObject(BuildingTileViewEnum.WoodExtraction)?.transform
                      ?? _allTileObjects.FindBuildingOnTileObject(BuildingTileViewEnum.StoneMining)?.transform
                      ?? _allTileObjects.FindBuildingOnTileObject(BuildingTileViewEnum.Base).transform;
                _tutorialArrowWorld.SetObjectTransform(target);
                break;
            case TutorialArrowObjectEnum.DamagedBuilding:
                target = _allTileObjects.FindDamagedBuildingOnTileObject().transform;
                _tutorialArrowWorld.SetObjectTransform(target);
                break;

        }
        _tutorialArrowWorld.gameObject.SetActive(true);
    }


    private void CompleteStep(TutorialStepEnum stepEnum)
    {
        if (IsCompleteAllTutorial() || GetTutorialStepEnum() != stepEnum) return;

        ResetStep();

        var nextEnum = (TutorialStepEnum)((int)stepEnum + 1);
        _currentStepIndex = _steps.FindIndex(s => s.TutorialStepEnum == nextEnum);

        SaveTutorial(nextEnum);

        if (_currentStepIndex >= 0) RunStep(false);
    }

    private void ResetStep()
    {
        _currentStepInProcess = false;
        if (_currentStep.ClickView != null) _currentStep.ClickView.SetActive(false);
        _tutorialPanel.SetActive(false);
        _justContinueButton.SetActive(false);

        // выключаем TutorialArrowWorld
        if (_tutorialArrowWorld != null) _tutorialArrowWorld.gameObject.SetActive(false);

        // меняем скорость игры
        if (GetTutorialStepEnum() is TutorialStepEnum.MissionDefeatMissionDescription_65)
        {
            _gameSpeedSystem.ChangeGameSpeed((int)GameSpeedEnum.Default, true);
        }
    }

    public void SaveTutorial(TutorialStepEnum stepEnum)
    {
        _hangarSaveGame.SaveTutorialStep((int)stepEnum);
    }

    // Степ без необходимости нажатия, просто инфа
    public void JustContinueButton()
    {
        if (_tutorialCanvasGroup.alpha == 0) return;
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        CompleteStep(_currentStep.TutorialStepEnum);
    }

    // Закрывает окно, если оно мешает
    public void JustCloseButton()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        _tutorialPanel.SetActive(false);
    }

    // Вызывается после ожидания определенных условий, но требует чтобы текущий степ совпадал
    public void RunStepAfterWait(TutorialStepEnum stepEnum)
    {
        if (IsCompleteAllTutorial() || _currentStep.TutorialStepEnum != stepEnum) return;
        RunStep(true);
    }

    /// <summary>
    /// Мгновенно переходит к указанному шагу и сразу запускаем его,
    /// </summary>
    public void ForceRunStep(TutorialStepEnum stepEnum)
    {
        if (IsCompleteAllTutorial()) return;

        ResetStep();
        _currentStepIndex = _steps.FindIndex(s => s.TutorialStepEnum == stepEnum);
        SaveTutorial(stepEnum);
        RunStep(true);
    }


    // Считываем нажатие на карту ландшафта
    public void SelectCard(GroundTileViewEnum groundTileView)
    {
        if (IsCompleteMissionTutorial()) return;

        switch (groundTileView)
        {
            case GroundTileViewEnum.BaseFoundation:
                CompleteStep(TutorialStepEnum.MissionSelectBaseFoundationCard_10);
                break;
            case GroundTileViewEnum.Forest:
                CompleteStep(TutorialStepEnum.MissionSelectForestCard_31);
                break;
        }

        CustomEvents.FireTurnOffTutorialCardObjectView();
    }

    // Считываем установку тайла ландшафта
    public void SetCard(GroundTileViewEnum groundTileView)
    {
        if (IsCompleteMissionTutorial()) return;

        switch (groundTileView)
        {
            case GroundTileViewEnum.BaseFoundation:
                CompleteStep(TutorialStepEnum.MissionSetBaseFoundationCard_11);
                break;
            case GroundTileViewEnum.Forest:
                CompleteStep(TutorialStepEnum.MissionSetForestCard_32);
                break;
        }
    }

    // Считываем нажатие на тайл земли
    public void SelectGroundTileObject(TileObject tileObject)
    {
        if (IsCompleteMissionTutorial()) return;

        switch (_currentStep.TutorialStepEnum)
        {
            case TutorialStepEnum.MissionSelectTileObjectForRepair_57:
                if (tileObject.GroundTileObject().CurrentGroundTile() == null) return;
                if (!tileObject.BuildingTileObject().HaveBuildingGameObject()) return;
                if (!tileObject.BuildingHealth().IsFullHealth())
                {
                    CompleteStep(TutorialStepEnum.MissionSelectTileObjectForRepair_57);
                }
                break;

            case TutorialStepEnum.MissionToggleOnSettlement_43:
                _tutorialArrowWorld.gameObject.SetActive(false);
                break;

            case TutorialStepEnum.MissionSelectTileWithResourceExtraction_48:
                if (tileObject.BuildingTileObject().HaveBuildingGameObject() && tileObject.BuildingTileObject().CurrentBuildingTile().BuildingTileView is BuildingTileViewEnum.WoodExtraction or BuildingTileViewEnum.StoneMining or BuildingTileViewEnum.Base)
                {
                    CompleteStep(TutorialStepEnum.MissionSelectTileWithResourceExtraction_48);
                }
                break;

            case TutorialStepEnum.MissionSelectBaseFoundationTile_12:
                if (tileObject.GroundTileObject().CurrentGroundTile().GroundTileView == GroundTileViewEnum.BaseFoundation)
                {
                    CompleteStep(TutorialStepEnum.MissionSelectBaseFoundationTile_12);
                }
                break;

            case TutorialStepEnum.MissionSelectForestTile_33:
                if (tileObject.GroundTileObject().CurrentGroundTile().GroundTileView is GroundTileViewEnum.Forest or GroundTileViewEnum.Oasis or GroundTileViewEnum.Grove)
                {
                    CompleteStep(TutorialStepEnum.MissionSelectForestTile_33);
                }
                break;
        }
    }

    // Считываем нажатие на тип здания BuildingType в SelectTilePanel
    public void SelectBuildingType(BuildingTileViewEnum buildingTileView)
    {
        if (IsCompleteMissionTutorial()) return;

        switch (buildingTileView)
        {
            case BuildingTileViewEnum.Base:
                CompleteStep(TutorialStepEnum.MissionSelectBaseTypeButton_17);
                break;
            case BuildingTileViewEnum.WoodExtraction:
                CompleteStep(TutorialStepEnum.MissionSelectWoodExtractionTypeButton_36);
                break;
        }
    }

    // Считываем наведение курсора на здание BuildingItem в SelectTilePanel
    public void SelectBuildingItem(Building building)
    {
        if (IsCompleteMissionTutorial()) return;

        if (building == _tutorialBuildings[0])
        {
            CompleteStep(TutorialStepEnum.MissionSelectSettlementBuildingItem_18);
        }
    }

    // Считываем нажатие на здание BuildingItem в SelectTilePanel
    public void ClickBuildingItem(Building building, BuildingState buildingState)
    {
        if (IsCompleteMissionTutorial()) return;

        if (building == _tutorialBuildings[0] && _currentStep.TutorialStepEnum == TutorialStepEnum.MissionStartConstructSettlement_20)
        {
            CompleteStep(TutorialStepEnum.MissionStartConstructSettlement_20);
        }

        if (building == _tutorialBuildings[1] && _currentStep.TutorialStepEnum == TutorialStepEnum.MissionStartConstructionManualWoodMining_37)
        {
            CompleteStep(TutorialStepEnum.MissionStartConstructionManualWoodMining_37);
        }

        if (buildingState == BuildingState.Repair && _currentStep.TutorialStepEnum == TutorialStepEnum.MissionRepairBuilding_59)
        {
            CompleteStep(TutorialStepEnum.MissionRepairBuilding_59);
        }

        CustomEvents.FireTurnOffTutorialCardObjectView();
    }


    // Считываем конец постройки здания определенного типа
    public void CompleteConstructionBuilding(BuildingTileViewEnum buildingTileView)
    {
        if (IsCompleteMissionTutorial()) return;

        switch (buildingTileView)
        {
            case BuildingTileViewEnum.Base:
                ForceRunStep(TutorialStepEnum.MissionAfterBaseSetStartTimer_23);
                break;
            case BuildingTileViewEnum.WoodExtraction:
                if (_currentStep.TutorialStepEnum <= TutorialStepEnum.MissionConstructionStoneMining_39) ForceRunStep(TutorialStepEnum.MissionConstructionStoneMining_39);
                break;
            case BuildingTileViewEnum.StoneMining:
                if (_currentStep.TutorialStepEnum <= TutorialStepEnum.MissionCompleteStoneAndWoodExtractionDescription_40) ForceRunStep(TutorialStepEnum.MissionCompleteStoneAndWoodExtractionDescription_40);
                break;
            case BuildingTileViewEnum.AttackingStructures:
                if (_currentStep.TutorialStepEnum <= TutorialStepEnum.MissionBallistaDescription_42) ForceRunStep(TutorialStepEnum.MissionBallistaDescription_42);
                break;
        }
    }

    // Считываем нажатие на вкл/выкл работы здания
    public void ClickToggleBuildingWork(bool isWorkNow)
    {
        if (IsCompleteMissionTutorial()) return;

        if (isWorkNow) CompleteStep(TutorialStepEnum.MissionToggleOffSettlement_30);
        if (!isWorkNow) CompleteStep(TutorialStepEnum.MissionToggleOnSettlement_43);
    }

    public bool CanChangeGameSpeed(int gameSpeed)
    {
        if (!IsCompleteMissionTutorial())
        {
            if (GetTutorialStepEnum() > TutorialStepEnum.MissionBallistaDescription_42 && GetTutorialStepEnum() < TutorialStepEnum.MissionDoubleTripleGameSpeedDescription_55 && gameSpeed == (int)GameSpeedEnum.Default) return false;
            if (GetTutorialStepEnum() < TutorialStepEnum.MissionPauseGame_24 && gameSpeed == (int)GameSpeedEnum.Pause) return false;
            if (GetTutorialStepEnum() < TutorialStepEnum.MissionDoubleTripleGameSpeedDescription_55 && gameSpeed is (int)GameSpeedEnum.Double or (int)GameSpeedEnum.Triple) return false;
            if (GetTutorialStepEnum() < TutorialStepEnum.MissionDefaultGameSpeed_38 && gameSpeed == (int)GameSpeedEnum.Default) return false;
            if (GetTutorialStepEnum() > TutorialStepEnum.MissionToggleOnSettlement_43 && GetTutorialStepEnum() < TutorialStepEnum.MissionPrepareAttack_54) return false;
        }

        return true;
    }

    // Считываем изменение скорости игры
    public void ChangeGameSpeed(int gameSpeed)
    {
        if (IsCompleteMissionTutorial()) return;

        GameSpeedEnum gameSpeedEnum = (GameSpeedEnum)gameSpeed;

        switch (gameSpeedEnum)
        {
            case GameSpeedEnum.Pause:
                CompleteStep(TutorialStepEnum.MissionPauseGame_24);
                CompleteStep(TutorialStepEnum.MissionDefaultGameSpeed_38);
                break;
            case GameSpeedEnum.Default:
                CompleteStep(TutorialStepEnum.MissionDefaultGameSpeed_38);
                break;
        }
    }

    public bool CanSelectCardObject(Tile tile)
    {
        if (IsCompleteMissionTutorial()) return true;

        if (_currentStep.TutorialStepEnum == TutorialStepEnum.SpaceOpenLearningPanel_67) return true;


        if (_currentStep.TutorialStepEnum >= TutorialStepEnum.MissionConstructionBallista_41) return true;

        switch (tile.GroundTileView)
        {
            case GroundTileViewEnum.BaseFoundation:
                return _currentStep.TutorialStepEnum == TutorialStepEnum.MissionSelectBaseFoundationCard_10;
            case GroundTileViewEnum.Forest:
                return _currentStep.TutorialStepEnum == TutorialStepEnum.MissionSelectForestCard_31;
            case GroundTileViewEnum.Mountain:
                return _currentStep.TutorialStepEnum == TutorialStepEnum.MissionConstructionStoneMining_39;

        }

        return _currentStep.TutorialStepEnum > TutorialStepEnum.MissionCompleteStoneAndWoodExtractionDescription_40;
    }

    public bool CanBuildOrUpgrade()
    {
        if (IsCompleteMissionTutorial()) return true;

        if (_currentStep.TutorialStepEnum < TutorialStepEnum.MissionStartConstructSettlement_20) return false;
        if (_currentStep.TutorialStepEnum == TutorialStepEnum.MissionTileForestDescription_35) return false;
        return true;
    }

    public bool CanInputOnTile()
    {
        if (IsCompleteMissionTutorial()) return true;

        if (_currentStep.TutorialStepEnum == TutorialStepEnum.SpaceOpenLearningPanel_67) return true;

        if (_currentStep.TutorialStepEnum is TutorialStepEnum.MissionSelectBaseFoundationTile_12 or TutorialStepEnum.MissionSetBaseFoundationCard_11) return true;
        if (_currentStep.TutorialStepEnum is TutorialStepEnum.MissionSelectForestTile_33 or TutorialStepEnum.MissionSetForestCard_32) return true;
        if (_currentStep.TutorialStepEnum is TutorialStepEnum.MissionConstructionStoneMining_39) return true;

        return _currentStep.TutorialStepEnum > TutorialStepEnum.MissionConstructionStoneMining_39;
    }

    public bool CanDetectGroundTileObject(TileObject tileObject)
    {
        if (IsCompleteMissionTutorial()) return true;

        if (_currentStep.TutorialStepEnum == TutorialStepEnum.SpaceOpenLearningPanel_67) return true;

        switch (_currentStep.TutorialStepEnum)
        {
            case TutorialStepEnum.MissionSelectBaseFoundationTile_12:
                return tileObject.GroundTileObject().CurrentGroundTile().GroundTileView == GroundTileViewEnum.BaseFoundation;
            case TutorialStepEnum.MissionSelectForestTile_33:
                return tileObject.GroundTileObject().CurrentGroundTile().GroundTileView == GroundTileViewEnum.Forest;
            case TutorialStepEnum.MissionToggleOnSettlement_43:
                return tileObject.GroundTileObject().CurrentGroundTile().GroundTileView == GroundTileViewEnum.BaseFoundation;
        }

        return true;
    }

    public bool CanClickBuildButton()
    {
        if (IsCompleteMissionTutorial()) return true;

        if (_currentStep.TutorialStepEnum == TutorialStepEnum.SpaceOpenLearningPanel_67) return true;

        if (_currentStep.TutorialStepEnum < TutorialStepEnum.MissionClickBuildButton_16) return false;

        CustomEvents.FireCompleteTutorialStep(TutorialStepEnum.MissionClickBuildButton_16);
        CustomEvents.FireCompleteTutorialStep(TutorialStepEnum.MissionClickBuildButton_34);
        CustomEvents.FireCompleteTutorialStep(TutorialStepEnum.MissionClickBuildButton_58);

        if (_currentStep.TutorialStepEnum > TutorialStepEnum.MissionSelectBaseTypeButton_17 &&
            _currentStep.TutorialStepEnum < TutorialStepEnum.MissionClickBuildButton_34) return false;

        return true;
    }

    public bool CanClickBuildingTypeButton(Tile tile)
    {
        if (IsCompleteMissionTutorial()) return true;

        if (_currentStep.TutorialStepEnum == TutorialStepEnum.SpaceOpenLearningPanel_67) return true;

        switch (_currentStep.TutorialStepEnum)
        {
            case TutorialStepEnum.MissionSelectWoodExtractionTypeButton_36:
                return tile.BuildingTileView == BuildingTileViewEnum.WoodExtraction;
        }

        return _currentStep.TutorialStepEnum != TutorialStepEnum.MissionTileForestDescription_35;
    }

    public bool CanClickBuildingWorkButton()
    {
        if (IsCompleteMissionTutorial()) return true;

        if (_currentStep.TutorialStepEnum == TutorialStepEnum.SpaceOpenLearningPanel_67) return true;

        return _currentStep.TutorialStepEnum >= TutorialStepEnum.MissionToggleOffSettlement_30;
    }

    public bool CanClickChangeModeButton()
    {
        if (IsCompleteMissionTutorial()) return true;

        if (_currentStep.TutorialStepEnum is TutorialStepEnum.MissionShipWeaponModeActive_62 or TutorialStepEnum.MissionPlanetModeActive_64) return true;

        return false;
    }

    public bool CanClearTileDetector()
    {
        if (IsCompleteMissionTutorial()) return true;

        return _currentStep.TutorialStepEnum > TutorialStepEnum.MissionDefaultGameSpeed_38;
    }

    public bool CanCancelSeletCard()
    {
        if (IsCompleteMissionTutorial()) return true;

        return _currentStep.TutorialStepEnum is not (TutorialStepEnum.MissionSetBaseFoundationCard_11 or TutorialStepEnum.MissionSetForestCard_32);
    }

    private void OnDestroy()
    {
        CustomEvents.OnCompleteTutorialStep -= CompleteStep;
        CustomEvents.OnRunStepAfterWait -= RunStepAfterWait;
        CustomEvents.OnForceRunStep -= ForceRunStep;
    }
}

[System.Serializable]
public class TutorialStep
{
    public TutorialStepEnum TutorialStepEnum;
    public GameObject ClickView;
    public int TextNumber;
    public TutorialTextPanelPos TutorialTextPanelPos;
    public TutorialArrowObjectEnum ArrowObject;
    public bool JustContinue;
    public bool JustClose;
    public bool RequirePreviousStep;
    public bool WaitRunStep;
}

public enum TutorialArrowObjectEnum
{
    None = 0,
    BaseFoundation = 1,
    Forest = 2,
    WoodExtraction = 3,
    DamagedBuilding = 4,
}

public enum TutorialStepEnum
{
    None = -1,
    SpaceHangarWelcome_0 = 0,
    SpaceAiCorePanel_1 = 1,
    SpaceQuantPanel_2 = 2,
    SpaceOpenResourcePanel_3 = 3,
    SpaceResourcePanelDescription_4 = 4,
    SpaceOpenMap_5 = 5,
    SpaceMapDescription_6 = 6,
    SpaceSelectNode_7 = 7,
    SpaceStartMission_8 = 8,
    MissionStartDescription_9 = 9,
    MissionSelectBaseFoundationCard_10 = 10,
    MissionSetBaseFoundationCard_11 = 11,
    MissionSelectBaseFoundationTile_12 = 12,
    MissionSelectTilePanelDescription_13 = 13,
    MissionEcology1_14 = 14,
    MissionEcology2_15 = 15,
    MissionClickBuildButton_16 = 16,
    MissionSelectBaseTypeButton_17 = 17,
    MissionSelectSettlementBuildingItem_18 = 18,
    MissionOpenResourcePanel_19 = 19,
    MissionStartConstructSettlement_20 = 20,
    MissionBuildingDescription1_21 = 21,
    MissionBuildingDescription2_22 = 22,
    MissionAfterBaseSetStartTimer_23 = 23,
    MissionPauseGame_24 = 24,
    MissionSettlementRequiredResurcesDescription_25 = 25,
    MissionDataFragmentsDescription_26 = 26,
    MissionSettlementChangeResourceRequired_27 = 27,
    MissionPauseRequiredProductionResourceDescription_28 = 28,
    MissionAddCardsDescription_29 = 29,
    MissionToggleOffSettlement_30 = 30,
    MissionSelectForestCard_31 = 31,
    MissionSetForestCard_32 = 32,
    MissionSelectForestTile_33 = 33,
    MissionClickBuildButton_34 = 34,
    MissionTileForestDescription_35 = 35,
    MissionSelectWoodExtractionTypeButton_36 = 36,
    MissionStartConstructionManualWoodMining_37 = 37,
    MissionDefaultGameSpeed_38 = 38,
    MissionConstructionStoneMining_39 = 39,
    MissionCompleteStoneAndWoodExtractionDescription_40 = 40,
    MissionConstructionBallista_41 = 41,
    MissionBallistaDescription_42 = 42,
    MissionToggleOnSettlement_43 = 43,
    MissionEnergyBeamDescription_44 = 44,
    MissionTileCombineDescription1_45 = 45,
    MissionTileCombineDescription2_46 = 46,
    MissionTileCombineDescription3_47 = 47,
    MissionSelectTileWithResourceExtraction_48 = 48,
    MissionProductionModifierDescription_49 = 49,
    MissionEventPanel_50 = 50,
    MissionOpenSkillsPanel_51 = 51,
    MissionSkillsPanelDescription_52 = 52,
    MissionShardsDescription_53 = 53,
    MissionPrepareAttack_54 = 54,
    MissionDoubleTripleGameSpeedDescription_55 = 55,
    MissionBuildingTakeDamage_56 = 56,
    MissionSelectTileObjectForRepair_57 = 57,
    MissionClickBuildButton_58 = 58,
    MissionRepairBuilding_59 = 59,
    MissionUpgradeBuildingDescription1_60 = 60,
    MissionUpgradeBuildingDescription2_61 = 61,
    MissionShipWeaponModeActive_62 = 62,
    MissionShipWeaponModeDescription_63 = 63,
    MissionPlanetModeActive_64 = 64,
    MissionDefeatMissionDescription_65 = 65,
    MissionGoodLuckDescription_66 = 66,
    SpaceOpenLearningPanel_67 = 67,
    SpaceSelectNotLearnBuilding_68 = 68,
    SpaceLearnBuilding_69 = 69,
    SpaceLearnBuildingDescription_70 = 70,
    SpaceExploreSpace_71 = 71,
    CompleteAllTutorials_72 = 72,
}

public enum TutorialTextPanelPos
{
    Center,
    Bottom,
    Top,
    Left,
    Right,
    TopLeft,
    TopRight,
    BottomLeft,
    BottomRight
}
