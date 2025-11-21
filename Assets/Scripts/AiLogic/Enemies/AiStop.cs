using Pathfinding;
using Pathfinding.RVO;
using UnityEngine;

public class AiStop : MonoBehaviour
{
    private CharacterController _characterController;
    private AIPath _aiPath;
    private RVOController _rVOController;
    private AIDestinationSetter _aiDestinationSetter;
    private AlternativePath _alternativePath;
    private BaseAiStateChanger _stateChanger;
    private BaseHealth _health;
    private BaseDamage _damage;


    private void Awake()
    {
        _aiPath = GetComponent<AIPath>();
        _rVOController = GetComponent<RVOController>();
        _aiDestinationSetter = GetComponent<AIDestinationSetter>();
        _stateChanger = GetComponent<BaseAiStateChanger>();
        _health = GetComponent<BaseHealth>();
        _damage = GetComponent<BaseDamage>();
        _characterController = GetComponent<CharacterController>();
        _alternativePath = GetComponent<AlternativePath>();
    }
    private void Start()
    {
        CustomEvents.OnMissionEnd += DisableAllLogic;
    }

    public void DisableLogicOnDeath()
    {
        _characterController.enabled = false;
        _aiPath.enabled = false;
        if (_rVOController != null) _rVOController.enabled = false;
        if (_alternativePath != null) _alternativePath.enabled = false;
    }

    private void DisableAllLogic(MissionEndEnum _)
    {
        _aiPath.enabled = false;
        if (_rVOController != null) _rVOController.enabled = false;
        if (_alternativePath != null) _alternativePath.enabled = false;
        _aiDestinationSetter.enabled = false;
        _stateChanger.enabled = false;
        _health.enabled = false;
        _damage.enabled = false;
    }

    private void OnDestroy()
    {
        CustomEvents.OnMissionEnd -= DisableAllLogic;
    }
}
