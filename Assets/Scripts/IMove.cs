using UnityEngine;

namespace Asteroids
{
    public interface IMove
    {
        public void Move(float deltaTime, float horizontal, float vertical, float acceleration, float maxVelocity);
    }
}