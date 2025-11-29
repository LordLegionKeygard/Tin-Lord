using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;
using Zenject;

public class EventNodePanel : MonoBehaviour
{
    [Inject] private SpaceSaveGame _spaceSaveGame;
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
    [SerializeField] private ShardsCalculateSystem _shardsCalculateSystem;
    [SerializeField] private QuantsSystem _quantsSystem;
    [SerializeField] private AiCoreSystem _aiCoreSystem;
    [SerializeField] private MainResources _mainResources;
    [SerializeField] private MapSystem _mapSystem;

    [Header("RewardText")]
    private static readonly Dictionary<RewardType, int> RewardNameKey = new()
    {
        [RewardType.AiCore] = 185,
        [RewardType.Quants] = 186,
        [RewardType.Memory] = 175,
        [RewardType.Wood] = 153,
        [RewardType.Stone] = 154,
        [RewardType.IronOre] = 155,
        [RewardType.CopperOre] = 156,
        [RewardType.Coal] = 157,
        [RewardType.Oil] = 158,
        [RewardType.Water] = 159,
        [RewardType.Sand] = 160,
        [RewardType.Electricity] = 161,
        [RewardType.StoneBlock] = 162,
        [RewardType.IronIngot] = 163,
        [RewardType.SteelIngot] = 164,
        [RewardType.CopperPlate] = 165,
        [RewardType.Concrete] = 166,
        [RewardType.Steam] = 167,
        [RewardType.Glass] = 168,
        [RewardType.CopperWire] = 169,
        [RewardType.GearWheel] = 170,
        [RewardType.ElectronicCircuit] = 171,
        [RewardType.Processor] = 172,
        [RewardType.Engine] = 173,
        [RewardType.ElectricEngine] = 174,
        [RewardType.BeamEnergy] = 176,
        [RewardType.Shard] = 83
    };

    private readonly List<(EventReward reward, int amount)> _cachedRewards = new();
    private readonly List<(EventReward reward, int amount)> _pendingRewards = new();

    [Header("PoolsForRandomReward")]
    // Пулы для рандома
    private static readonly RewardType[] _randomResourceTypes = new[]
{
    RewardType.Wood,
    RewardType.Stone,
    RewardType.IronOre,
    RewardType.CopperOre,
    RewardType.Coal,
    RewardType.Oil,
    RewardType.Water,
    RewardType.Sand,
    RewardType.Electricity,
};

    private static readonly RewardType[] _randomMaterialTypes = new[]
    {
    RewardType.StoneBlock,
    RewardType.IronIngot,
    RewardType.SteelIngot,
    RewardType.CopperPlate,
    RewardType.Concrete,
    RewardType.Glass,
    RewardType.Steam,
};

    private static readonly RewardType[] _randomComponentTypes = new[]
    {
    RewardType.CopperWire,
    RewardType.GearWheel,
    RewardType.ElectronicCircuit,
    RewardType.Processor,
    RewardType.Engine,
    RewardType.ElectricEngine,
};

    // Маппинг RewardType -> ResourceEnum (для считывания количества и изменения)
    private bool TryMapRewardTypeToResourceEnum(RewardType type, out ResourceEnum res)
    {
        switch (type)
        {
            // Ресурсы
            case RewardType.Wood: res = ResourceEnum.Wood; return true;
            case RewardType.Stone: res = ResourceEnum.Stone; return true;
            case RewardType.IronOre: res = ResourceEnum.IronOre; return true;
            case RewardType.CopperOre: res = ResourceEnum.CopperOre; return true;
            case RewardType.Coal: res = ResourceEnum.Coal; return true;
            case RewardType.Oil: res = ResourceEnum.Oil; return true;
            case RewardType.Water: res = ResourceEnum.Water; return true;
            case RewardType.Sand: res = ResourceEnum.Sand; return true;
            case RewardType.Electricity: res = ResourceEnum.Electricity; return true;

            // Материалы
            case RewardType.StoneBlock: res = ResourceEnum.StoneBlock; return true;
            case RewardType.IronIngot: res = ResourceEnum.IronIngot; return true;
            case RewardType.SteelIngot: res = ResourceEnum.SteelIngot; return true;
            case RewardType.CopperPlate: res = ResourceEnum.CopperPlate; return true;
            case RewardType.Concrete: res = ResourceEnum.Concrete; return true;
            case RewardType.Steam: res = ResourceEnum.Steam; return true;
            case RewardType.Glass: res = ResourceEnum.Glass; return true;

            // Компоненты
            case RewardType.CopperWire: res = ResourceEnum.CopperWire; return true;
            case RewardType.GearWheel: res = ResourceEnum.GearWheel; return true;
            case RewardType.ElectronicCircuit: res = ResourceEnum.ElectronicCircuit; return true;
            case RewardType.Processor: res = ResourceEnum.Processor; return true;
            case RewardType.Engine: res = ResourceEnum.Engine; return true;
            case RewardType.ElectricEngine: res = ResourceEnum.ElectricEngine; return true;

            default:
                res = default;
                return false;
        }
    }

