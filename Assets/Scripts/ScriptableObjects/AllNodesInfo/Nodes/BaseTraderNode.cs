

public class BaseTraderNode : NodeData
{
    public DialogueSequence Dialogue;
    public TraderKind TraderKind;
}

public enum TraderKind
{
    Resource = 0,
    Skill = 1,
}
