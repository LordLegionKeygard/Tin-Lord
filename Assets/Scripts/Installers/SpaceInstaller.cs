using UnityEngine;
using Zenject;
public class SpaceInstaller : MonoInstaller
{
    [SerializeField] private TutorialSystem _tutorialSystem;

    public override void InstallBindings()
    {
        Container.Bind<TutorialSystem>().FromInstance(_tutorialSystem).AsSingle();
    }
}
