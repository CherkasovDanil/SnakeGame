using System;
using UnityEngine;
using Zenject;

namespace Game.Grid
{
    public class GridCell : MonoBehaviour, IDisposable
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        private IMemoryPool _pool;
        private Color _darkGreen = new Color(0.9f, 0.9f, 0.9f, 1);
        
        private int _colorPosition;

        public void SetColor()
        { 
            _colorPosition = (int)transform.position.x + (int)transform.position.y;
            
            if (_colorPosition % 2 == 1)
            {
                spriteRenderer.color = _darkGreen;
            }
        }

        private void OnSpawned(IMemoryPool pool)
        {
            _pool = pool;
        }

        public void Dispose()
        {
            _pool = null;
        }

        private void ReInit(SceneObjectProtocol protocol)
        {
            transform.position = protocol.Position;
        }

        public class Pool : MonoMemoryPool<SceneObjectProtocol,GridCell>
        {
            protected override void Reinitialize(SceneObjectProtocol protocol, GridCell item)
            {
                item.ReInit(protocol);
                item.SetColor();
            }

            protected override void OnSpawned(GridCell item)
            {
                base.OnSpawned(item);
                item.OnSpawned(this);
            }
        }
    }
}