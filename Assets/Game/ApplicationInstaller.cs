using Game.Grid;
using Zenject;

namespace Game
{
    public class ApplicationInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            GridInstaller.Install(Container);
            
            Container
                .Bind<ApplicationLauncher>()
                .AsSingle()
                .NonLazy();
        }
    }
}