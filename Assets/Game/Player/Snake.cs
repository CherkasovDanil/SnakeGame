using System;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class Snake: MonoBehaviour, IDisposable
    {
        public void Dispose()
        {
            Destroy(gameObject);
        }

        public class Factory : PlaceholderFactory<Snake>
        { }
    }
}