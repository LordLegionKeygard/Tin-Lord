using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeSetRotation : MonoBehaviour
{
    private TileRiver _tileRiver;
    private void Start()
    {
        _tileRiver = gameObject.transform.parent.parent.GetComponent<TileRiver>();
        transform.rotation = Quaternion.Euler(0, _tileRiver.RiverRotation(), 0);
    }
}
