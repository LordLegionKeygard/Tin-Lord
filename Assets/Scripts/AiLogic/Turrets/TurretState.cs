using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretState : MonoBehaviour
{
    public abstract TurretState Tick(TurretStateChanger stateChanger, BaseAnimator animator, AIDestinationSetter aiDestinationSetter, BaseAttacks attacks);

    public virtual void OnExit() { }
}
