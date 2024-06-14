using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareTileRiver : MonoBehaviour
{
    [SerializeField] private GameObject[] _rivers;

    public void SetRiver(int number, int rotation)
    {
        if (number == 0)
        {
            _rivers[0].SetActive(true);
            _rivers[1].SetActive(false);
        }
        else
        {
            _rivers[1].SetActive(true);
            _rivers[0].SetActive(false);
        }
        _rivers[number].transform.rotation = Quaternion.Euler(0, rotation, 0);
    }
}
