using UnityEngine;
using Zenject;
public class SelectTilePanelInstaller : MonoInstaller
{
    [SerializeField] private SelectTilePanel _selectTilePanel;
    public override void InstallBindings()
    {
        Container.Bind<SelectTilePanel>().FromInstance(_selectTilePanel).AsSingle();
    }
}
