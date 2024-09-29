using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class AiStop : MonoBehaviour
{
    private CharacterController _characterController;
    private AIPath _aIPath;
    private AIDestinationSetter _aiDestinationSetter;
    private CreatureAnimator _creatureAnimator;
    private EnemyStateChanger _enemyStateChanger;
    private CreatureHealth _creatureHealth;
    private CreatureDamage _creatureDamage;


    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _aIPath = GetComponent<AIPath>();
        _aiDestinationSetter = GetComponent<AIDestinationSetter>();
        _creatureAnimator = GetComponent<CreatureAnimator>();
        _enemyStateChanger = GetComponent<EnemyStateChanger>();
        _creatureHealth = GetComponent<CreatureHealth>();
        _creatureDamage = GetComponent<CreatureDamage>();
    }
    private void Start()
    {
        CustomEvents.OnBaseDestroy += DisableAllLogic;
    }

    private void DisableAllLogic()
    {
        _characterController.enabled = false;
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
