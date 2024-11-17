using UnityEngine;
using Zenject;


public class TakeDamageVFXPoolInstaller : MonoInstaller
{
    [SerializeField] private TakeDamageVFXPool _pool;

    public override void InstallBindings()
    {
        Container.Bind<TakeDamageVFXPool>().FromInstance(_pool).AsSingle();
    }
}
