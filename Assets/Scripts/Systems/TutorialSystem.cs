using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TutorialSystem : MonoBehaviour
{
    [Inject] private HangarSaveGame _hangarSaveGame;
    [SerializeField] private List<TutorialStep> _steps;
    [SerializeField] private GameObject _tutorialPanel;
    [SerializeField] private TextMeshProUGUI _tutorialPanelText;
    [SerializeField] private GameObject _justContinueButton;
    [SerializeField] private CanvasGroup _tutorialCanvasGroup;
    private int _currentStepIndex = -1;
    private TutorialStep _currentStep;
    public bool IsStartTutorial() => _currentStep != null ? _currentStep.TutorialStepEnum == TutorialStepEnum.SpaceHangarWelcome_0 : false;
    public TutorialStepEnum GetTutorialStepEnum() => _currentStep != null ? _currentStep.TutorialStepEnum : TutorialStepEnum.None;

    private static readonly Dictionary<TutorialTextPanelPos, (Vector2 anchor, Vector2 offset)> PanelLayout =
        new()
        {
            {TutorialTextPanelPos.Center,       (new Vector2(0.5f, 0.5f), Vector2.zero)},
            {TutorialTextPanelPos.Bottom,       (new Vector2(0.5f, 0f),   new Vector2(0,  186.5f))},
            {TutorialTextPanelPos.Top,          (new Vector2(0.5f, 1f),   new Vector2(0, -186.5f))},
            {TutorialTextPanelPos.Left,         (new Vector2(0f,   0.5f), new Vector2( 186.5f, 0))},
            {TutorialTextPanelPos.Right,        (new Vector2(1f,   0.5f), new Vector2(-186.5f, 0))},
            {TutorialTextPanelPos.TopLeft,      (new Vector2(0f,   1f),   new Vector2( 186.5f,-186.5f))},
            {TutorialTextPanelPos.TopRight,     (new Vector2(1f,   1f),   new Vector2(-186.5f,-186.5f))},
            {TutorialTextPanelPos.BottomLeft,   (new Vector2(0f,   0f),   new Vector2( 186.5f, 186.5f))},
            {TutorialTextPanelPos.BottomRight,  (new Vector2(1f,   0f),   new Vector2(-186.5f, 186.5f))}
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

        var nextEnum = (TutorialStepEnum)((int)stepEnum + 1);
        _currentStepIndex = _steps.FindIndex(s => s.TutorialStepEnum == nextEnum);

        SaveTutorial(nextEnum);

        if (_currentStepIndex >= 0) RunStep(false);
        else _currentStep = null; // конец туториала
    }

    // Степ без необходимости нажатия, просто инфа
    public void JustContinue()
    {
        if (_tutorialCanvasGroup.alpha == 0) return;
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);
        CompleteStep(_currentStep.TutorialStepEnum);
    }

    // // Вызывается внутри самих кнопок, если не удается реализовать логику в их скриптах
    // public void ClickButtonCompleteTest(int stepNumber)
    // {
    //     if (_currentStep == null || (int)_currentStep.TutorialStepEnum != stepNumber) return;
    //     CompleteStep(_currentStep.TutorialStepEnum);
    // }

    // Вызывается при определенных условиях и выполняется если совпадает с текущим шагом
    public void RunStepAfterWait(TutorialStepEnum stepEnum)
    {
        if (_currentStep == null || _currentStep.TutorialStepEnum != stepEnum) return;
        RunStep(true);
    }

    private void SaveTutorial(TutorialStepEnum stepEnum)
    {
        _hangarSaveGame.SaveTutorialStep((int)stepEnum);
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
    public bool JustContinue;
    public bool RequirePreviousStep;
    public bool WaitRunStep;
    public Button[] ButtonsDisabled;
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
    Complete
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
