using UnityEngine;

public abstract class PlayerState : MonoBehaviour
{
    public abstract PlayerState Tick(PlayerStateChanger stateChanger, BaseHealth health, BaseAnimator animator, AIDestinationSetter aIDestinationSetter, PlayerAttacks attacks);
}
