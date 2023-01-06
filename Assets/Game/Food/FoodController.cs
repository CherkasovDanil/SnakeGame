
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Food
{
    public class FoodController
    {
        private readonly Food.Pool _pool;
        private readonly List<Food> _foods = new List<Food>();

        public FoodController(Food.Pool pool)
        { 
            _pool = pool;

            SpawnFood();
        }

        private void SpawnFood()
        {
            var food = _pool.Spawn();
            food.transform.position = new Vector3(Random.Range(0, 10), Random.Range(0, 8));
            food.OnTriggerEvent.AddListener(DispawnFood);
            _foods.Add(food);
        }

        private void DispawnFood()
        {
            var foo = _foods[0];
            _pool.Despawn(foo);
            _foods.Remove(foo);

            SpawnFood();
            
            
        }
    }
}