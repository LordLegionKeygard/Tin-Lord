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

    [Header("Rewards")]
    [SerializeField] private QuantsSystem _quantsSystem;
    [SerializeField] private AiCoreSystem _aiCoreSystem;
    [SerializeField] private BuildingsLearnPanel _buildingsLearnPanel;

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
        var mainTextBuilder = new System.Text.StringBuilder();

        mainTextBuilder.AppendLine(Language.TextStatic[step.TextNumber]);

        if (step.Choices != null && step.Choices.Count > 0)
        {
            var rewards = step.Choices[0].Rewards;
            if (rewards != null && rewards.Count > 0)
            {
                mainTextBuilder.AppendLine();
                foreach (var reward in rewards)
                {
                    int amount = Random.Range(reward.MinAmount, reward.MaxAmount);

                    string rewardText = reward.Type switch
                    {
                        RewardType.AiCore => amount >= 0
                            ? $"{Language.TextStatic[279]} {amount}"    // "Вы получили ядра ИИ:"
                            : $"{Language.TextStatic[282]} {amount}",  // "Вы потеряли ядра ИИ:"
                        RewardType.Quants => amount >= 0
                            ? $"{Language.TextStatic[280]} {amount}"    // "Вы получили кванты:"
                            : $"{Language.TextStatic[283]} {amount}",  // "Вы потеряли кванты:"
                        RewardType.Memory => amount >= 0
                            ? $"{Language.TextStatic[281]} {amount}"    // "Вы получили фрагменты памяти:"
                            : $"{Language.TextStatic[284]} {amount}",  // "Вы потеряли фрагменты памяти:"
                        _ => null
                    };

                    if (!string.IsNullOrEmpty(rewardText))
                    {
                        mainTextBuilder.AppendLine(rewardText);
                    }
                }
            }
        }


        _mainText.text = mainTextBuilder.ToString();

        // очищаем старые кнопки
        foreach (Transform trans in _buttonsHolder)
        {
            Destroy(trans.gameObject);
        }

        int visible = Mathf.Min(step.Choices.Count, 4);
        for (int i = visible - 1; i >= 0; i--)
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

        if (choice.Rewards != null)
        {
            foreach (var reward in choice.Rewards)
            {
                int amount = Random.Range(reward.MinAmount, reward.MaxAmount);
                GrantReward(reward, amount);
            }
        }

        _commandCenterSaveGame.SaveGameData(false);

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



    private void GrantReward(EventReward reward, int amount)
    {
        switch (reward.Type)
        {
            case RewardType.AiCore:
                _aiCoreSystem.ChangeAiCores(amount);
                break;
            case RewardType.Quants:
                _quantsSystem.ChangeQuants(amount);
                break;
            case RewardType.Memory:
                _buildingsLearnPanel.ChangeFragments(amount);
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
