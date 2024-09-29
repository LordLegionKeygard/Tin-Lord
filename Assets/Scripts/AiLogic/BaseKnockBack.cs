using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseKnockBack : MonoBehaviour
{
    public int MaxKnockbackPoints = 100;
    public int MinimumKnockbackPoints = 30;
    public int CurrentKnockBackPoints;

    public virtual void Awake()
    {

    }

    public void TakeKnockbackPoints(KnockBackType knockBackType)
    {
        CurrentKnockBackPoints += (int)knockBackType;
        CheckKnockBack();
    }

    public virtual void CheckKnockBack()
    {

    }

    public void ResetKnockbackPoints()
    {
        CurrentKnockBackPoints = 0;
    }
}

[System.Serializable]
public enum KnockBackType
{
    Full = 105, //Traps, BallistaBolt, GolemBlock, BerserkJump
    Half_ArrowShield = 55, //Arrows
    Fourty_GreatWeapon = 40, // GreatSword, GreatHammer, GreatAxe, Fireball, MagicAoe
    Thirty_SpearScytheBite = 30, // Spear, Scythe 
    Quarter_SwordAxeHammerStaffPunch = 25, //Sword,Axe,Hammer
    Fifteen_Knuckles = 15, //Knuckles
    Ten_FistDagger = 10, //Fist,Dagger,
    Five = 5,
    One = 1, //TriggerStay
    Zero = 0, //Dots, BloodSkills
}
