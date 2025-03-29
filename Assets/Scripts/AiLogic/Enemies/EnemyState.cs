using Pathfinding;
using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    public abstract EnemyState Tick(EnemyStateChanger stateChanger, BaseHealth health, BaseAnimator animator, AIDestinationSetter aiDestinationSetter, EnemyAttacks attacks, AIPath aiPath);
}
