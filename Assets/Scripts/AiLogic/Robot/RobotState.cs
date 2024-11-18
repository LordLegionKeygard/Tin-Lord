using UnityEngine;

public abstract class RobotState : MonoBehaviour
{
    public abstract RobotState Tick(RobotStateChanger stateChanger, BaseHealth health, BaseAnimator animator, AIDestinationSetter aIDestinationSetter, RobotAttacks attacks, RobotSpeed playerSpeed);
}
