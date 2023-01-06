using UnityEngine;
using Zenject;

namespace Game.Grid
{
    public class GridView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        private Color _darkGreen = new Color(0.9f, 0.9f, 0.9f, 1);
        
        private int _colorPosition;

        [Inject] 
        private void Constructor(GridViewProtocol gridViewProtocol)
        {
            if (gridViewProtocol.IsDarkedSprite)
            {
                spriteRenderer.color = _darkGreen;
            }

            transform.position = gridViewProtocol.Position;
        }
    }
}