using Asteroids.Common;
using UnityEngine;

namespace Asteroids.MoveSystems
{
    public class MoveRigidbody: IMoveRigidbody
    {
        public void Move(IMovable movable)
        {
            movable.Rigidbody.velocity = movable.Direction.normalized * movable.Speed;
        }
    }
}