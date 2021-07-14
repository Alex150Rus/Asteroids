using UnityEngine;

namespace Asteroids.MoveSystems
{
    public interface IMove
    {
        public void Move(float deltaTime, float horizontal, float vertical, float acceleration, float maxVelocity);
    }
}