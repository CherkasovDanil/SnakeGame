using System;
using DG.Tweening;
using Game.Grid;
using UniRx;
using UnityEngine;

namespace Game.Player
{
    public class SnakeMovement
    {
        private readonly Snake.Factory _snakeFactory;
        private readonly GridConfig _gridConfig;

        private Snake _player;
        private Direction _cashedDirection;
        private Direction _moveDirection;
        private Vector3 _moveDirectionVector;
        private Vector3 _desirePosition;

        public SnakeMovement(
            Snake.Factory snakeFactory,
            GridConfig gridConfig)
        {
            MessageBroker
                .Default
                .Receive<OnChangeDirectionMessage>()
                .Subscribe(message =>
                {
                    _cashedDirection = message.MessageDirection;
                });
            
            _snakeFactory = snakeFactory;
            _gridConfig = gridConfig;

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

            CheckBorder();

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
        
        private void CheckBorder()
        {
            if (_desirePosition.x > _gridConfig.Width - 1)
            {
                _player.transform.DOKill();

                var pos = _player.transform.position;
                pos.x = 0;
                _player.transform.position = pos;
                
                _desirePosition.x = 1;
            }
            if (_desirePosition.y > _gridConfig.Height - 1)
            {
                _player.transform.DOKill();

                var pos = _player.transform.position;
                pos.y = 0;
                _player.transform.position = pos;
                
                _desirePosition.y = 1;
            }
            
            if (_desirePosition.x < 0)
            {
                _player.transform.DOKill();

                var pos = _player.transform.position;
                pos.x = _gridConfig.Width - 1;
                _player.transform.position = pos;

                _desirePosition.x = _gridConfig.Width - 2;
            }
            
            if (_desirePosition.y < 0)
            {
                _player.transform.DOKill();

                var pos = _player.transform.position;
                pos.y = _gridConfig.Height - 1;
                _player.transform.position = pos;
                
                _desirePosition.y = _gridConfig.Height - 2;
            }
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