using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Food
{
    public class FoodController : ITickable
    {
        private readonly Food.Pool _pool;
        readonly List<Food> _foos = new List<Food>();

        public FoodController(Food.Pool pool)
        { 
            _pool = pool;
        }

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("aaaaaa");
                _foos.Add(_pool.Spawn());
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log("bbbbbbbbb");
                
                var foo = _foos[0];
                _pool.Despawn(foo);
                _foos.Remove(foo);
            }
        }
    }
}