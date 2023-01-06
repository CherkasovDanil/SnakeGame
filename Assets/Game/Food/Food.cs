using System;
using UnityEngine;
using Zenject;

namespace Game.Food
{
    public class Food : MonoBehaviour
    {
        private IMemoryPool _pool;
       

        public class Pool : MonoMemoryPool<Food>
        {
            protected override void OnSpawned(Food item)
            {
                Debug.Log("spawn");
                item.gameObject.SetActive(true);
            }
            protected override void OnDespawned(Food item)
            {
                Debug.Log("despawn");
                item.gameObject.SetActive(false);
            }
        }
    }
}