using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "TinLord/Dialogue")]
public class DialogueSequence : ScriptableObject
{
    public List<DialogueStep> Steps;

    public int GetRewardAmount(RewardCount rewardCount, RewardType rewardType, int act, bool isMin)
    {
        if (rewardCount == null || rewardType == RewardType.None) return 0;

        var amount = GetAmountByAct(rewardType, act, isMin);
        return rewardCount.PlusMinusEnum == PlusMinusEnum.Plus ? amount : -amount;
    }

    private int GetAmountByAct(RewardType rewardType, int act, bool isMin)
    {
        var tier = Mathf.Clamp(act, 0, 2); // 0=low, 1=medium, 2+=hight

        switch (rewardType)
        {
            case RewardType.AiCore:
                return WorldGameInfo.AiCoreLow;
            case RewardType.Quants:
                return GetQuantsAmount(tier, isMin);
            case RewardType.Memory:
                return GetMemoryAmount(tier, isMin);

            case RewardType.Wood:
            case RewardType.Stone:
            case RewardType.IronOre:
            case RewardType.CopperOre:
            case RewardType.Coal:
            case RewardType.Oil:
            case RewardType.Water:
            case RewardType.Sand:
            case RewardType.Electricity:
            case RewardType.RandomResource:
                return GetResourceAmount(tier, isMin);

            case RewardType.StoneBlock:
            case RewardType.IronIngot:
            case RewardType.SteelIngot:
            case RewardType.CopperPlate:
            case RewardType.Concrete:
            case RewardType.Steam:
            case RewardType.Glass:
            case RewardType.CopperWire:
            case RewardType.GearWheel:
            case RewardType.ElectronicCircuit:
            case RewardType.Processor:
            case RewardType.Engine:
            case RewardType.ElectricEngine:
            case RewardType.RandomMaterial:
            case RewardType.RandomComponent:
                return GetMaterialAmount(tier, isMin);

            default:
                return 0;
        }
    }

    private int GetQuantsAmount(int tier, bool isMin)
    {
        return tier == 0
            ? (isMin ? WorldGameInfo.QuantsLowMin : WorldGameInfo.QuantsLowMax)
            : tier == 1
                ? (isMin ? WorldGameInfo.QuantsMediumMin : WorldGameInfo.QuantsMediumMax)
                : (isMin ? WorldGameInfo.QuantsHightMin : WorldGameInfo.QuantsHightMax);
    }

    private int GetMemoryAmount(int tier, bool isMin)
    {
        return tier == 0
            ? (isMin ? WorldGameInfo.MemoryLowMin : WorldGameInfo.MemoryLowMax)
            : tier == 1
                ? (isMin ? WorldGameInfo.MemoryMediumMin : WorldGameInfo.MemoryMediumMax)
                : (isMin ? WorldGameInfo.MemoryHightMin : WorldGameInfo.MemoryHightMax);
    }

    private int GetResourceAmount(int tier, bool isMin)
    {
        return tier == 0
            ? (isMin ? WorldGameInfo.ResourceLowMin : WorldGameInfo.ResourceLowMax)
            : tier == 1
                ? (isMin ? WorldGameInfo.ResourceMediumMin : WorldGameInfo.ResourceMediumMax)
                : (isMin ? WorldGameInfo.ResourceHightMin : WorldGameInfo.ResourceHightMax);
    }

    private int GetMaterialAmount(int tier, bool isMin)
    {
        return tier == 0
            ? (isMin ? WorldGameInfo.MaterialLowMin : WorldGameInfo.MaterialLowMax)
            : tier == 1
                ? (isMin ? WorldGameInfo.MaterialMediumMin : WorldGameInfo.MaterialMediumMax)
                : (isMin ? WorldGameInfo.MaterialHightMin : WorldGameInfo.MaterialHightMax);
    }
}

[System.Serializable]
public class RewardCount
{
    public PlusMinusEnum PlusMinusEnum;
}

[System.Serializable]
public enum PlusMinusEnum
{
    Plus = 0,
    Minus = 1,
}

[System.Serializable]
public class DialogueStep
{
    public int TextNumber;
    public List<StepChoice> Choices;
}

public enum ChoiceKind
{
    Standard,
    Chance,
    Random
}

[System.Serializable]
public class StepChoice
{
    public int ChoiseTextNumber;
    public ChoiceKind Kind = ChoiceKind.Standard;

    public StandardChoiceData Standard;
    public ChanceChoiceData Chance;
    public RandomChoiceData Random;
}

[System.Serializable]
public class StandardChoiceData
{
    public int NextStepIndex = -1;
    public List<EventReward> Rewards;
    public ChoiceRequired ChoiceRequired;
}

[System.Serializable]
public class ChanceChoiceData
{
    public int SuccessTextNumber;
    public int FailureTextNumber;

    public List<EventReward> SuccessRewards;
    public List<EventReward> FailureRewards;
}

[System.Serializable]
public class RandomChoiceData
{
    public int NextStepIndex = -1;
    public List<RewardType> PossibleRewards;
    public RewardCount RewardCount;
}

[System.Serializable]
public struct ChoiceRequired
{
    public RewardType RequiredType;
    public int Amount;
}

[System.Serializable]
public struct EventReward
{
    public RewardType Type;
    public RewardCount RewardCount;
}

public enum RewardType
{
    None = 0,
    AiCore = 1,
    Quants = 2,
    Memory = 3,
    Wood = 4,
    Stone = 5,
    IronOre = 6,
    CopperOre = 7,
    Coal = 8,
    Oil = 9,
    Water = 10,
    Sand = 11,
    Electricity = 12,
    StoneBlock = 13,
    IronIngot = 14,
    SteelIngot = 15,
    CopperPlate = 16,
    Concrete = 17,
    Steam = 18,
    Glass = 19,
    CopperWire = 20,
    GearWheel = 21,
    ElectronicCircuit = 22,
    Processor = 23,
    Engine = 24,
    ElectricEngine = 25,
    BeamEnergy = 26,
    Shard = 27, //только для EndGame_Dialogue
    RandomResource = 28,
    RandomMaterial = 29,
    RandomComponent = 30
}

