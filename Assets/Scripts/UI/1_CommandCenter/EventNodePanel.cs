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
            if (ch.Kind == ChoiceKind.Standard && ch.Standard.Rewards != null && ch.Standard.Rewards.Count > 0)
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
            if (ch.Kind == ChoiceKind.Random && ch.Random.PossibleRewards?.Count > 0)
            {
                sb.AppendLine();
                var rnd = ch.Random;
                var type = rnd.PossibleRewards[UnityEngine.Random.Range(0, rnd.PossibleRewards.Count)];
                int amt = UnityEngine.Random.Range(rnd.MinAmount, rnd.MaxAmount + 1);

                var reward = new EventReward { Type = type };
                _cachedRewards.Add((reward, amt));
                sb.AppendLine(FormatRewardLine(reward, amt));
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

            bool allowed = choice.Kind != ChoiceKind.Standard || RequirementMet(choice.Standard);
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
            var toGrant = new List<(EventReward, int)>(_cachedRewards);
            _cachedRewards.Clear();

            foreach (var (r, amt) in toGrant) GrantReward(r, amt);

            int next = choice.Random.NextStepIndex;
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
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[185]}: {amount}</color>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[185]}: {amount}</color>",
            RewardType.Quants =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[186]}: {amount}</color>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[186]}: {amount}</color>",
            RewardType.Memory =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[175]}: {amount}</color>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[175]}: {amount}</color>",
            RewardType.Wood =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[153]}: {amount}</color>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[153]}: {amount}</color>",
            RewardType.Stone =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[154]}: {amount}</color>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[154]}: {amount}</color>",
            RewardType.IronOre =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[155]}: {amount}</color>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[155]}: {amount}</color>",
            RewardType.CopperOre =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[156]}: {amount}</color>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[156]}: {amount}</color>",
            RewardType.Coal =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[157]}: {amount}</color>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[157]}: {amount}</color>",
            RewardType.Oil =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[158]}: {amount}</color>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[158]}: {amount}</color>",
            RewardType.Water =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[159]}: {amount}</color>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[159]}: {amount}</color>",
            RewardType.Sand =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[160]}: {amount}</color>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[160]}: {amount}</color>",
            RewardType.Electricity =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[161]}: {amount}</color>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[161]}: {amount}</color>",
            RewardType.StoneBlock =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[162]}: {amount}</color>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[162]}: {amount}</color>",
            RewardType.IronIngot =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[163]}: {amount}</color>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[163]}: {amount}</color>",
            RewardType.SteelIngot =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[164]}: {amount}</color>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[164]}: {amount}</color>",
            RewardType.CopperPlate =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[165]}: {amount}</color>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[165]}: {amount}</color>",
            RewardType.Concrete =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[166]}: {amount}</color>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[166]}: {amount}</color>",
            RewardType.Steam =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[167]}: {amount}</color>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[167]}: {amount}</color>",
            RewardType.Glass =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[168]}: {amount}</color>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[168]}: {amount}</color>",
            RewardType.CopperWire =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[169]}: {amount}</color>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[169]}: {amount}</color>",
            RewardType.GearWheel =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[170]}: {amount}</color>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[170]}: {amount}</color>",
            RewardType.ElectronicCircuit =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[171]}: {amount}</color>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[171]}: {amount}</color>",
            RewardType.Processor =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[172]}: {amount}</color>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[172]}: {amount}</color>",
            RewardType.Engine =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[173]}: {amount}</color>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[173]}: {amount}</color>",
            RewardType.ElectricEngine =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[174]}: {amount}</color>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[174]}: {amount}</color>",
            RewardType.BeamEnergy =>
                amount >= 0
                    ? $"<color=#00FF00>{Language.TextStatic[183]} {Language.TextStatic[176]}: {amount}</color>"
                    : $"<color=#FF0000>{Language.TextStatic[184]} {Language.TextStatic[176]}: {amount}</color>",
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
            case RewardType.Wood:
                AudioManager.Instance.PlayerOneShot(amount > 0 ? FMODEvents.Instance.ReceivedResource : FMODEvents.Instance.LostResource, transform.position);
                _mainResources.ChangeResource(ResourceEnum.Wood, amount);
                break;
            case RewardType.Stone:
                AudioManager.Instance.PlayerOneShot(amount > 0 ? FMODEvents.Instance.ReceivedResource : FMODEvents.Instance.LostResource, transform.position);
                _mainResources.ChangeResource(ResourceEnum.Stone, amount);
                break;
            case RewardType.IronOre:
                AudioManager.Instance.PlayerOneShot(amount > 0 ? FMODEvents.Instance.ReceivedResource : FMODEvents.Instance.LostResource, transform.position);
                _mainResources.ChangeResource(ResourceEnum.IronOre, amount);
                break;
            case RewardType.CopperOre:
                AudioManager.Instance.PlayerOneShot(amount > 0 ? FMODEvents.Instance.ReceivedResource : FMODEvents.Instance.LostResource, transform.position);
                _mainResources.ChangeResource(ResourceEnum.CopperOre, amount);
                break;
            case RewardType.Coal:
                AudioManager.Instance.PlayerOneShot(amount > 0 ? FMODEvents.Instance.ReceivedResource : FMODEvents.Instance.LostResource, transform.position);
                _mainResources.ChangeResource(ResourceEnum.Coal, amount);
                break;
            case RewardType.Oil:
                AudioManager.Instance.PlayerOneShot(amount > 0 ? FMODEvents.Instance.ReceivedResource : FMODEvents.Instance.LostResource, transform.position);
                _mainResources.ChangeResource(ResourceEnum.Oil, amount);
                break;
            case RewardType.Water:
                AudioManager.Instance.PlayerOneShot(amount > 0 ? FMODEvents.Instance.ReceivedResource : FMODEvents.Instance.LostResource, transform.position);
                _mainResources.ChangeResource(ResourceEnum.Water, amount);
                break;
            case RewardType.Sand:
                AudioManager.Instance.PlayerOneShot(amount > 0 ? FMODEvents.Instance.ReceivedResource : FMODEvents.Instance.LostResource, transform.position);
                _mainResources.ChangeResource(ResourceEnum.Sand, amount);
                break;
            case RewardType.Electricity:
                AudioManager.Instance.PlayerOneShot(amount > 0 ? FMODEvents.Instance.ReceivedResource : FMODEvents.Instance.LostResource, transform.position);
                _mainResources.ChangeResource(ResourceEnum.Electricity, amount);
                break;
            case RewardType.StoneBlock:
                AudioManager.Instance.PlayerOneShot(amount > 0 ? FMODEvents.Instance.ReceivedResource : FMODEvents.Instance.LostResource, transform.position);
                _mainResources.ChangeResource(ResourceEnum.StoneBlock, amount);
                break;
            case RewardType.IronIngot:
                AudioManager.Instance.PlayerOneShot(amount > 0 ? FMODEvents.Instance.ReceivedResource : FMODEvents.Instance.LostResource, transform.position);
                _mainResources.ChangeResource(ResourceEnum.IronIngot, amount);
                break;
            case RewardType.SteelIngot:
                AudioManager.Instance.PlayerOneShot(amount > 0 ? FMODEvents.Instance.ReceivedResource : FMODEvents.Instance.LostResource, transform.position);
                _mainResources.ChangeResource(ResourceEnum.SteelIngot, amount);
                break;
            case RewardType.CopperPlate:
                AudioManager.Instance.PlayerOneShot(amount > 0 ? FMODEvents.Instance.ReceivedResource : FMODEvents.Instance.LostResource, transform.position);
                _mainResources.ChangeResource(ResourceEnum.CopperPlate, amount);
                break;
            case RewardType.Concrete:
                AudioManager.Instance.PlayerOneShot(amount > 0 ? FMODEvents.Instance.ReceivedResource : FMODEvents.Instance.LostResource, transform.position);
                _mainResources.ChangeResource(ResourceEnum.Concrete, amount);
                break;
            case RewardType.Steam:
                AudioManager.Instance.PlayerOneShot(amount > 0 ? FMODEvents.Instance.ReceivedResource : FMODEvents.Instance.LostResource, transform.position);
                _mainResources.ChangeResource(ResourceEnum.Steam, amount);
                break;
            case RewardType.Glass:
                AudioManager.Instance.PlayerOneShot(amount > 0 ? FMODEvents.Instance.ReceivedResource : FMODEvents.Instance.LostResource, transform.position);
                _mainResources.ChangeResource(ResourceEnum.Glass, amount);
                break;
            case RewardType.CopperWire:
                AudioManager.Instance.PlayerOneShot(amount > 0 ? FMODEvents.Instance.ReceivedResource : FMODEvents.Instance.LostResource, transform.position);
                _mainResources.ChangeResource(ResourceEnum.CopperWire, amount);
                break;
            case RewardType.GearWheel:
                AudioManager.Instance.PlayerOneShot(amount > 0 ? FMODEvents.Instance.ReceivedResource : FMODEvents.Instance.LostResource, transform.position);
                _mainResources.ChangeResource(ResourceEnum.GearWheel, amount);
                break;
            case RewardType.ElectronicCircuit:
                AudioManager.Instance.PlayerOneShot(amount > 0 ? FMODEvents.Instance.ReceivedResource : FMODEvents.Instance.LostResource, transform.position);
                _mainResources.ChangeResource(ResourceEnum.ElectronicCircuit, amount);
                break;
            case RewardType.Processor:
                AudioManager.Instance.PlayerOneShot(amount > 0 ? FMODEvents.Instance.ReceivedResource : FMODEvents.Instance.LostResource, transform.position);
                _mainResources.ChangeResource(ResourceEnum.Processor, amount);
                break;
            case RewardType.Engine:
                AudioManager.Instance.PlayerOneShot(amount > 0 ? FMODEvents.Instance.ReceivedResource : FMODEvents.Instance.LostResource, transform.position);
                _mainResources.ChangeResource(ResourceEnum.Engine, amount);
                break;
            case RewardType.ElectricEngine:
                AudioManager.Instance.PlayerOneShot(amount > 0 ? FMODEvents.Instance.ReceivedResource : FMODEvents.Instance.LostResource, transform.position);
                _mainResources.ChangeResource(ResourceEnum.ElectricEngine, amount);
                break;
            case RewardType.BeamEnergy:
                AudioManager.Instance.PlayerOneShot(amount > 0 ? FMODEvents.Instance.ReceivedResource : FMODEvents.Instance.LostResource, transform.position);
                _mainResources.ChangeResource(ResourceEnum.BeamEnergy, amount);
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
