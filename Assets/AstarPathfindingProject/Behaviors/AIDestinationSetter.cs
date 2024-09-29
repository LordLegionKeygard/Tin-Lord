using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIDestinationSetter : VersionedMonoBehaviour
{
    public Transform CurrentTarget;
    private IAstarAI _ai;

    private void OnEnable()
    {
        _ai = GetComponent<IAstarAI>();
        if (_ai != null) _ai.onSearchPath += Update;
    }

    private void Update()
    {
        if (CurrentTarget != null && _ai != null)
        {
            _ai.destination = CurrentTarget.position;
        }
    }

    private void OnDisable()
    {
        if (_ai != null) _ai.onSearchPath -= Update;
    }
}

