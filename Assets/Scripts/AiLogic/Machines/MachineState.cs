using UnityEngine;

public abstract class MachineState : MonoBehaviour
{
    public abstract MachineState Tick(MachineStateChanger stateChanger, BaseHealth health, BaseAnimator animator, AIDestinationSetter aIDestinationSetter, MachineAttacks attacks, MachineSpeed playerSpeed);
}
