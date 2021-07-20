using System;
using UnityEngine;

namespace Asteroids.Input
{
    public interface IInput: IDisposable
    {
        public event Action<float, float> OnAxisChange;
        public event Action<bool> OnKeyPressed;
        public event Action<bool> OnEscapeKeyPressed;
        public void Execute(float deltaTime);
        public void UpdateExecute();
    }
}