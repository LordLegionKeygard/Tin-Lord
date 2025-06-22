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
    public Action<int> OnChoiceSelected;
    private bool _waitingForContinueAfterChance;
    private float _successChance = 0.5f;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _mainText;
    [SerializeField] private EventNodeButton _buttonPrefab;
    [SerializeField] private Transform _buttonsHolder;

    [Header("Systems")]
    [SerializeField] private QuantsSystem _quantsSystem;
    [SerializeField] private AiCoreSystem _aiCoreSystem;
    [SerializeField] private MainResources _mainResources;
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
        foreach (var ch in step.Choices)
        {
            if (ch.Kind == ChoiceKind.Standard &&
                ch.Standard.Rewards != null &&
                ch.Standard.Rewards.Count > 0)
            {
                sb.AppendLine();
                foreach (var r in ch.Standard.Rewards)
                {
                    int amt = UnityEngine.Random.Range(r.MinAmount, r.MaxAmount);
                    _cachedRewards.Add((r, amt));
                    sb.AppendLine(FormatRewardLine(r, amt));
                }
                break;
            }
        }
        _mainText.text = sb.ToString();

        foreach (Transform t in _buttonsHolder) Destroy(t.gameObject);

        int visible = Mathf.Min(step.Choices.Count, 4);
        for (int i = visible - 1; i >= 0; i--)
        {
            var choice = step.Choices[i];
            string text = $"{i + 1}. {Language.TextStatic[choice.ChoiseTextNumber]}";

            var btn = Instantiate(_buttonPrefab, _buttonsHolder);
            btn.Setup(text, () => OnChoice(choice));

            bool allowed = choice.Kind == ChoiceKind.Standard ? RequirementMet(choice.Standard) : true;
            btn.SetInteractable(allowed);
        }
    }

    private void OnChoice(StepChoice choice)
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);

        var step = _dialogue.Steps[_stack.Peek()];
        int idx = step.Choices.IndexOf(choice);
        OnChoiceSelected?.Invoke(idx);

        if (choice.Kind == ChoiceKind.Chance)
        {
            bool success = UnityEngine.Random.value < _successChance;
            int textId = success ? choice.Chance.SuccessTextNumber : choice.Chance.FailureTextNumber;
            var rewards = success ? choice.Chance.SuccessRewards : choice.Chance.FailureRewards;
            var sb = new StringBuilder();
            sb.AppendLine(Language.TextStatic[textId]);
            sb.AppendLine(); //пропускаем строку перед выводом награды

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

            _waitingForContinueAfterChance = true;

            var contBtn = Instantiate(_buttonPrefab, _buttonsHolder);
            contBtn.Setup($"1. {Language.TextStatic[33]}", () => FinishChance());
        }
        else if (choice.Kind == ChoiceKind.Random)
        {
            var rnd = choice.Random;
            var type = rnd.PossibleRewards[UnityEngine.Random.Range(0, rnd.PossibleRewards.Count)];
            int amount = UnityEngine.Random.Range(rnd.MinAmount, rnd.MaxAmount + 1);

            GrantReward(new EventReward { Type = type }, amount);

            var sb = new StringBuilder();
            sb.AppendLine(Language.TextStatic[choice.ChoiseTextNumber]);
            sb.AppendLine();
            sb.AppendLine(FormatRewardLine(new EventReward { Type = type }, amount)); _mainText.text = sb.ToString();

            int next = rnd.NextStepIndex;
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

        if (_aiCoreSystem.GetAiCores() > 0) _commandCenterSaveGame.SaveGameData(false);
    }

    private void FinishChance()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);

        foreach (var (r, amt) in _pendingRewards) GrantReward(r, amt);

        _waitingForContinueAfterChance = false;
        _onFinished?.Invoke();
        _mapSystem.CompleteCurrentNode();
        _stack.Clear();
        Close();
    }

    private string FormatRewardLine(EventReward reward, int amount)
    {
        return reward.Type switch
        {
            RewardType.AiCore =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[185]}: {amount}<color=#00FF00>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[185]}: {amount}<color=#00FF00>",
            RewardType.Quants =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[186]}: {amount}<color=#00FF00>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[186]}: {amount}<color=#00FF00>",
            RewardType.Memory =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[175]}: {amount}<color=#00FF00>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[175]}: {amount}<color=#00FF00>",
            RewardType.Wood =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[153]}: {amount}<color=#00FF00>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[153]}: {amount}<color=#00FF00>",
            RewardType.Stone =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[154]}: {amount}<color=#00FF00>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[154]}: {amount}<color=#00FF00>",
            RewardType.IronOre =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[155]}: {amount}<color=#00FF00>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[155]}: {amount}<color=#00FF00>",
            RewardType.CopperOre =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[156]}: {amount}<color=#00FF00>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[156]}: {amount}<color=#00FF00>",
            RewardType.Coal =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[157]}: {amount}<color=#00FF00>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[157]}: {amount}<color=#00FF00>",
            RewardType.Oil =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[158]}: {amount}<color=#00FF00>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[158]}: {amount}<color=#00FF00>",
            RewardType.Water =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[159]}: {amount}<color=#00FF00>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[159]}: {amount}<color=#00FF00>",
            RewardType.Sand =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[160]}: {amount}<color=#00FF00>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[160]}: {amount}<color=#00FF00>",
            RewardType.Electricity =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[161]}: {amount}<color=#00FF00>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[161]}: {amount}<color=#00FF00>",
            RewardType.StoneBlock =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[162]}: {amount}<color=#00FF00>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[162]}: {amount}<color=#00FF00>",
            RewardType.IronIngot =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[163]}: {amount}<color=#00FF00>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[163]}: {amount}<color=#00FF00>",
            RewardType.SteelIngot =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[164]}: {amount}<color=#00FF00>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[164]}: {amount}<color=#00FF00>",
            RewardType.CopperPlate =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[165]}: {amount}<color=#00FF00>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[165]}: {amount}<color=#00FF00>",
            RewardType.Concrete =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[166]}: {amount}<color=#00FF00>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[166]}: {amount}<color=#00FF00>",
            RewardType.Steam =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[167]}: {amount}<color=#00FF00>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[167]}: {amount}<color=#00FF00>",
            RewardType.Glass =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[168]}: {amount}<color=#00FF00>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[168]}: {amount}<color=#00FF00>",
            RewardType.CopperWire =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[169]}: {amount}<color=#00FF00>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[169]}: {amount}<color=#00FF00>",
            RewardType.GearWheel =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[170]}: {amount}<color=#00FF00>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[170]}: {amount}<color=#00FF00>",
            RewardType.ElectronicCircuit =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[171]}: {amount}<color=#00FF00>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[171]}: {amount}<color=#00FF00>",
            RewardType.Processor =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[172]}: {amount}<color=#00FF00>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[172]}: {amount}<color=#00FF00>",
            RewardType.Engine =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[173]}: {amount}<color=#00FF00>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[173]}: {amount}<color=#00FF00>",
            RewardType.ElectricEngine =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[174]}: {amount}<color=#00FF00>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[174]}: {amount}<color=#00FF00>",
            RewardType.BeamEnergy =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[176]}: {amount}<color=#00FF00>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[176]}: {amount}<color=#00FF00>",
            _ => null
        };
    }

    private void GrantReward(EventReward reward, int amount)
    {
        switch (reward.Type)
        {
            case RewardType.AiCore:
                AudioManager.Instance.PlayerOneShot(amount > 0 ? FMODEvents.Instance.ReceivedAiCore : FMODEvents.Instance.LostAiCore, transform.position);
                _aiCoreSystem.ChangeAiCores(amount);
                break;
            case RewardType.Quants:
                AudioManager.Instance.PlayerOneShot(amount > 0 ? FMODEvents.Instance.ReceivedQuants : FMODEvents.Instance.LostQuants, transform.position);
                _quantsSystem.ChangeQuants(amount);
                break;
            case RewardType.Memory:
                AudioManager.Instance.PlayerOneShot(amount > 0 ? FMODEvents.Instance.ReceivedMemory : FMODEvents.Instance.LostMemory, transform.position);
                _mainResources.ChangeResource(ResourceEnum.MemoryFragment, amount);
                break;
        }
    }

    private int GetCurrent(RewardType type)
    {
        return type switch
        {
            RewardType.Quants => _quantsSystem.GetQuants(),
            RewardType.AiCore => _aiCoreSystem.GetAiCores(),
            RewardType.Memory => (int)_mainResources.GetResourceAmountForEnum(ResourceEnum.MemoryFragment),
            _ => 0
        };
    }

    private bool RequirementMet(StandardChoiceData std)
    {
        if (std == null) return true;

        var req = std.ChoiceRequired;
        if (req.RequiredType == RewardType.None) return true;

        return GetCurrent(req.RequiredType) >= req.Amount;
    }

    public void PlayerInputSelectNumber(int n)
    {
        if (!gameObject.activeInHierarchy || n is < 1 or > 4) return;

        if (_waitingForContinueAfterChance)
        {
            if (n == 1) FinishChance();
            return;
        }

        if (_stack.Count == 0 || _dialogue == null) return;

        int stepIndex = _stack.Peek();
        if (stepIndex < 0 || stepIndex >= _dialogue.Steps.Count) return;

        var step = _dialogue.Steps[stepIndex];

        int choiceIndex = n - 1;
        if (choiceIndex >= step.Choices.Count) return;

        var choice = step.Choices[choiceIndex];

        if (choice.Kind == ChoiceKind.Standard && !RequirementMet(choice.Standard)) return;

        OnChoice(choice);
    }

    public void Close() => gameObject.SetActive(false);
}
