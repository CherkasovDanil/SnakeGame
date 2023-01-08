using UnityEngine;

namespace Game.Grid
{
    public readonly struct GridViewProtocol
    {
        public readonly bool IsDarkSprite;
        public readonly Vector3 Position;

        public GridViewProtocol(
            bool isDarkSprite, 
            Vector3 position)
        {
            IsDarkSprite = isDarkSprite;
            Position = position;
        }
    }
}