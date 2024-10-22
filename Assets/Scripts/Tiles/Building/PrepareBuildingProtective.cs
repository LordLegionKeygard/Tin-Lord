using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareBuildingProtective : MonoBehaviour
{
    [SerializeField] private WallsWrapper[] _wallsWrappers;

    public void SetBuildingProtective(ProtectiveTypeEnum protectiveTypeEnum, int rotation, int level)
    {
        for (int i = 0; i < _wallsWrappers.Length; i++)
        {
            for (int k = 0; k < _wallsWrappers[i].Models.Length; k++)
            {
                if (_wallsWrappers[i].Models[k] != null) _wallsWrappers[i].Models[k].SetActive(false);
            }
        }

        var gameObject = _wallsWrappers[(int)protectiveTypeEnum].Models[level - 1];

        gameObject.SetActive(true);
        gameObject.transform.rotation = Quaternion.Euler(0, rotation, 0);
    }
}

[System.Serializable]
public class WallsWrapper
{
    public ProtectiveTypeEnum ProtectiveTypeEnum;
    public GameObject[] Models;
}

public enum ProtectiveTypeEnum
{
    None = -1,
    WallForward = 0,
    WallTurn = 1,
    WallT = 2,
    WallCross = 3,
    Gate = 4,
}
