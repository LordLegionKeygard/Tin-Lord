using UnityEngine;

public abstract class CityRobotState : MonoBehaviour
{
    public abstract CityRobotState Tick(CityRobotStateChanger stateChanger, BaseAnimator animator, AIDestinationSetter aiDestinationSetter);

    public virtual void OnExit() { }
}
