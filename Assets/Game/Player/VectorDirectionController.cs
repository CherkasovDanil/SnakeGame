using System;
using UnityEngine;

namespace Game.Player
{
    public class VectorDirectionController
    {
        private Vector3 _vector3 = Vector3.zero;

        public Vector3 GetVectorFromDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    _vector3 = Vector3.left;
                    break;
                case Direction.Right:
                    _vector3 = Vector3.right;
                    break;
                case Direction.Up:
                    _vector3 = Vector3.up;
                    break;
                case Direction.Down:
                    _vector3 = Vector3.down;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return _vector3;
        }

        public float GetAngleFromDirection(Direction direction)
        {
            _vector3 = GetVectorFromDirection(direction);
            
            float n = Mathf.Atan2(_vector3.y, _vector3.x) * Mathf.Rad2Deg;
            if (n < 0)
                n += 360;
            return n;
        }

        public float GetAngleForTurnBodyFromSnakeMovePosition(SnakeMovePosition snakeMovePosition)
        {
            float angle = snakeMovePosition.GetDirection() 
                switch
                {
                    Direction.Up => snakeMovePosition.GetPreviousDirection() switch
                    {
                        Direction.Left => 0 + 45, Direction.Right => 0 - 45, _ => 0
                    },
                    Direction.Down => snakeMovePosition.GetPreviousDirection() switch
                    {
                        Direction.Left => 180 - 45, Direction.Right => 180 + 45, _ => 180
                    },
                    Direction.Left => snakeMovePosition.GetPreviousDirection() switch
                    {
                        Direction.Down => 180 - 45, Direction.Up => 45, _ => +90
                    },
                    Direction.Right => snakeMovePosition.GetPreviousDirection() switch
                    {
                        Direction.Down => 180 + 45, Direction.Up => -45, _ => -90
                    },
                    _ => snakeMovePosition.GetPreviousDirection() switch
                    {
                        Direction.Left => 0 + 45, Direction.Right => 0 - 45, _ => 0
                    }
                };
            return angle;
        }
    }
}