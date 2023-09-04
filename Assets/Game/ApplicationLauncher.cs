using Game.UI.UIService;
using Zenject;

namespace Game
{
    public class ApplicationLauncher
    {
        public ApplicationLauncher(IInstantiator instantiator)
        {
            instantiator.Instantiate<ApplicationLaunchCommand>().Execute();
        }
    }
}