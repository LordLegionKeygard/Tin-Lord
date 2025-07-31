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
        if (_currentStep.ArrowObject != TutorialArrowObjectEnum.None)
        {
            switch (_currentStep.ArrowObject)
            {
                case TutorialArrowObjectEnum.BaseFoundation:
                    var target = _allTileObjects.FindGroundTileObject(GroundTileViewEnum.BaseFoundation).transform;
                    _tutorialArrowWorld.SetObjectTransform(target);
                    _tutorialArrowWorld.gameObject.SetActive(true);
                    break;
            }
        }

        CustomEvents.FireStartTutorialStep(_currentStep.TutorialStepEnum);
    }

    private void CompleteStep(TutorialStepEnum stepEnum)
    {
        if (_currentStep == null || _currentStep.TutorialStepEnum != stepEnum) return;

        if (_currentStep.ClickView != null) _currentStep.ClickView.SetActive(false);
        _tutorialPanel.SetActive(false);
        _justContinueButton.SetActive(false);

        // включаем кнопки
        foreach (var item in _currentStep.ButtonsDisabled)
        {
            item.enabled = true;
        }

        // выключаем TutorialArrowWorld
        _tutorialArrowWorld.gameObject.SetActive(false);

        var nextEnum = (TutorialStepEnum)((int)stepEnum + 1);
        _currentStepIndex = _steps.FindIndex(s => s.TutorialStepEnum == nextEnum);

        SaveTutorial(nextEnum);

        if (_currentStepIndex >= 0) RunStep(false);
        else _currentStep = null; // конец туториала
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

    // Вызывается при соверешении определенных условий
    public void RunStepAfterWait(TutorialStepEnum stepEnum)
    {
        if (_currentStep == null || _currentStep.TutorialStepEnum != stepEnum) return;
        RunStep(true);
    }


    // Считываем нажатие на карту ландшафта
    public void SelectCard(GroundTileViewEnum groundTileView)
    {
        switch (groundTileView)
        {
            case GroundTileViewEnum.BaseFoundation:
                CompleteStep(TutorialStepEnum.MissionSelectBaseFoundationCard_10);
                break;
        }
    }

    // Считываем установку тайла ландшафта
    public void SetCard(GroundTileViewEnum groundTileView)
    {
        switch (groundTileView)
        {
            case GroundTileViewEnum.BaseFoundation:
                CompleteStep(TutorialStepEnum.MissionSetBaseFoundationCard_11);
                break;
        }
    }

    // Считываем нажатие на тайл земли
    public void SelectGroundTileObject(GroundTileViewEnum groundTileViewEnum)
    {
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
        switch (buildingTileView)
        {
            case BuildingTileViewEnum.Base:
                CompleteStep(TutorialStepEnum.MissionSelectBaseTypeButton_17);
                break;
        }
    }

    // Считываем наведение курсора на здание BuildingItem в SelectTilePanel
    public void SelectBuildingItem(Building building)
    {
        if (building == _tutorialBuildings[0])
        {
            CompleteStep(TutorialStepEnum.MissionSelectSettlementBuildingItem_18);
        }
    }

    private void OnDestroy()
    {
        CustomEvents.OnCompleteTutorialStep -= CompleteStep;
        CustomEvents.OnRunStepAfterWait -= RunStepAfterWait;
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
