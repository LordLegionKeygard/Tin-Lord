using System.Collections.Generic;
using JetBrains.Annotations;
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
    Chance
}

[System.Serializable]
public class StepChoice
{
    public int ChoiseTextNumber;
    public ChoiceKind Kind = ChoiceKind.Standard;

    public StandardChoiceData Standard;
    public ChanceChoiceData Chance;
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
}
