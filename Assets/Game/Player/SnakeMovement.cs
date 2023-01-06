using System;
using DG.Tweening;
using UniRx;
using UnityEngine;

namespace Game.Player
{
    public class SnakeMovement
    {
        private readonly Snake.Factory _snakeFactory;
        
        private Snake _player;
        private Direction _cashedDirection;
        private Direction _moveDirection;
        private Vector3 _moveDirectionVector;
        private Vector3 _desirePosition;

        public SnakeMovement(Snake.Factory snakeFactory)
        {
            MessageBroker
                .Default
                .Receive<OnChangeDirectionMessage>()
                .Subscribe(message =>
                {
                    _cashedDirection = message.MessageDirection;
                });
            
            _snakeFactory = snakeFactory;

            _player = _snakeFactory.Create();
            
            Moving();
        }


        private void Moving()
        {
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
                    Moving();
                });
            
            _player.transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(_moveDirectionVector) - 90);

            MessageBroker
                .Default
                .Publish(new OnStartTrackInputMessage(true));
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