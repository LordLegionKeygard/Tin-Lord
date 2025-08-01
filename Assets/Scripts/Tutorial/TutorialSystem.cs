using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TutorialSystem : MonoBehaviour
{
    [Inject] private HangarSaveGame _hangarSaveGame;
    [SerializeField] private AllTileObjects _allTileObjects;
    [SerializeField] private GameObject _tutorialPanel;
    [SerializeField] private TextMeshProUGUI _tutorialPanelText;
    [SerializeField] private GameObject _justContinueButton;
    [SerializeField] private CanvasGroup _tutorialCanvasGroup;
    [SerializeField] private TutorialArrowWorld _tutorialArrowWorld;
    [SerializeField] private Building[] _tutorialBuildings;
    [SerializeField] private List<TutorialStep> _steps;
    private int _currentStepIndex = -1;
    private TutorialStep _currentStep;
    public bool IsStartTutorial() => _currentStep != null ? _currentStep.TutorialStepEnum == TutorialStepEnum.SpaceHangarWelcome_0 : false;
    public TutorialStepEnum GetTutorialStepEnum() => _currentStep != null ? _currentStep.TutorialStepEnum : TutorialStepEnum.None;
    public Building GetTutorialBuilding(int number) => _tutorialBuildings[number];
    public bool IsCompleteMissionTutorial() => _currentStep != null ? _currentStep.TutorialStepEnum == TutorialStepEnum.CompleteMissionTutorial : true;

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


    public void LoadTutorial(int tutorialStepEnum, bool prologueCompleted)
    {
        if (!prologueCompleted || tutorialStepEnum == (int)TutorialStepEnum.Complete) return;

        // ищем нужный шаг и при необходимости «откатываемся» по цепочке
        _currentStepIndex = _steps.FindIndex(s => (int)s.TutorialStepEnum == tutorialStepEnum);
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

        // выключаем кнопки
        foreach (var item in _currentStep.ButtonsDisabled)
        {
            item.enabled = false;
        }

        // активируем TutorialArrowWorld
        ActivateWorldArrow();

        CustomEvents.FireStartTutorialStep(_currentStep.TutorialStepEnum);
    }

    private void ActivateWorldArrow()
    {
        if (_currentStep.ArrowObject != TutorialArrowObjectEnum.None)
        {
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

            }
            _tutorialArrowWorld.gameObject.SetActive(true);
        }
    }

    private void CompleteStep(TutorialStepEnum stepEnum)
    {
        if (_currentStep == null || _currentStep.TutorialStepEnum != stepEnum) return;

        ResetStepObjects();

        var nextEnum = (TutorialStepEnum)((int)stepEnum + 1);
        _currentStepIndex = _steps.FindIndex(s => s.TutorialStepEnum == nextEnum);

        SaveTutorial(nextEnum);

        if (_currentStepIndex >= 0) RunStep(false);
        else _currentStep = null; // конец туториала
    }

    private void ResetStepObjects()
    {
        if (_currentStep.ClickView != null) _currentStep.ClickView.SetActive(false);
        _tutorialPanel.SetActive(false);
        _justContinueButton.SetActive(false);

        // включаем кнопки
        foreach (var item in _currentStep.ButtonsDisabled)
        {
            item.enabled = true;
        }

        // выключаем TutorialArrowWorld
        if (_tutorialArrowWorld != null) _tutorialArrowWorld.gameObject.SetActive(false);
    }

    private void SaveTutorial(TutorialStepEnum stepEnum)
    {
        _hangarSaveGame.SaveTutorialStep((int)stepEnum);
    }

    // Степ без необходимости нажатия, просто инфа
    public void JustContinue()
    {
        if (_tutorialCanvasGroup.alpha == 0) return;
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        CompleteStep(_currentStep.TutorialStepEnum);
    }

    // Вызывается после ожидания определенных условий, но требует чтобы текущий степ совпадал
    public void RunStepAfterWait(TutorialStepEnum stepEnum)
    {
        if (_currentStep == null || _currentStep.TutorialStepEnum != stepEnum) return;
        RunStep(true);
    }

    /// <summary>
    /// Мгновенно переходит к указанному шагу и сразу запускаем его,
    /// </summary>
    public void ForceRunStep(TutorialStepEnum stepEnum)
    {
        if (_currentStep != null)
        {
            ResetStepObjects();
        }

        _currentStepIndex = _steps.FindIndex(s => s.TutorialStepEnum == stepEnum);
        if (_currentStepIndex < 0) return; // нет такого шага – выходим

        SaveTutorial(stepEnum);
        RunStep(true);
    }


    // Считываем нажатие на карту ландшафта
    public void SelectCard(GroundTileViewEnum groundTileView)
    {
        if (_currentStep == null || IsCompleteMissionTutorial()) return;

        switch (groundTileView)
        {
            case GroundTileViewEnum.BaseFoundation:
                CompleteStep(TutorialStepEnum.MissionSelectBaseFoundationCard_10);
                break;
            case GroundTileViewEnum.Forest:
                CompleteStep(TutorialStepEnum.MissionSelectForestCard_31);
                break;
        }
    }

    // Считываем установку тайла ландшафта
    public void SetCard(GroundTileViewEnum groundTileView)
    {
        if (_currentStep == null || IsCompleteMissionTutorial()) return;

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
    public void SelectGroundTileObject(GroundTileViewEnum groundTileViewEnum)
    {
        if (_currentStep == null || IsCompleteMissionTutorial()) return;

        switch (groundTileViewEnum)
        {
            case GroundTileViewEnum.BaseFoundation:
                CompleteStep(TutorialStepEnum.MissionSelectBaseFoundationTile_12);
                break;
        }
    }

    // Считываем нажатие на тип здания BuildingType в SelectTilePanel
    public void SelectBuildingType(BuildingTileViewEnum buildingTileView)
    {
        if (_currentStep == null || IsCompleteMissionTutorial()) return;

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
        if (_currentStep == null || IsCompleteMissionTutorial()) return;

        if (building == _tutorialBuildings[0])
        {
            CompleteStep(TutorialStepEnum.MissionSelectSettlementBuildingItem_18);
        }
    }

    // Считываем нажатие на здание BuildingItem в SelectTilePanel
    public void StartConstructBuilding(Building building)
    {
        if (_currentStep == null || IsCompleteMissionTutorial()) return;

        if (building == _tutorialBuildings[0])
        {
            CompleteStep(TutorialStepEnum.MissionStartConstructSettlement_20);
        }

        if (building == _tutorialBuildings[1])
        {
            CompleteStep(TutorialStepEnum.MissionStartConstructionManualWoodMining_37);
        }
    }

    public void ChangeGameSpeed(int gameSpeed)
    {
        if (_currentStep == null || IsCompleteMissionTutorial()) return;

        GameSpeedEnum gameSpeedEnum = (GameSpeedEnum)gameSpeed;

        switch (gameSpeedEnum)
        {
            case GameSpeedEnum.Pause:
                CompleteStep(TutorialStepEnum.MissionPauseGame_24);
                break;
            case GameSpeedEnum.Default:

                break;
        }
    }

    public bool CanSelectCardObject(Tile tile)
    {
        if (_currentStep == null || _currentStep.TutorialStepEnum == TutorialStepEnum.CompleteMissionTutorial) return true;

        switch (tile.GroundTileView)
        {
            case GroundTileViewEnum.BaseFoundation:
                return _currentStep.TutorialStepEnum == TutorialStepEnum.MissionSelectBaseFoundationCard_10;
            case GroundTileViewEnum.Forest:
                return _currentStep.TutorialStepEnum == TutorialStepEnum.MissionSelectForestCard_31;

        }

        return IsCompleteMissionTutorial();
    }

    public bool CanBuildOrUpgrade()
    {
        if (_currentStep.TutorialStepEnum < TutorialStepEnum.MissionStartConstructSettlement_20) return false;
        if (_currentStep.TutorialStepEnum == TutorialStepEnum.MissionTileForestDescription_35) return false;
        return true;
    }

    public bool CanInputOnTile()
    {
        if (_currentStep == null || _currentStep.TutorialStepEnum == TutorialStepEnum.CompleteMissionTutorial) return true;

        if (_currentStep.TutorialStepEnum is TutorialStepEnum.MissionSelectBaseFoundationTile_12 or TutorialStepEnum.MissionSetBaseFoundationCard_11) return true;
        if (_currentStep.TutorialStepEnum is TutorialStepEnum.MissionSelectForestTile_33 or TutorialStepEnum.MissionSetForestCard_32) return true;

        return IsCompleteMissionTutorial();
    }

    public bool CanDetectGroundTileObject(TileObject tileObject)
    {
        switch (_currentStep.TutorialStepEnum)
        {
            case TutorialStepEnum.MissionSelectBaseFoundationTile_12:
                return tileObject.GroundTileObject().CurrentGroundTile().GroundTileView == GroundTileViewEnum.BaseFoundation;
            case TutorialStepEnum.MissionSelectForestTile_33:
                return tileObject.GroundTileObject().CurrentGroundTile().GroundTileView == GroundTileViewEnum.Forest;
        }

        return true;
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
    public bool RequirePreviousStep;
    public bool WaitRunStep;
    public Button[] ButtonsDisabled;
}

public enum TutorialArrowObjectEnum
{
    None = 0,
    BaseFoundation = 1,
    Forest = 2,
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






    MissionSkillsPanel,
    CompleteMissionTutorial = 998,
    Complete = 999
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
