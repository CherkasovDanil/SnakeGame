using System;
using System.Collections.Generic;
using DG.Tweening;
using Game.Food;
using Game.Grid;
using UniRx;
using UnityEngine;

namespace Game.Player
{
    public readonly struct OnCreateNewBodyPartMessage
    {
        public readonly SnakeMovePosition MovePosition;

        public OnCreateNewBodyPartMessage(SnakeMovePosition movePosition)
        {
            MovePosition = movePosition;
        }
    }
    
    public class SnakeMovement
    {
        private readonly Snake.Factory _snakeFactory;
        private readonly GridConfig _gridConfig;
        private readonly SnakeConfig _snakeConfig;
        private readonly SnakeController _snakeController;
        private readonly VectorDirectionController _vectorDirectionController;

        private Snake _player;
        private Direction _cashedDirection;
        private Vector3 _desirePosition;


        private SnakeMovePosition _previousSnakeMovePosition;
        private List<SnakeMovePosition> _snakeMovePositionList = new List<SnakeMovePosition>();
       
        private float _playerSpeed;
        private int _snakeBodySize = 1;

        public SnakeMovement(
            Snake.Factory snakeFactory,
            GridConfig gridConfig,
            SnakeConfig snakeConfig,
            SnakeController snakeController,
            VectorDirectionController vectorDirectionController)
        {
            _snakeFactory = snakeFactory;
            _gridConfig = gridConfig;
            _snakeConfig = snakeConfig;
            _snakeController = snakeController;
            _vectorDirectionController = vectorDirectionController;

            MessageBroker
                .Default
                .Receive<OnChangeDirectionMessage>()
                .Subscribe(message =>
                {
                    _cashedDirection = message.MessageDirection;
                });
            
            MessageBroker
                .Default
                .Receive<OnEatFoodMessage>()
                .Subscribe(message =>
                {
                    _snakeBodySize++;
                    
                    MessageBroker
                        .Default
                        .Publish(new OnCreateNewBodyPartMessage(_snakeMovePositionList[_snakeController.SnakeBodyParts.Count]));
                });

            SetPlayerMovementSettings();
            
            Moving();
        }


        private void Moving()
        {
            if (_snakeMovePositionList.Count > 0)
            {
                _previousSnakeMovePosition = _snakeMovePositionList[0];
            }
            
            var snakeMovePosition = new SnakeMovePosition(_previousSnakeMovePosition, _desirePosition, _cashedDirection);

            _snakeMovePositionList.Insert(0, snakeMovePosition);
            
           
            _desirePosition += _vectorDirectionController.GetVectorFromDirection(_cashedDirection);

            CheckBorder();

            _player.transform
                .DOMove(_desirePosition, _playerSpeed)
                .SetEase(Ease.Linear)
                .OnComplete(Moving);
            
            _player.transform.eulerAngles = new Vector3(0, 0, _vectorDirectionController.GetAngleFromDirection(_cashedDirection) - 90);
            
            UpdateSnakeBodyPartsPositions();

            if (_snakeMovePositionList.Count > _snakeBodySize)
            {
                _snakeMovePositionList.RemoveAt(_snakeMovePositionList.Count-1);
            }

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

        private void SetPlayerMovementSettings()
        {
            _player = _snakeFactory.Create();
            
            _player.transform.position = _snakeConfig.StartPosition;
            _desirePosition = _snakeConfig.StartPosition;

            _cashedDirection = _snakeConfig.StartDirection;

            _playerSpeed = _snakeConfig.MoveDuration;
        }
        
        private void UpdateSnakeBodyPartsPositions()
        {
            for (int i = 0; i < _snakeController.SnakeBodyParts.Count; i++)
            {
                SetSnakeMovePosition(_snakeMovePositionList[i], _playerSpeed, _snakeController.SnakeBodyParts[i]);
            }
        }
        
        private void SetSnakeMovePosition(SnakeMovePosition snakeMovePosition,float speed, SnakeBody snakeBody) 
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
            
            if (snakeMovePosition.GetGridPreviousPosition().x == _gridConfig.Width - 1 && snakeMovePosition.GetGridPosition().x == 1)
            {
                snakeBody.transform.DOKill();
               
                var pos = snakeBody.transform.position;
                pos.x = 0;
                snakeBody.transform.position = pos;
            }

            if (snakeMovePosition.GetGridPreviousPosition().y == _gridConfig.Height - 1 && snakeMovePosition.GetGridPosition().y == 1)
            {
                snakeBody.DOKill();
                    
                var pos = snakeBody.transform.position;
                pos.y = 0;
                snakeBody.transform.position = pos;
            }
            
            if (snakeMovePosition.GetGridPreviousPosition().x == 0 && snakeMovePosition.GetGridPosition().x == _gridConfig.Width - 2)
            {
                snakeBody.DOKill();
                    
                var pos = snakeBody.transform.position;
                pos.x = _gridConfig.Width-1;
                snakeBody.transform.position = pos;
            }
            
            if (snakeMovePosition.GetGridPreviousPosition().y == 0 && snakeMovePosition.GetGridPosition().y == _gridConfig.Height - 2)
            {
                snakeBody.DOKill();
                    
                var pos = snakeBody.transform.position;
                pos.y = _gridConfig.Height-1;;
                snakeBody.transform.position = pos;
            }

            snakeBody
                .transform
                .DOMove(snakeMovePosition.GetGridPosition(), speed)
                .SetEase(Ease.Linear);

            snakeBody.transform.eulerAngles = new Vector3(0, 0, angle);
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