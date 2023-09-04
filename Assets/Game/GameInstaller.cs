using Zenject;

namespace Game
{
    public class GameInstaller : Installer<GameInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<GameController>()
                .AsSingle()
                .NonLazy();
        }
    }
}