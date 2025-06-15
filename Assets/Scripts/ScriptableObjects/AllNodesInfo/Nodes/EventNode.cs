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
   [TextArea(3, 8)]
   public string Text;

   public List<EventChoice> Choices = new();
}

[System.Serializable]
public class EventChoice
{
   public string Caption;
   public List<EventReward> Rewards = new();

   /// <summary>
   /// Следующий шаг для этого варианта.  
   /// null ⇒ событие завершается.
   /// </summary>
   public EventStep NextStep;
}

[System.Serializable]
public struct EventReward
{
   public RewardType Type;
   public int Amount;
   public string Id;
}

public enum RewardType
{
   None = 0,
}
