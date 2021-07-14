using UnityEngine;

namespace Asteroids.MoveSystems
{
    public class MovePhysicsWithoutInertia : IMove
    {
        private Vector2 _direction;
        private Rigidbody2D _rigidbody;
        private Transform _transform;
        private Vector2 _newVelocity;

        public MovePhysicsWithoutInertia(Rigidbody2D rigidBody)
        {
            _rigidbody = rigidBody;
            _transform = rigidBody.transform;
        }

        public void Move(float deltaTime, float horizontal, float vertical, float acceleration, float maxVelocity)
        {
            if (vertical > 0 && _newVelocity.magnitude <= maxVelocity)
            {
                Vector2 v0 = _rigidbody.velocity;
                var accelerationVector = (Vector2) _transform.up * acceleration * deltaTime;
                _newVelocity.Set(accelerationVector.x + v0.x, accelerationVector.y + v0.y);
                _rigidbody.velocity = _newVelocity;
            }
            else
            {
                _rigidbody.velocity = _transform.up * _newVelocity.magnitude;
            }
        }
    }
}