using UnityEngine;
using Zenject;

public class MissionInstaller : MonoInstaller
{
    [SerializeField] private EnemyDefenceSystem _enemyDefenceSystem;
    [SerializeField] private TilesSystem _tilesSystem;
    [SerializeField] private MissionResources _missionResources;
    [SerializeField] private SelectTilePanel _selectTilePanel;
    [SerializeField] private HealthCanvas _healthCanvas;
    [SerializeField] private BulletsPool _bulletsPool;
    [SerializeField] private TakeDamageVFXPool _pool;
    [SerializeField] private LearnedBuildingsDataMission _learnedBuildingsDataMission;
    [SerializeField] private EndMissionSystem _endMissionSystem;
    [SerializeField] private AllSkills _allSkills;
    [SerializeField] private MissionQuantSystem _quantSystem;
    [SerializeField] private QuantPickupPool _quantPickupPool;
    [SerializeField] private DeathExplosionPool _deathExplosionPool;
    [SerializeField] private MissionHangarSystem _missionHangarSystem;
    [SerializeField] private TutorialSystem _tutorialSystem;
    [SerializeField] private TileViewSystem _tileViewSystem;

    public override void InstallBindings()
    {
        Container.Bind<TilesSystem>().FromInstance(_tilesSystem).AsSingle();
        Container.Bind<MissionResources>().FromInstance(_missionResources).AsSingle();
        Container.Bind<SelectTilePanel>().FromInstance(_selectTilePanel).AsSingle();
        Container.Bind<HealthCanvas>().FromInstance(_healthCanvas).AsSingle();
        Container.Bind<BulletsPool>().FromInstance(_bulletsPool).AsSingle();
        Container.Bind<TakeDamageVFXPool>().FromInstance(_pool).AsSingle();
        Container.Bind<LearnedBuildingsDataMission>().FromInstance(_learnedBuildingsDataMission).AsSingle();
        Container.Bind<EndMissionSystem>().FromInstance(_endMissionSystem).AsSingle();
        Container.Bind<AllSkills>().FromInstance(_allSkills).AsSingle();
        Container.Bind<EnemyDefenceSystem>().FromInstance(_enemyDefenceSystem).AsSingle();
        Container.Bind<MissionQuantSystem>().FromInstance(_quantSystem).AsSingle();
        Container.Bind<QuantPickupPool>().FromInstance(_quantPickupPool).AsSingle();
        Container.Bind<DeathExplosionPool>().FromInstance(_deathExplosionPool).AsSingle();
        Container.Bind<MissionHangarSystem>().FromInstance(_missionHangarSystem).AsSingle();
        Container.Bind<TutorialSystem>().FromInstance(_tutorialSystem).AsSingle();
        Container.Bind<TileViewSystem>().FromInstance(_tileViewSystem).AsSingle();
    }
}
