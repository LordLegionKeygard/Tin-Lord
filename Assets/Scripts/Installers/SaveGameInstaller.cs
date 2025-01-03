using UnityEngine;
using Zenject;

public class SaveGameInstaller : MonoInstaller
{
    [SerializeField] private CommandCenterSaveGame _commandCenterSaveGame;
    [SerializeField] private WorldSaveGame _worldSaveGame;
    [SerializeField] private SceneLoader _sceneLoader;
    // [SerializeField] private WorldSaveSettings _worldSaveSettings;
    public override void InstallBindings()
    {
        Container.Bind<CommandCenterSaveGame>().FromComponentInNewPrefab(_commandCenterSaveGame).AsSingle();
        Container.Bind<WorldSaveGame>().FromComponentInNewPrefab(_worldSaveGame).AsSingle();
        Container.Bind<SceneLoader>().FromComponentInNewPrefab(_sceneLoader).AsSingle().NonLazy();
        // Container.Bind<WorldSaveSettings>().FromComponentInNewPrefab(_worldSaveSettings).AsSingle();
    }
}
