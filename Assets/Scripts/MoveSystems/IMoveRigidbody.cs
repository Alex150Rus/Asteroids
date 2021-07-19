using Asteroids.Common;
using UnityEngine;

namespace Asteroids.MoveSystems
{
    public interface IMoveRigidbody
    {
        public void Move(IMovable movable);
    }
}