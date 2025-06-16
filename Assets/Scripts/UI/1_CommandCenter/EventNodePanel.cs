using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Zenject;

public class EventNodePanel : MonoBehaviour
{
    [Inject] private CommandCenterSaveGame _commandCenterSaveGame;
    private Stack<int> _stack = new();
    private DialogueSequence _dialogue;
    [SerializeField] private MapSystem _mapSystem;
    private System.Action _onFinished;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _mainText;
    [SerializeField] private EventNodeButton _buttonPrefab;
    [SerializeField] private Transform _buttonsHolder;
    [SerializeField] private AiCoreSystem _aiCoreSystem;

    public void Open(DialogueSequence node, System.Action onFinished = null)
    {
        _onFinished = onFinished;
        _dialogue = node;
        _stack.Clear();
        _stack.Push(0);
        ShowStep(0);
    }

    private void ShowStep(int stepIndex)
    {
        var step = _dialogue.Steps[stepIndex];

        _mainText.text = Language.TextStatic[step.TextNumber];

        foreach (Transform trans in _buttonsHolder) Destroy(trans.gameObject);

        int visible = Mathf.Min(step.Choices.Count, 4);

        for (int i = 0; i < visible; i++)
        {
            var choice = step.Choices[i];
            string text = $"{i + 1}. {Language.TextStatic[choice.ChoiseTextNumber]}";

            var button = Instantiate(_buttonPrefab, _buttonsHolder);
            button.Setup(text, () => OnChoice(choice));
        }
    }

    private void OnChoice(StepChoice choice)
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);

        // выдаём награды
        if (choice.Rewards != null)
        {
            foreach (var reward in choice.Rewards) GrantReward(reward);
        }

        if (choice.NextStepIndex < 0)
        {
            _onFinished?.Invoke();
            _mapSystem.CompleteCurrentNode();
            Close();
        }
        else
        {
            _stack.Push(choice.NextStepIndex);
            ShowStep(choice.NextStepIndex);
        }
    }


    private void GrantReward(EventReward eventReward)
    {
        switch (eventReward.Type)
        {
            case RewardType.AiCore:
                _aiCoreSystem.ChangeAiCores(1);
                break;
        }

        _commandCenterSaveGame.SaveGameData(false);
    }

    public void PlayerInputSelectNumber(int n)
    {
        if (!gameObject.activeInHierarchy || n is < 1 or > 4) return;

        var step = _dialogue.Steps[_stack.Peek()];
        int idx = n - 1;

        if (idx < step.Choices.Count)
        {
            OnChoice(step.Choices[idx]);
        }
    }

    public void Close() => gameObject.SetActive(false);
}
