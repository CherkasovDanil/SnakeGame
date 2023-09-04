using Game.UI.UIService.Interfaces;

namespace Game.UI.UIService
{
    public class InitUICommand : Command
    {
        private readonly IUIRoot _uIRoot;
        private readonly IUIService _uIService;

        public InitUICommand(IUIRoot uIRoot, IUIService uIService)
        {
            _uIRoot = uIRoot;
            _uIService = uIService;
        }
        
        public override void Execute()
        {
            _uIService.LoadWindows();
            
            _uIService.InitWindows(_uIRoot.PoolContainer);
            
            OnDone();
        }
    }
}