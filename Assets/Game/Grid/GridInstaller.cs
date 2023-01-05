using Zenject;

namespace Game.Grid
{
    public class GridInstaller : Installer<GridInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<GridConfig>()
                .FromScriptableObjectResource("GridConfig")
                .AsSingle();
            
            Container
                .BindMemoryPool<GridCell, GridCell.Pool>()
                .FromComponentInNewPrefabResource("GridCell")
                .UnderTransformGroup("Grid");
            
            Container
                .Bind<GridController>()
                .AsSingle()
                .NonLazy();
        }
    }
}