    // Универсальный резолвер для Random-типов
    private RewardType? ResolveRandomConcreteType(RewardType randKind, int amount)
    {
        RewardType[] pool;
        switch (randKind)
        {
            case RewardType.RandomResource: pool = _randomResourceTypes; break;
            case RewardType.RandomMaterial: pool = _randomMaterialTypes; break;
            case RewardType.RandomComponent: pool = _randomComponentTypes; break;
            default: return null;
        }

        // Копия пула для фильтрации
        var list = new List<RewardType>(pool);

        if (amount < 0)
        {
            // При штрафе оставляем только те, у которых запас > 0
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (!TryMapRewardTypeToResourceEnum(list[i], out var rEnum)) { list.RemoveAt(i); continue; }
                if (_mainResources.GetResourceAmountForEnum(rEnum) <= 0f)
                    list.RemoveAt(i);
            }
            if (list.Count == 0) return null; // нечего списывать
        }

        return list[UnityEngine.Random.Range(0, list.Count)];
    }

    /// <summary>
    /// Возвращает случайное значение в диапазоне,
    /// исходя из RewardCount и текущего _dialogue.
    /// </summary>
    private int RollAmount(RewardType type, RewardCount rewardCount)
    {
        // Особый случай
        if (type == RewardType.Shard) return _shardsCalculateSystem.GetCalculatedShards();

        int min = _dialogue.GetRewardAmount(rewardCount, true);
        int max = _dialogue.GetRewardAmount(rewardCount, false);
        int raw = UnityEngine.Random.Range(min, max + 1);

        // Явно применяем знак из настроек (и страхуемся Abs'ом)
        int signed = Mathf.Abs(raw);
        int sign;

        switch (rewardCount.PlusMinusEnum)
        {
            case PlusMinusEnum.Plus: sign = +1; break;
            case PlusMinusEnum.Minus: sign = -1; break;
            default: sign = +1; break; // по умолчанию — плюс
        }

        return sign * signed;
    }


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
        foreach (var choise in step.Choices)
        {
            if (choise.Kind == ChoiceKind.Standard && choise.Standard.Rewards != null && choise.Standard.Rewards.Count > 0)
            {
                sb.AppendLine();
                foreach (var reward in choise.Standard.Rewards)
                {
                    int amount = RollAmount(reward.Type, reward.RewardCount);

                    RewardType effective;
                    var line = FormatRewardLine(reward, amount, out effective);
                    if (!string.IsNullOrEmpty(line))
                    {
                        var resolvedReward = new EventReward { Type = effective, RewardCount = reward.RewardCount };
                        _cachedRewards.Add((resolvedReward, amount));
                        sb.AppendLine(line);
                    }
                }
            }
            if (choise.Kind == ChoiceKind.Random && choise.Random.PossibleRewards?.Count > 0)
            {
                sb.AppendLine();
                var rndReward = choise.Random;
                var type = rndReward.PossibleRewards[UnityEngine.Random.Range(0, rndReward.PossibleRewards.Count)];
                int amount = RollAmount(type, rndReward.RewardCount);

                var reward = new EventReward { Type = type, RewardCount = rndReward.RewardCount };

                RewardType effective;
                var line = FormatRewardLine(reward, amount, out effective); // effective == type
                if (!string.IsNullOrEmpty(line))
                {
                    _cachedRewards.Add((new EventReward { Type = effective, RewardCount = rndReward.RewardCount }, amount));
                    sb.AppendLine(line);
                }
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
                foreach (var reward in rewards)
                {
                    int amount = RollAmount(reward.Type, reward.RewardCount);

                    RewardType effective;
                    var line = FormatRewardLine(reward, amount, out effective);
                    if (!string.IsNullOrEmpty(line))
                    {
                        _pendingRewards.Add((new EventReward { Type = effective, RewardCount = reward.RewardCount }, amount));
                        sb.AppendLine(line);
                    }
                }
            }

            _mainText.text = sb.ToString();

            foreach (Transform trans in _buttonsHolder) Destroy(trans.gameObject);

            _waitingForContinueAfterChance = true;

            var contBtn = Instantiate(_buttonPrefab, _buttonsHolder);
            contBtn.Setup($"1. {Language.TextStatic[33]}", () => FinishChance());
        }
        else if (choice.Kind == ChoiceKind.Random)
        {
            var toGrant = new List<(EventReward, int)>(_cachedRewards);
            _cachedRewards.Clear();

            foreach (var (reward, amount) in toGrant) GrantReward(reward, amount);

            if (_aiCoreSystem.GetAiCores() <= 0)
            {
                if (choice.Random.NextStepIndex < 0)
                {
                    _onFinished?.Invoke();
                    _stack.Clear();
                    Close();
                }
                return;
            }

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
                foreach (var reward in choice.Standard.Rewards)
                {
                    int amount = RollAmount(reward.Type, reward.RewardCount);
                    toGrant.Add((reward, amount));
                }
            }

            foreach (var (reward, amount) in toGrant) GrantReward(reward, amount);

            if (_aiCoreSystem.GetAiCores() <= 0)
            {
                if (choice.Standard.NextStepIndex < 0)
                {
                    _onFinished?.Invoke();
                    _stack.Clear();
                    Close();
                }
                return;
            }

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

        if (_aiCoreSystem.GetAiCores() > 0) _spaceSaveGame.SaveDataToJson();
    }

    private void FinishChance()
    {
        AudioManager.Instance.PlayerOneShot(FMODEvents.Instance.UiClick[(int)UiClickEnum.Default], transform.position);

        foreach (var (r, amt) in _pendingRewards) GrantReward(r, amt);

        if (_aiCoreSystem.GetAiCores() <= 0)
        {
            _waitingForContinueAfterChance = false;
            _onFinished?.Invoke();
            _stack.Clear();
            Close();
            return;
        }

        _waitingForContinueAfterChance = false;
        _onFinished?.Invoke();
        _mapSystem.CompleteCurrentNode();
        _stack.Clear();
        Close();
    }

    /// <summary>
    /// Формирует текст строки изменения. Для Random-типа выбирает конкретный подтип и возвращает его в effectiveType.
    /// Если вывести нечего (неизвестный тип / нечего списывать) — возвращает null.
    /// </summary>
    private string FormatRewardLine(EventReward reward, int amount, out RewardType effectiveType)
    {
        effectiveType = reward.Type;

        if (reward.Type == RewardType.RandomResource || reward.Type == RewardType.RandomMaterial || reward.Type == RewardType.RandomComponent)
        {
            var resolved = ResolveRandomConcreteType(reward.Type, amount);
            if (resolved == null) return null;
            effectiveType = resolved.Value;
        }

        if (!RewardNameKey.TryGetValue(effectiveType, out int nameKey))
            return null;

        bool isPositive = amount >= 0;
        string color = isPositive ? Colors.HexGreen : Colors.HexWarningRed;
        int statusKey = isPositive ? 183 : 184;

        int displayAmount = Mathf.Abs(amount);

        return $"<color={color}>{Language.TextStatic[statusKey]} " + $"{Language.TextStatic[nameKey]}: {displayAmount}</color>";
    }


    private void GrantRandomFromKind(RewardType randKind, int amount)
    {
        var resolved = ResolveRandomConcreteType(randKind, amount);
        if (resolved == null) return; // нечего применять

        var concrete = resolved.Value;

        if (!TryMapRewardTypeToResourceEnum(concrete, out var resEnum)) return;

        int delta = amount;

        if (amount < 0)
        {
            float haveF = _mainResources.GetResourceAmountForEnum(resEnum);
            int haveInt = Mathf.FloorToInt(haveF);
            if (haveInt <= 0) return;
            if (-amount > haveInt) delta = -haveInt;
        }

        if (delta == 0) return;

        AudioManager.Instance.PlayerOneShot(delta > 0 ? FMODEvents.Instance.ReceivedResource : FMODEvents.Instance.LostResource, transform.position);
        _mainResources.ChangeResource(resEnum, delta);
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
                _mainResources.ChangeResource(ResourceEnum.DataFragment, amount);
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
            case RewardType.RandomResource:
                {
                    GrantRandomFromKind(RewardType.RandomResource, amount);
                    break;
                }
            case RewardType.RandomMaterial:
                {
                    GrantRandomFromKind(RewardType.RandomMaterial, amount);
                    break;
                }
            case RewardType.RandomComponent:
                {
                    GrantRandomFromKind(RewardType.RandomComponent, amount);
                    break;
                }

        }
    }


    private int GetCurrent(RewardType type)
    {
        return type switch
        {
            RewardType.Quants => _quantsSystem.GetQuants(),
            RewardType.AiCore => _aiCoreSystem.GetAiCores(),
            RewardType.Memory => (int)_mainResources.GetResourceAmountForEnum(ResourceEnum.DataFragment),
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
