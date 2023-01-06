using UnityEngine;

namespace Game.Player
{
    [CreateAssetMenu(fileName = "SnakeConfig", menuName = "Config/SnakeConfig", order = 0)]
    public class SnakeConfig : ScriptableObject
    {
        public Vector3 StartPosition;
        public float MoveDuration;
        public Direction StartDirection;
    }
}