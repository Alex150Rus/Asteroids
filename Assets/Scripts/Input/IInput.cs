using System;

namespace Asteroids.Input
{
    public interface IInput
    {
        public event Action<float, float> OnAxisChange;
        public void Execute(float deltaTime);
    }
}