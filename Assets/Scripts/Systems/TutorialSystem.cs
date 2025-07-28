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
    [SerializeField] private GameObject _justContinueButon;
    private int _currentStepIndex;
    private TutorialStep _currentStep;

    private void Start()
    {
        CustomEvents.OnCompleteTutorialStep += CompleteStep;
    }

    public void LoadTutorial(int stepIndex, bool prologueCompleted)
    {
        if (stepIndex == (int)TutorialStepEnum.Complete || !prologueCompleted) return;

        for (int i = 0; i < _steps.Count; i++)
        {
            if ((int)_steps[i].TutorialStepEnum == stepIndex)
            {
                _currentStepIndex = i;
                RunStep();
                return;
            }
        }
    }

    private void SaveTutorial(TutorialStepEnum stepEnum)
    {
        _hangarSaveGame.SaveTutorialStep((int)stepEnum);
    }

    private void RunStep()
    {
        _currentStep = _steps[_currentStepIndex];

        if (_currentStep.ClickView != null) _currentStep.ClickView.SetActive(true);
        _tutorialPanelText.text = Language.TextStatic[_currentStep.TextNumber];

        var rectTrans = _tutorialPanel.GetComponent<RectTransform>();
        var targetPos = GetTutorialPanelPos(_currentStep.TutorialTextPanelPos);
        var currentPos = rectTrans.anchoredPosition;
        currentPos.x = targetPos.x;
        currentPos.y = targetPos.y;
        rectTrans.anchoredPosition = currentPos;

        _tutorialPanel.SetActive(true);
        _justContinueButon.SetActive(_currentStep.JustContinue);
    }

    private void CompleteStep(TutorialStepEnum tutorialStepEnum)
    {
        if (_currentStep.TutorialStepEnum != tutorialStepEnum) return;

        if (_currentStep.ClickView != null) _currentStep.ClickView.SetActive(false);
        _justContinueButon.SetActive(false);
        _tutorialPanel.SetActive(false);

        var nextEnum = (TutorialStepEnum)((int)tutorialStepEnum + 1);
        for (int i = 0; i < _steps.Count; i++)
        {
            if (_steps[i].TutorialStepEnum == nextEnum)
            {
                _currentStepIndex = i;
                _currentStep = _steps[i];

                SaveTutorial(nextEnum);
                RunStep();
                return;
            }
        }

        SaveTutorial(nextEnum);
        _currentStep = null;
    }


    public void JustContinue()
    {
        CompleteStep(_currentStep.TutorialStepEnum);
    }


    private Vector2 GetTutorialPanelPos(TutorialTextPanelPos pos)
    {
        switch (pos)
        {
            case TutorialTextPanelPos.Center: return new Vector2(0, 0);
            case TutorialTextPanelPos.Bottom: return new Vector2(0, -353);
            case TutorialTextPanelPos.Top: return new Vector2(0, 353);
        }
        return Vector2.zero;
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
}

[System.Serializable]
public enum TutorialStepEnum
{
    HangarWelcome = 0,
    AiCorePanel = 1,
    QuantPanel = 2,
    HangarOpenMap = 3,
    HangarSelectNode = 4,
    HangarStartMission = 5,
    Complete = 6,
}

[System.Serializable]
public enum TutorialTextPanelPos
{
    Center = 0,
    Bottom = 1,
    Top = 2,
}
