using UnityEngine;

public class AiStop : MonoBehaviour
{
    private AIDestinationSetter _aiDestinationSetter;
    private EnemyStateChanger _enemyStateChanger;
    private EnemyHealth _creatureHealth;
    private BaseDamage _creatureDamage;


    private void Awake()
    {
        _aiDestinationSetter = GetComponent<AIDestinationSetter>();
        _enemyStateChanger = GetComponent<EnemyStateChanger>();
        _creatureHealth = GetComponent<EnemyHealth>();
        _creatureDamage = GetComponent<BaseDamage>();
    }
    private void Start()
    {
        CustomEvents.OnMissionEnd += DisableAllLogic;
    }

    private void DisableAllLogic(MissionEndEnum _)
    {
        _aiDestinationSetter.enabled = false;
        _enemyStateChanger.enabled = false;
        _creatureHealth.enabled = false;
        _creatureDamage.enabled = false;
    }

    private void OnDestroy()
    {
        CustomEvents.OnMissionEnd -= DisableAllLogic;
    }
}
