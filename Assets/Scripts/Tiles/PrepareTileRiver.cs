using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareTileRiver : MonoBehaviour
{
    [SerializeField] private GameObject[] _rivers;

    public void SetRiver(RiverTypeEnum riverTypeEnum, int rotation)
    {
        foreach (var item in _rivers) item.SetActive(false);

        _rivers[(int)riverTypeEnum].SetActive(true);
        _rivers[(int)riverTypeEnum].transform.rotation = Quaternion.Euler(0, rotation, 0);
    }
}

public enum RiverTypeEnum
{
    RiverForward = 0,
    RiverTurn = 1,
    Lake = 2,
    LakeExit = 3,
    RiverEnd = 4,
    RiverBridge = 5,
    RiverBridgeEnd = 6,
}
