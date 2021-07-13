using System;
using UnityEngine;

namespace Asteroids
{
    public class MovePhysicsWithInertia : IMove
    {
        private Vector2 _direction;
        private Rigidbody2D _rigidbody;
        private Transform _transform;
        private Vector2 _newVelocity;
        private Vector2 _previousTransformUp;

        public MovePhysicsWithInertia(Rigidbody2D rigidBody)
        {
            _rigidbody = rigidBody;
            _transform = rigidBody.transform;
        }

        public void Move(float deltaTime, float horizontal, float vertical, float acceleration, float maxVelocity)
        {
            if (vertical > 0 && horizontal == 0f)
            {
                if (_previousTransformUp != (Vector2) _transform.up)
                {
                    _rigidbody.velocity = Vector2.zero;
                    _rigidbody.velocity = _newVelocity.magnitude * _transform.up;
                }
                Vector2 v0 = _rigidbody.velocity;
                _previousTransformUp = _transform.up;
                var accelerationVector = (Vector2) _transform.up * acceleration * deltaTime;
                _newVelocity.Set(accelerationVector.x + v0.x, accelerationVector.y + v0.y);
                if (_rigidbody.velocity.magnitude > maxVelocity)
                {
                    _rigidbody.velocity = _newVelocity = _transform.up * maxVelocity;
                }
                
                _rigidbody.velocity = _newVelocity;
                Debug.Log(_rigidbody.velocity.magnitude);
            }
        }
    }
}