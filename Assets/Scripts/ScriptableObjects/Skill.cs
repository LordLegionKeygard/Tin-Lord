using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "New Skill", menuName = "TinLord/Skill")]
public class Skill : ScriptableObject
{
    public Sprite Icon;
    public int DescriptionLanguageNumber;
    public int CooldownTicks;
    public int RequiredOpenedMission;
    public InputActionReference InputAction;
}
