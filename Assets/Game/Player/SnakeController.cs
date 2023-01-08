using System.Collections.Generic;
using Game.Food;
using UniRx;
using UnityEngine;

namespace Game.Player
{
    public class SnakeController
    {
        public List<SnakeBody> SnakeBodyParts => _snakeBodyPartList;
        
        private readonly SnakeBody.Factory _factory;
        private readonly SnakeMovement _snakeMovement;

        private List<SnakeBody> _snakeBodyPartList = new List<SnakeBody>();

        public SnakeController(
            SnakeBody.Factory factory)
        {
            _factory = factory;

            MessageBroker
                .Default
                .Receive<OnCreateNewBodyPartMessage>()
                .Subscribe(_ =>
                {
                    CreateBodyPart(_.MovePosition);
                });
        }

        private void CreateBodyPart(SnakeMovePosition snakeMovePosition)
        {
            var body = _factory.Create(snakeMovePosition);
            _snakeBodyPartList.Add(body);
        }
    }
}