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
    Full = 105,
    Half = 55,
    Fourty = 40,
    Thirty = 30,
    Quarter = 25,
    Fifteen = 15,
    Ten = 10,
    Five = 5,
    One = 1,
    Zero = 0,
}
