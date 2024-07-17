using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Случайным образом вращает объект по оси Y кратно 90.
/// </summary>
public class RandomTileRotation : MonoBehaviour
{ 
    private void Awake()
    {
        RandomRotation();
    }

    private void RandomRotation()
    {
        var rnd = Random.Range(0, 4);
        var newRotation = rnd * 90;

        transform.rotation = Quaternion.Euler(0, newRotation, 0);
    }
}
