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

[System.Serializable]
public class StepChoice
{
    public int ChoiseTextNumber;
    public int NextStepIndex = -1;
    public List<EventReward> Rewards;
}

[System.Serializable]
public struct EventReward
{
    public RewardType Type;
    public int Amount;
}

public enum RewardType
{
    None = 0,
    AiCore = 1,
}

