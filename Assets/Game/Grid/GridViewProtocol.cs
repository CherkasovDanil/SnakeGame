using UnityEngine;

namespace Game.Grid
{
    public readonly struct GridViewProtocol
    {
        public readonly bool IsDarkedSprite;
        public readonly Vector3 Position;

        public GridViewProtocol(
            bool isDarkedSprite, 
            Vector3 position)
        {
            IsDarkedSprite = isDarkedSprite;
            Position = position;
        }
    }
}