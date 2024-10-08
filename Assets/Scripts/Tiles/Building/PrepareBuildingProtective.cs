using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareBuildingProtective : MonoBehaviour
{
    [SerializeField] private GameObject[] _walls;

    public void SetBuildingProtective(ProtectiveTypeEnum protectiveTypeEnum, int rotation)
    {
        foreach (var item in _walls) item.SetActive(false);

        _walls[(int)protectiveTypeEnum].SetActive(true);
        _walls[(int)protectiveTypeEnum].transform.rotation = Quaternion.Euler(0, rotation, 0);
    }
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
