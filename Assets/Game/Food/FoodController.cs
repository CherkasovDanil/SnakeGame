using System.Collections.Generic;
using DG.Tweening;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Food
{
    public readonly struct OnEatFoodMessage { }
    
    public class FoodController
    {
        private readonly Food.Pool _pool;
        private readonly List<Food> _foods = new List<Food>();

        public FoodController(Food.Pool pool)
        { 
            _pool = pool;

            DOVirtual.DelayedCall(1.5f, () =>
            {
                SpawnFood();
            });
        }

        private void SpawnFood()
        {
            var food = _pool.Spawn();
            food.transform.position = new Vector3(Random.Range(0, 10), Random.Range(0, 8));
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
    }
}