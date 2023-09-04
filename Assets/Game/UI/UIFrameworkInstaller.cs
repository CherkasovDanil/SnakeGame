using Game.UI.UIService.Interfaces;
using Game.UI.UIService.Realization;
using Zenject;

namespace Game.UI
{
    public class UIFrameworkInstaller : Installer<UIFrameworkInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IUIRoot>()
                .To<UIRoot>()
                .FromComponentInNewPrefabResource("UIRoot")
                .AsSingle()
                .NonLazy();

            Container
                .Bind<IUIService>()
                .To<UIService.Realization.UIService>()
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<UIFrameworkInitController>()
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<UIMainWindowController>()
                .AsSingle()
                .NonLazy();
        }
    }
}