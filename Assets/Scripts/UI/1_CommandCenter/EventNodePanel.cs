using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;
using Zenject;

public class EventNodePanel : MonoBehaviour
{
    [Inject] private CommandCenterSaveGame _commandCenterSaveGame;
    private Stack<int> _stack = new();
    private DialogueSequence _dialogue;
    private Action _onFinished;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _mainText;
    [SerializeField] private EventNodeButton _buttonPrefab;
    [SerializeField] private Transform _buttonsHolder;

    [Header("Systems")]
    [SerializeField] private QuantsSystem _quantsSystem;
    [SerializeField] private AiCoreSystem _aiCoreSystem;
    [SerializeField] private BuildingsLearnPanel _buildingsLearnPanel;
    [SerializeField] private MapSystem _mapSystem;

    private readonly List<(EventReward reward, int amount)> _cachedRewards = new();
    private readonly List<(EventReward reward, int amount)> _pendingRewards = new();

    public void Open(DialogueSequence node, Action onFinished = null)
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
        var sb = new StringBuilder();

        sb.AppendLine(Language.TextStatic[step.TextNumber]);

        _cachedRewards.Clear();
        foreach (var choice in step.Choices)
        {
            if (choice.Kind == ChoiceKind.Standard &&
                choice.Standard.Rewards != null &&
                choice.Standard.Rewards.Count > 0)
            {
                sb.AppendLine();
                foreach (var r in choice.Standard.Rewards)
                {
                    int amt = UnityEngine.Random.Range(r.MinAmount, r.MaxAmount);
                    _cachedRewards.Add((r, amt));

                    var line = FormatRewardLine(r, amt);
                    if (!string.IsNullOrEmpty(line))
                        sb.AppendLine(line);
                }
                break;
            }
        }

        _mainText.text = sb.ToString();

        foreach (Transform t in _buttonsHolder) Destroy(t.gameObject);

        int count = Mathf.Min(step.Choices.Count, 4);
        for (int i = count - 1; i >= 0; i--)
        {
            var choice = step.Choices[i];
            string text = $"{i + 1}. {Language.TextStatic[choice.ChoiseTextNumber]}";

            var btn = Instantiate(_buttonPrefab, _buttonsHolder);
            btn.Setup(text, () => OnChoice(choice));
        }
    }

    private void OnChoice(StepChoice choice)
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);

        if (choice.Kind == ChoiceKind.Chance)
        {
            bool success = UnityEngine.Random.value < choice.Chance.SuccessChance;
            int textId = success ? choice.Chance.SuccessTextNumber : choice.Chance.FailureTextNumber;
            var rewards = success ? choice.Chance.SuccessRewards : choice.Chance.FailureRewards;
            var sb = new StringBuilder();
            sb.AppendLine(Language.TextStatic[textId]);

            _pendingRewards.Clear();
            if (rewards != null)
            {
                foreach (var r in rewards)
                {
                    int amount = UnityEngine.Random.Range(r.MinAmount, r.MaxAmount);
                    _pendingRewards.Add((r, amount));

                    var line = FormatRewardLine(r, amount);
                    if (!string.IsNullOrEmpty(line)) sb.AppendLine(line);
                }
            }

            _mainText.text = sb.ToString();

            foreach (Transform t in _buttonsHolder) Destroy(t.gameObject);

            var contBtn = Instantiate(_buttonPrefab, _buttonsHolder);
            contBtn.Setup($"1.{Language.TextStatic[33]}", () =>
            {
                AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);

                foreach (var (r, amt) in _pendingRewards) GrantReward(r, amt);

                _onFinished?.Invoke();
                _mapSystem.CompleteCurrentNode();
                _stack.Clear();
                Close();
            });
        }
        else
        {
            var toGrant = new List<(EventReward, int)>();
            if (_cachedRewards.Count > 0)
            {
                toGrant.AddRange(_cachedRewards);
                _cachedRewards.Clear();
            }
            else if (choice.Standard.Rewards != null)
            {
                foreach (var r in choice.Standard.Rewards)
                {
                    int amt = UnityEngine.Random.Range(r.MinAmount, r.MaxAmount);
                    toGrant.Add((r, amt));
                }
            }

            foreach (var (r, amt) in toGrant) GrantReward(r, amt);


            int next = choice.Standard.NextStepIndex;
            if (next < 0)
            {
                _onFinished?.Invoke();
                _mapSystem.CompleteCurrentNode();
                _stack.Clear();
                Close();
            }
            else
            {
                _stack.Push(next);
                ShowStep(next);
            }
        }

        _commandCenterSaveGame.SaveGameData(false);
    }

    private string FormatRewardLine(EventReward reward, int amount)
    {
        return reward.Type switch
        {
            RewardType.AiCore =>
                amount >= 0
                    ? $"{Language.TextStatic[279]} {amount}"
                    : $"{Language.TextStatic[282]} {amount}",
            RewardType.Quants =>
                amount >= 0
                    ? $"{Language.TextStatic[280]} {amount}"
                    : $"{Language.TextStatic[283]} {amount}",
            RewardType.Memory =>
                amount >= 0
                    ? $"{Language.TextStatic[281]} {amount}"
                    : $"{Language.TextStatic[284]} {amount}",
            _ => null
        };
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
