// EventNode.cs
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventNode", menuName = "TinLord/Nodes/EventNode")]
public class EventNode : NodeData
{
   public EventStep RootStep;
}

[System.Serializable]
public class EventStep
{
   [TextArea(3, 18)]
   public string[] Text;

   public List<EventChoice> Choices = new();
}

[System.Serializable]
public class EventChoice
{
   public string[] ChoiseText;
   public bool IsFinal;
   
   public List<EventReward> Rewards = new();
   public EventStep NextStep;
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
}
