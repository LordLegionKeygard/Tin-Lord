using UnityEngine;

[CreateAssetMenu(fileName = "EventNode", menuName = "TinLord/Nodes/EventNode")]
public class EventNode : NodeData
{
   public DialogueSequence[] Dialogue;
   public bool EachCosmosForEachDialogue = false;
}

