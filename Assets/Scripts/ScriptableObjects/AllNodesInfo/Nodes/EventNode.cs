using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventNode", menuName = "TinLord/Nodes/EventNode")]
public class EventNode : NodeData
{
   public List<EventStep> Steps = new();
}

[System.Serializable]
public class EventStep
{
   public int TextNumber;
   public List<EventChoice> Choices = new();
}

[System.Serializable]
public class EventChoice
{
   public int ChoiseTextNumber;
   public List<EventReward> Rewards = new();
   public int NextStepIndex = -1;
}

[System.Serializable]
public struct EventReward
{
   public RewardType Type;
   public int Amount;
}

public enum RewardType
{
   None = 0
}
