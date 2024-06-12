using UnityEngine;
using Zenject;

public class TilesSystemInstaller : MonoInstaller
{
    [SerializeField] private TilesSystem _tilesSystem;
    public override void InstallBindings()
    {
        Container.Bind<TilesSystem>().FromInstance(_tilesSystem).AsSingle();
    }
}
