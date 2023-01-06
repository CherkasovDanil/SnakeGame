using Game.SceneObject;
using Zenject;

namespace Game
{
    public class ApplicationLaunchCommand : Command
    {
        private readonly IInstantiator _instantiator;

        public ApplicationLaunchCommand(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }
        public override void Execute()
        {
            _instantiator.InstantiatePrefabResourceForComponent<EnvironmentSceneObject>("Environment");
            
            _instantiator.InstantiatePrefabResourceForComponent<CameraSceneObject>("Main Camera");
            
            OnDone();
        }
    }
}