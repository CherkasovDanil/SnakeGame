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

        private const float DurationForHeadRotation = 0.35f;
        private const float DurationForBodyPartsRotation = 0.25f;

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
        
        public List<Vector3> GetFullSnakeGridPositionList()
        {
            var gridPositionList = new List<Vector3>() {_desirePosition};
            foreach (SnakeMovePosition snakeMovePosition in _snakeMovePositionList)
            {
                gridPositionList.Add(snakeMovePosition.GetGridPosition());
            }
            return gridPositionList;
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
            
            _player.transform.DORotate(
                new Vector3(0, 0, _vectorDirectionController.GetAngleFromDirection(_cashedDirection) - 90), DurationForHeadRotation)
                .SetEase(Ease.Linear);
            
            UpdateSnakeBodyPartsPositions();

            if (_snakeMovePositionList.Count >= _snakeBodySize+1)
            {
                _snakeMovePositionList.RemoveAt(_snakeMovePositionList.Count - 1);
            }

            MessageBroker
                .Default
                .Publish(new OnStartTrackInputMessage(true));
        }
        
        private void CheckBorder()
        {
            _player.transform.DOKill();
            var pos = _player.transform.position;
            
            if (_desirePosition.x > _gridConfig.Width - 1)
            {
                pos.x = 0;

                _desirePosition.x = 1;
            }
            
            if (_desirePosition.y > _gridConfig.Height - 1)
            {
                pos.y = 0;

                _desirePosition.y = 1;
            }
            
            if (_desirePosition.x < 0)
            {
                pos.x = _gridConfig.Width - 1;

                _desirePosition.x = _gridConfig.Width - 2;
            }
            
            if (_desirePosition.y < 0)
            {
                pos.y = _gridConfig.Height - 1;
               
                _desirePosition.y = _gridConfig.Height - 2;
            }
            
            _player.transform.position = pos;
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
                SetSnakeMovePosition(_snakeMovePositionList[i], _snakeController.SnakeBodyParts[i]);
            }
        }
        
        private void SetSnakeMovePosition(SnakeMovePosition snakeMovePosition, SnakeBody snakeBody) 
        {
            snakeBody.transform.DOKill();
            
            var pos = snakeBody.transform.position;
            
            if (snakeMovePosition.GetGridPreviousPosition().x == _gridConfig.Width - 1 && snakeMovePosition.GetGridPosition().x == 1)
            {
                pos.x = 0;
            }

            else if (snakeMovePosition.GetGridPreviousPosition().y == _gridConfig.Height - 1 && snakeMovePosition.GetGridPosition().y == 1)
            {
                pos.y = 0;
            }
            
            else if (snakeMovePosition.GetGridPreviousPosition().x == 0 && snakeMovePosition.GetGridPosition().x == _gridConfig.Width - 2)
            {
                pos.x = _gridConfig.Width-1;
            }
            
            else if (snakeMovePosition.GetGridPreviousPosition().y == 0 && snakeMovePosition.GetGridPosition().y == _gridConfig.Height - 2)
            {
               pos.y = _gridConfig.Height-1;
            }
            
            snakeBody.transform.position = pos;

            snakeBody
                .transform
                .DOMove(snakeMovePosition.GetGridPosition(), _playerSpeed)
                .SetEase(Ease.Linear);
            
            snakeBody
                .transform
                .DORotate( new Vector3(0, 0, _vectorDirectionController.GetAngleForTurnBodyFromSnakeMovePosition(snakeMovePosition)), DurationForBodyPartsRotation)
                .SetEase(Ease.Linear);
        }
    }
}