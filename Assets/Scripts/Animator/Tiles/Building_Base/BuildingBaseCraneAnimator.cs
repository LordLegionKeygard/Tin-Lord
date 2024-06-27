using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBaseCraneAnimator : MonoBehaviour
{
    [SerializeField] private Transform _woodenPallet;
    [SerializeField] private Transform _bigCan;
    [SerializeField] private Transform _hook;
    [SerializeField] private Transform _crane;

    public void WoodenPalletTakeToggle(string isTake)
    {
        if (isTake == "isTake")
        {
            _woodenPallet.SetParent(_hook);
        }
        else _woodenPallet.SetParent(_crane);
    }

    public void BigCanTakeToggle(string isTake)
    {
        if (isTake == "isTake")
        {
            _bigCan.SetParent(_hook);
        }
        else _bigCan.SetParent(_crane);
    }
}
