using Game.UI.UIService;
using Zenject;

namespace Game.UI
{
    public class UIFrameworkInitController
    {
        public UIFrameworkInitController(IInstantiator instantiator)
        {
            var command = instantiator.Instantiate<InitUICommand>(); 
            command.Execute(); 
        }
    }
}