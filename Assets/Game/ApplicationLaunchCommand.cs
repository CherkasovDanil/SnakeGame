using Game.SceneObject;
using Game.UI;
using Game.UI.UIService.Interfaces;
using Zenject;

namespace Game
{
    public class ApplicationLaunchCommand : Command
    {
        private readonly IInstantiator _instantiator;
        private readonly IUIService _uiService;

        public ApplicationLaunchCommand(IInstantiator instantiator, IUIService uiService)
        {
            _instantiator = instantiator;
            _uiService = uiService;
        }
        public override void Execute()
        {
            _instantiator.InstantiatePrefabResourceForComponent<EnvironmentSceneObject>("Environment");
            
            _instantiator.InstantiatePrefabResourceForComponent<CameraSceneObject>("Main Camera");

            _uiService.Show<UIMainMenuWindow>();
            
            OnDone();
        }
    }
}