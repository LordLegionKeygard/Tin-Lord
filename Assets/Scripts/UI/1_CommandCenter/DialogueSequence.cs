using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "TinLord/Dialogue")]
public class DialogueSequence : ScriptableObject
{
    public List<DialogueStep> Steps;
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
    public int MinAmount;
    public int MaxAmount;
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
    public int MinAmount;
    public int MaxAmount;
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
}
