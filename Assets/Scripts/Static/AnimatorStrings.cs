using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorStrings : MonoBehaviour
{
    [Header("CharacterBuilding")]
    public static readonly int PickaxeMining = Animator.StringToHash("Pickaxe_Mining");
    public static readonly int ShovelDig = Animator.StringToHash("Shovel_Dig");
    public static readonly int AxeChop = Animator.StringToHash("Axe_Chop");
    public static readonly int Idle = Animator.StringToHash("Idle");
    public static readonly int HoldPlank = Animator.StringToHash("Hold_Plank");
    public static readonly int OilHandPump = Animator.StringToHash("Oil_Hand_Pump");
    public static readonly int WellHandleRotate = Animator.StringToHash("Well_Handle_Rotate");
    public static readonly int StoneCuttingTable = Animator.StringToHash("Stone_Cutting_Table");
    public static readonly int StoneCuttingWorkbrench = Animator.StringToHash("Stone_Cutting_Workbrench");
    public static readonly int StickMix = Animator.StringToHash("Stick_Mix");
    public static readonly int ComponentsCraft = Animator.StringToHash("Components_Craft");

    [Header("Ai")]
    public static readonly int Speed = Animator.StringToHash("Speed");
    public static readonly int TakeDamage = Animator.StringToHash("TakeDamage");
    public static readonly int Attack = Animator.StringToHash("Attack");


    [Header("Tile")]
    public static readonly int TileSpawn = Animator.StringToHash("TileSpawn");
    public static readonly int TileDestroy = Animator.StringToHash("TileDestroy");

    public static readonly Dictionary<CharacterWorkType, int> WorkTriggers = new Dictionary<CharacterWorkType, int>
    {
        { CharacterWorkType.PickaxeMining, PickaxeMining },
        { CharacterWorkType.ShovelDig, ShovelDig },
        { CharacterWorkType.AxeChop, AxeChop },
        { CharacterWorkType.HoldPlank, HoldPlank },
        { CharacterWorkType.OilHandPump, OilHandPump },
        { CharacterWorkType.WellHandleRotate, WellHandleRotate },
        { CharacterWorkType.StoneCuttingTable, StoneCuttingTable },
        { CharacterWorkType.StoneCuttingWorkbrench, StoneCuttingWorkbrench },
        { CharacterWorkType.StickMix, StickMix },
        { CharacterWorkType.ComponentsCraft, ComponentsCraft},
    };
}
