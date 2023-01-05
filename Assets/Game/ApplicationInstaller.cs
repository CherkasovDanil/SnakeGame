using Game.Grid;
using Game.Player;
using Zenject;

namespace Game
{
    public class ApplicationInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            GridInstaller.Install(Container);
            
            SnakeInstaller.Install(Container);
            
            Container
                .Bind<ApplicationLauncher>()
                .AsSingle()
                .NonLazy();
        }
    }
}