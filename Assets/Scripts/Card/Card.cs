using UnityEngine;

public class Card : ScriptableObject
{
    public string[] Name; // 0 eng, 1 rus
    public Sprite Icon;
    public CardKind Kind;
    public int Id;
}

public enum CardKind
{ 
    Tile = 0,
    Tactic = 1 
}
