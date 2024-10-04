using UnityEngine;
using Zenject;

public class BulletsPoolInstaller : MonoInstaller
{
    [SerializeField] private BulletsPool _bulletsPool;

    public override void InstallBindings()
    {
        Container.Bind<BulletsPool>().FromInstance(_bulletsPool).AsSingle();
    }
}
