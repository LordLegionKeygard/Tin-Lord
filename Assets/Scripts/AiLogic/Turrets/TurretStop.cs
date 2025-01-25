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
    private ITurretAttack _turretAttack;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _aIPath = GetComponent<AIPath>();
        _aIDestinationSetter = GetComponent<AIDestinationSetter>();
        _turretStateChanger = GetComponent<TurretStateChanger>();
        _turretBaseAttack = GetComponent<TurretBaseAttack>();
        _turretAttack = GetComponent<ITurretAttack>();
    }

    private void Start()
    {
        CustomEvents.OnBuildingDestroyed += CheckStopTurret;
        CustomEvents.OnMissionEnd += StopTurretLogic;
    }

    private void CheckStopTurret(int id)
    {
        if (_buildingLevels.CurrentTileObject().GetId() == id)
        {
            StopTurretLogic(MissionEndEnum.Nothing);
        }
    }

    public void StopTurretLogic(MissionEndEnum _)
    {
        if (_animator != null) _animator.enabled = false;
        _aIPath.enabled = false;
        _aIDestinationSetter.enabled = false;
        _turretStateChanger.enabled = false;
        if (_turretBaseAttack != null) _turretBaseAttack.AttackToggle(false);
        if (_turretAttack != null) _turretAttack.StopAttack();
    }

    private void OnDestroy()
    {
        CustomEvents.OnBuildingDestroyed -= CheckStopTurret;
        CustomEvents.OnMissionEnd -= StopTurretLogic;
    }
}
