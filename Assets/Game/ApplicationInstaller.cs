using Game.Food;
using Game.Grid;
using Game.Player;
using Game.UI;
using Zenject;

namespace Game
{
    public class ApplicationInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            GridInstaller.Install(Container);
            
            SnakeInstaller.Install(Container);
            
            FoodInstaller.Install(Container);
            
            UIFrameworkInstaller.Install(Container);
            
            Container
                .Bind<ApplicationLauncher>()
                .AsSingle()
                .NonLazy();
        }
    }
}