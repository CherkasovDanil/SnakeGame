using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Game.Food
{
    public class Food : MonoBehaviour
    {
        [HideInInspector]
        public UnityEvent OnTriggerEvent = new UnityEvent();

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Collider2D collider2D;

        private IMemoryPool _pool;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            OnTriggerEvent.Invoke();
        }
        

        public class Pool : MonoMemoryPool<Food>
        {
            protected override void OnSpawned(Food item)
            {
                item.gameObject.SetActive(true);
                item.spriteRenderer.DOFade(1f,0.8f).OnComplete(() =>
                {
                    item.collider2D.enabled = true;
                });
            }
            protected override void OnDespawned(Food item)
            {
               item.gameObject.SetActive(false);
               item.collider2D.enabled = false;
               item.spriteRenderer.DOFade(0f, 0f);
            }
        }
    }
}