using System;
using UnityEngine;

namespace Asteroids.Input
{
    public interface IInput
    {
        public event Action<float, float> OnAxisChange;
        public event Action<bool, KeyCode> OnKeyPressed;
        public void Execute(float deltaTime);
    }
}