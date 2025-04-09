using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "TinLord/Skill")]
public class Skill : ScriptableObject
{
    [Header("Main")]
    public Sprite Icon;
    public int CooldownTicks;
    public int RequiredOpenedMission;
    public string ActionText;
    [Header("Description")]
    public int DescriptionLanguageNumber;
    public int MaxWidth;
    public int Padding;


}
