using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTileRotation : MonoBehaviour
{
    private void Start()
    {
        RandomRotation();
    }
    
    private void RandomRotation()
    {
        var rnd = Random.Range(0, 3);

        transform.rotation = Quaternion.Euler(0, rnd * 90, 0);
    }
}
