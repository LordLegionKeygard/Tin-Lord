using Pathfinding;
using UnityEngine;

public class TurretStop : MonoBehaviour
{
    [SerializeField] private BuildingLevels _buildingLevels;
    private AIPath _aIPath;
    private AIDestinationSetter _aIDestinationSetter;
    private TurretStateChanger _turretStateChanger;
    private Animator _animator;
    private TurretBaseAttack _turretBaseAttack;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _aIPath = GetComponent<AIPath>();
        _aIDestinationSetter = GetComponent<AIDestinationSetter>();
        _turretStateChanger = GetComponent<TurretStateChanger>();
        _turretBaseAttack = GetComponent<TurretBaseAttack>();
    }

    private void Start()
    {
        CustomEvents.OnBuildingDestroyed += CheckStopTurret;
    }

    private void CheckStopTurret(int id)
    {
        if (_buildingLevels.CurrentTileObject().GetId() == id)
        {
            StopTurretLogic();
        }
    }

    public void StopTurretLogic()
    {
        if (_animator != null) _animator.enabled = false;
        _aIPath.enabled = false;
        _aIDestinationSetter.enabled = false;
        _turretStateChanger.enabled = false;
        if(_turretBaseAttack != null) _turretBaseAttack.AttackToggle(false);
    }

    private void OnDestroy()
    {
        CustomEvents.OnBuildingDestroyed -= CheckStopTurret;
    }
}
