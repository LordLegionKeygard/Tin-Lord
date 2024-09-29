using UnityEngine;
using Zenject;

public class HealthCanvasInstaller : MonoInstaller
{
    [SerializeField] private HealthCanvas _healthCanvas;

    public override void InstallBindings()
    {
        Container.Bind<HealthCanvas>().FromInstance(_healthCanvas).AsSingle();
    }
}
