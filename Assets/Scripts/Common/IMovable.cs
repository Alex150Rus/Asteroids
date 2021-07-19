using UnityEngine;

namespace Asteroids.Common
{
    public interface IMovable
    {
        public float Speed { get; set; }
        public Vector2 Direction { get; set; }
        public Rigidbody2D Rigidbody { get; }
        
    }
}