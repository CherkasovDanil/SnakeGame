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

        private const float EffectDuration = 0.8f;
        
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
                item.spriteRenderer
                    .DOFade(1f, EffectDuration)
                    .OnComplete(() =>
                    {
                        item.collider2D.enabled = true;
                    });
            }
            protected override void OnDespawned(Food item)
            { 
                item.collider2D.enabled = false;
                item.spriteRenderer.gameObject.transform
                    .DOScale(0f, EffectDuration)
                    .OnComplete(() =>
                    {
                        item.gameObject.SetActive(false);
                        item.spriteRenderer.gameObject.transform.DOScale(1f, 0);
                        item.spriteRenderer.DOFade(0f, 0f);
                    });
            }
        }
    }
}