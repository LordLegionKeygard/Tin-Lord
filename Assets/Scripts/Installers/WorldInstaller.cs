using UnityEngine;
using Zenject;

public class WorldInstaller : MonoInstaller
{
    [SerializeField] private EnemyDefenceSystem _enemyDefenceSystem;
    [SerializeField] private TilesSystem _tilesSystem;
    [SerializeField] private PlayerResources _playerResources;
    [SerializeField] private SelectTilePanel _selectTilePanel;
    [SerializeField] private HealthCanvas _healthCanvas;
    [SerializeField] private BulletsPool _bulletsPool;
    [SerializeField] private TakeDamageVFXPool _pool;
    [SerializeField] private LearnedBuildingsDataWorld _learnedBuildingsDataWorld;
    [SerializeField] private EndMissionSystem _endMissionSystem;
    [SerializeField] private AllSkills _allSkills;

    public override void InstallBindings()
    {
        Container.Bind<TilesSystem>().FromInstance(_tilesSystem).AsSingle();
        Container.Bind<PlayerResources>().FromInstance(_playerResources).AsSingle();
        Container.Bind<SelectTilePanel>().FromInstance(_selectTilePanel).AsSingle();
        Container.Bind<HealthCanvas>().FromInstance(_healthCanvas).AsSingle();
        Container.Bind<BulletsPool>().FromInstance(_bulletsPool).AsSingle();
        Container.Bind<TakeDamageVFXPool>().FromInstance(_pool).AsSingle();
        Container.Bind<LearnedBuildingsDataWorld>().FromInstance(_learnedBuildingsDataWorld).AsSingle();
        Container.Bind<EndMissionSystem>().FromInstance(_endMissionSystem).AsSingle();
        Container.Bind<AllSkills>().FromInstance(_allSkills).AsSingle();
        Container.Bind<EnemyDefenceSystem>().FromInstance(_enemyDefenceSystem).AsSingle();
    }
}
