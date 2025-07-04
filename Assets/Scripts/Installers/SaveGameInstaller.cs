using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class SaveGameInstaller : MonoInstaller
{
    [SerializeField] private HangarSaveGame _hangarSaveGame;
    [SerializeField] private SpaceSaveGame _spaceSaveGame;
    [SerializeField] private MissionSaveGame _missionSaveGame;
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private SettingsSaveGame _settingsSaveGame;
    public override void InstallBindings()
    {
        Container.Bind<HangarSaveGame>().FromComponentInNewPrefab(_hangarSaveGame).AsSingle();
        Container.Bind<SpaceSaveGame>().FromComponentInNewPrefab(_spaceSaveGame).AsSingle();
        Container.Bind<MissionSaveGame>().FromComponentInNewPrefab(_missionSaveGame).AsSingle();
        Container.Bind<SceneLoader>().FromComponentInNewPrefab(_sceneLoader).AsSingle().NonLazy();
        Container.Bind<SettingsSaveGame>().FromComponentInNewPrefab(_settingsSaveGame).AsSingle();
    }
}
