using Game.Food;

namespace Game.Grid
{
    public class GridCreateCommand: Command 
    {
        private readonly GridViewProtocol _protocol;
        private readonly GridFactory _gridFactory;

        public GridCreateCommand(
            GridViewProtocol protocol,
            GridFactory gridFactory)
        {
            _protocol = protocol;
            _gridFactory = gridFactory;
        }
        public override void Execute()
        {
            _gridFactory.Create(_protocol);
            OnDone();
        }
    }
}