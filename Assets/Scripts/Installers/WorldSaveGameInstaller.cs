using UnityEngine;
using Zenject;

public class WorldSaveGameInstaller : MonoInstaller
{
    [SerializeField] private WorldSaveGame _worldSaveGame;
    // [SerializeField] private WorldSaveSettings _worldSaveSettings;
    public override void InstallBindings()
    {
        Container.Bind<WorldSaveGame>().FromComponentInNewPrefab(_worldSaveGame).AsSingle();
        // Container.Bind<WorldSaveSettings>().FromComponentInNewPrefab(_worldSaveSettings).AsSingle();
    }
}
