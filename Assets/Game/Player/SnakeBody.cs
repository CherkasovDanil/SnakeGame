using System;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class SnakeBody: MonoBehaviour, IDisposable
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        [Inject]
        private void Constructor(SnakeMovePosition snakeMovePosition,
            VectorDirectionController vectorDirectionController)
        {
            transform.position = snakeMovePosition.GetGridPosition();
            transform.eulerAngles = new Vector3(0, 0, vectorDirectionController.GetAngleFromDirection(snakeMovePosition.GetDirection()) - 90);
            spriteRenderer.sortingOrder = 5;
            
            transform.DOScale(1, 0.5f);
        }
        
        public void Dispose()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            Debug.Log("EatHimself");
        }

        public class Factory : PlaceholderFactory<SnakeMovePosition,SnakeBody>
        { }
    }
}