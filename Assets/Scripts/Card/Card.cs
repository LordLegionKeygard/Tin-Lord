using UnityEngine;

public class Card : ScriptableObject
{
    public int NameLanguageNumber;
    public Sprite Icon;
    public CardKind Kind;
    public int Id;
}

public enum CardKind
{ 
    Tile = 0,
    Tactic = 1 
}
