using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class AiStop : MonoBehaviour
{
    private AIPath _aIPath;
    private AIDestinationSetter _aiDestinationSetter;
    private EnemyAnimator _creatureAnimator;
    private EnemyStateChanger _enemyStateChanger;
    private EnemyHealth _creatureHealth;
    private BaseDamage _creatureDamage;


    private void Awake()
    {
        _aIPath = GetComponent<AIPath>();
        _aiDestinationSetter = GetComponent<AIDestinationSetter>();
        _creatureAnimator = GetComponent<EnemyAnimator>();
        _enemyStateChanger = GetComponent<EnemyStateChanger>();
        _creatureHealth = GetComponent<EnemyHealth>();
        _creatureDamage = GetComponent<BaseDamage>();
    }
    private void Start()
    {
        CustomEvents.OnBaseDestroy += DisableAllLogic;
    }

    private void DisableAllLogic()
    {
        _aIPath.enabled = false;
        _aiDestinationSetter.enabled = false;
        _creatureAnimator.enabled = false;
        _enemyStateChanger.enabled = false;
        _creatureHealth.enabled = false;
        _creatureDamage.enabled = false;
    }

    private void OnDestroy()
    {
        CustomEvents.OnBaseDestroy -= DisableAllLogic;
    }
}
