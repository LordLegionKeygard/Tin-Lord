using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    public abstract EnemyState Tick(EnemyStateChanger enemyStateChanger, CreatureHealth creatureHealth, CreatureAnimator enemyAnimator, AIDestinationSetter aiDestinationSetter, BaseHealth baseHealth, CreatureAttacks creatureAttacks);
}
