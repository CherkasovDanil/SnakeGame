using System.Collections.Generic;
using DG.Tweening;
using Game.Grid;
using Game.Player;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Food
{
    public readonly struct OnEatFoodMessage { }
    
    public class FoodController
    {
        private readonly Food.Pool _pool;
        private readonly SnakeMovement _snakeMovement;
        private readonly GridConfig _gridConfig;
        
        private List<Food> _foods = new List<Food>();

        private Vector3 _foodPosition = new Vector3();
        private List<Vector3> _snakeMovePositionList = new List<Vector3>();

        public FoodController(
            Food.Pool pool,
            SnakeMovement snakeMovement,
            GridConfig gridConfig)
        { 
            _pool = pool;
            _snakeMovement = snakeMovement;
            _gridConfig = gridConfig;

            DOVirtual.DelayedCall(1.5f, () =>
            {
                SpawnFood();
            });
        }

        private void SpawnFood()
        {
            _snakeMovePositionList = null;
            _snakeMovePositionList = _snakeMovement.GetFullSnakeGridPositionList();

            RandomFoodPosition();

            if (_snakeMovePositionList.Contains(_foodPosition))
            {
                while (_snakeMovePositionList.Contains(_foodPosition))
                {
                    RandomFoodPosition();
                }
            }

            var food = _pool.Spawn(_foodPosition);

            food.OnTriggerEvent.AddListener(DisposeFood);
            _foods.Add(food);
        }

        private void DisposeFood()
        {
            MessageBroker
                .Default
                .Publish(new OnEatFoodMessage());
            
            var foo = _foods[0];
            foo.OnTriggerEvent.RemoveListener(DisposeFood);
            
            _pool.Despawn(foo);
            _foods.Remove(foo);

            DOVirtual.DelayedCall(1f, () =>
            {
                SpawnFood();
            });
        }

        private void RandomFoodPosition()
        {
            _foodPosition.x = Random.Range(0, _gridConfig.Width);
            _foodPosition.y = Random.Range(0, _gridConfig.Height);
        }
    }
}