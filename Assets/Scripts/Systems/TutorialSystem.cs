using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class TutorialSystem : MonoBehaviour
{
    [Inject] private HangarSaveGame _hangarSaveGame;
    [SerializeField] private List<TutorialStep> _steps;
    [SerializeField] private GameObject _tutorialPanel;
    [SerializeField] private TextMeshProUGUI _tutorialPanelText;
    [SerializeField] private GameObject _justContinueButton;
    private int _currentStepIndex = -1;
    private TutorialStep _currentStep;

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

    private void Start() => CustomEvents.OnCompleteTutorialStep += CompleteStep;

    public void LoadTutorial(int savedEnum, bool prologueCompleted)
    {
        if (!prologueCompleted || savedEnum == (int)TutorialStepEnum.Complete) return;

        // ищем нужный шаг и при необходимости «откатываемся» по цепочке
        _currentStepIndex = _steps.FindIndex(s => (int)s.TutorialStepEnum == savedEnum);
        if (_currentStepIndex < 0) return;

        while (_currentStepIndex > 0 && _steps[_currentStepIndex].RequirePreviousStep)
        {
            _currentStepIndex--;
        }

        RunStep();
    }

    private void RunStep()
    {
        _currentStep = _steps[_currentStepIndex];

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
    }

    private void CompleteStep(TutorialStepEnum stepEnum)
    {
        if (_currentStep.TutorialStepEnum != stepEnum) return;

        if (_currentStep.ClickView != null) _currentStep.ClickView.SetActive(false);
        _tutorialPanel.SetActive(false);
        _justContinueButton.SetActive(false);

        var nextEnum = (TutorialStepEnum)((int)stepEnum + 1);
        _currentStepIndex = _steps.FindIndex(s => s.TutorialStepEnum == nextEnum);

        SaveTutorial(nextEnum);

        if (_currentStepIndex >= 0) RunStep();
        else _currentStep = null; // конец туториала
    }

    public void JustContinue() => CompleteStep(_currentStep.TutorialStepEnum);

    private void SaveTutorial(TutorialStepEnum stepEnum)
    {
        _hangarSaveGame.SaveTutorialStep((int)stepEnum);
    }

    private void OnDestroy()
    {
        CustomEvents.OnCompleteTutorialStep -= CompleteStep;
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
}

public enum TutorialStepEnum
{
    SpaceHangarWelcome,
    SpaceAiCorePanel,
    SpaceQuantPanel,
    SpaceOpenMap,
    SpaceSelectNode,
    SpaceStartMission,
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
