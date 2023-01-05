using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class SnakeMovement : ITickable
    {
        private readonly Snake.Factory _snakeFactory;
        
        private Snake _player;
        private Direction _cashedDirection;
        private Direction _moveDirection;
        private Vector3 _moveDirectionVector;
        private Vector3 _desirePosition;

        private bool _isMoving;
        
        public SnakeMovement(Snake.Factory snakeFactory)
        {
            _snakeFactory = snakeFactory;

            _player = _snakeFactory.Create();
            Moving();
        }


        public void Tick()
        {
            Input();
        }


        private void Input()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.LeftArrow) && _isMoving == false)
            {
                _isMoving = true;
                
                _cashedDirection = _cashedDirection switch {
                    Direction.Left => Direction.Down,
                    Direction.Up => Direction.Left,
                    Direction.Right => Direction.Up,
                    Direction.Down => Direction.Right,
                    _ => _cashedDirection
                };
            }

            if (UnityEngine.Input.GetKeyDown(KeyCode.RightArrow) && _isMoving == false)
            {
                _isMoving = true;
                
                _cashedDirection = _cashedDirection switch
                {
                    Direction.Left => Direction.Up,
                    Direction.Up => Direction.Right,
                    Direction.Right => Direction.Down,
                    Direction.Down => Direction.Left,
                    _ => _cashedDirection
                };
            }
        }

        private void Moving()
        {
            _isMoving = true;

            switch (_cashedDirection)
            {
                case Direction.Left:
                    _moveDirectionVector = Vector3.left;
                    break;
                case Direction.Right:
                    _moveDirectionVector = Vector3.right;
                    break;
                case Direction.Up:
                    _moveDirectionVector = Vector3.up;
                    break;
                case Direction.Down:
                    _moveDirectionVector = Vector3.down;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
           
            _desirePosition += _moveDirectionVector;


            _player.transform
                .DOMove(_desirePosition, 0.5f)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
            {
                _isMoving = false;
                Moving();
            });
            
            _player.transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(_moveDirectionVector) - 90);

            _isMoving = false;
        }
        
        private float GetAngleFromVector(Vector3 dir)
        {
            float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if (n < 0)
                n += 360;
            return n;
        }
    }
}