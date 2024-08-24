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

    [Header("Tile")]
    public static readonly int TileSpawn = Animator.StringToHash("TileSpawn");

}
