using UnityEngine;

namespace Game.Grid
{
    [CreateAssetMenu(fileName = "GridConfig", menuName = "Config/GridConfig", order = 1)]
    public class GridConfig : ScriptableObject
    {
        public int Width;
        public int Height;
    }
}