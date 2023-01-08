using UnityEngine;

namespace Game.Player
{
    public class SnakeMovePosition
    {
        private readonly SnakeMovePosition _previousSnakeMovePosition;
        private readonly Vector3 _movePosition;
        private readonly Direction _direction;

        public SnakeMovePosition(
            SnakeMovePosition previousSnakeMovePosition,
            Vector3 movePosition,
            Direction direction)
        {
            _previousSnakeMovePosition = previousSnakeMovePosition;
            _movePosition = movePosition;
            _direction = direction;
        }


        public Vector3 GetGridPosition() 
        {
            return _movePosition;
        }
        public Vector3 GetGridPreviousPosition() 
        {
            return _previousSnakeMovePosition.GetGridPosition();
        }

        public Direction GetDirection() 
        {
            return _direction;
        }

        public Direction GetPreviousDirection() 
        {
            if (_previousSnakeMovePosition == null) 
            {
                return Direction.Right;
            } 
            else 
            {
                return _previousSnakeMovePosition._direction;
            }
        }
    }
}