using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "TinLord/Dialogue")]
public class DialogueSequence : ScriptableObject
{
    public List<DialogueStep> Steps;

    public int GetRewardAmount(RewardCount rewardCount, bool isMin)
    {
        var amount = 0;
        switch (rewardCount.RewardAmountEnum)
        {
            case RewardAmountEnum.AiCoreLow:
                amount = WorldGameInfo.AiCoreLow;
                break;
            case RewardAmountEnum.AiCoreMedium:
                amount = WorldGameInfo.AiCoreMedium;
                break;
            case RewardAmountEnum.AiCoreLowOrMedium:
                amount = isMin ? WorldGameInfo.AiCoreLow : WorldGameInfo.AiCoreMedium;
                break;

            case RewardAmountEnum.QuantsLow:
                amount = isMin ? WorldGameInfo.QuantsLowMin : WorldGameInfo.QuantsLowMax;
                break;
            case RewardAmountEnum.QuantsMedium:
                amount = isMin ? WorldGameInfo.QuantsMediumMin : WorldGameInfo.QuantsMediumMax;
                break;
            case RewardAmountEnum.QuantsHight:
                amount = isMin ? WorldGameInfo.QuantsHightMin : WorldGameInfo.QuantsHightMax;
                break;

            case RewardAmountEnum.MemoryLow:
                amount = isMin ? WorldGameInfo.MemoryLowMin : WorldGameInfo.MemoryLowMax;
                break;
            case RewardAmountEnum.MemoryMedium:
                amount = isMin ? WorldGameInfo.MemoryMediumMin : WorldGameInfo.MemoryMediumMax;
                break;
            case RewardAmountEnum.MemoryHight:
                amount = isMin ? WorldGameInfo.MemoryHightMin : WorldGameInfo.MemoryHightMax;
                break;

            case RewardAmountEnum.ResourceLow:
                amount = isMin ? WorldGameInfo.ResourceLowMin : WorldGameInfo.ResourceLowMax;
                break;
            case RewardAmountEnum.ResourceMedium:
                amount = isMin ? WorldGameInfo.ResourceMediumMin : WorldGameInfo.ResourceMediumMax;
                break;
            case RewardAmountEnum.ResourceHight:
                amount = isMin ? WorldGameInfo.ResourceHightMin : WorldGameInfo.ResourceHightMax;
                break;

            case RewardAmountEnum.MaterialLow:
                amount = isMin ? WorldGameInfo.MaterialLowMin : WorldGameInfo.MaterialLowMax;
                break;
            case RewardAmountEnum.MaterialMedium:
                amount = isMin ? WorldGameInfo.MaterialMediumMin : WorldGameInfo.MaterialMediumMax;
                break;
            case RewardAmountEnum.MaterialHight:
                amount = isMin ? WorldGameInfo.MaterialHightMin : WorldGameInfo.MaterialHightMax;
                break;
        }

        return rewardCount.PlusMinusEnum == PlusMinusEnum.Plus ? amount : -amount;
    }
}

[System.Serializable]
public class RewardCount
{
    public RewardAmountEnum RewardAmountEnum;
    public PlusMinusEnum PlusMinusEnum;
}

[System.Serializable]
public enum PlusMinusEnum
{
    Plus = 0,
    Minus = 1,
}

[System.Serializable]
public enum RewardAmountEnum
{
    AiCoreLow = 0, // -1
    AiCoreMedium = 1, // -2
    AiCoreLowOrMedium = 14,

    QuantsLow = 2, // 10 - 30
    QuantsMedium = 3, // 30 -50
    QuantsHight = 4, // 50 - 100

    MemoryLow = 5, // 10 - 30
    MemoryMedium = 6, // 30 -50
    MemoryHight = 7, // 50 - 100

    ResourceLow = 8, // 5 - 10
    ResourceMedium = 9, // 10 -15
    ResourceHight = 10, // 15 - 20

    MaterialLow = 11, // 2 - 5
    MaterialMedium = 12, // 5 -10
    MaterialHight = 13, // 10 - 15
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

