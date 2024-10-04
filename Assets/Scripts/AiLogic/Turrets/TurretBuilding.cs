using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBuilding : MonoBehaviour
{
    [SerializeField] private Building _building;

    public Building Building() => _building;
}
