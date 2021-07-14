using UnityEngine;

namespace Asteroids.RotateSystems
{
    public class RotateByPhysics: IRotate
    {
        private Rigidbody2D _rigidbody;
        
        public RotateByPhysics(Rigidbody2D rigidbody)
        {
            _rigidbody = rigidbody;
        }


        public void Rotate(float torque)
        {
            if(torque > 0 || torque < 0) {
                _rigidbody.rotation += torque;
            }
        }
    }
}