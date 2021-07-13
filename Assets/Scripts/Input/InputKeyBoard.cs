using System;
using UnityEngine;

namespace Asteroids.Input
{
    public class InputKeyBoard: IInput
    {
        public event Action<float, float> OnAxisChange;
        public void Execute(float deltaTime)
        {
            var vertical = UnityEngine.Input.GetAxis(NamesManager.AXIS_VERTICAL);
            
            var horizontal = UnityEngine.Input.GetAxis(NamesManager.AXIS_HORIZONTAL);
            OnAxisChange?.Invoke(horizontal, vertical);
        }
    }
}