using Pathfinding;
using Pathfinding.RVO;
using UnityEngine;

public class AiStop : MonoBehaviour
{
    private AIPath _aiPath;
    private RVOController _rVOController;
    private AIDestinationSetter _aiDestinationSetter;
    private EnemyStateChanger _enemyStateChanger;
    private EnemyHealth _creatureHealth;
    private BaseDamage _creatureDamage;


    private void Awake()
    {
        _aiPath = GetComponent<AIPath>();
        _rVOController = GetComponent<RVOController>();
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
        _aiPath.enabled = false;
        _rVOController.enabled = false;
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
