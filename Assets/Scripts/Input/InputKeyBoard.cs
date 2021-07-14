using System;
using UnityEngine;

namespace Asteroids.Input
{
    public class InputKeyBoard: IInput
    {
        public event Action<float, float> OnAxisChange;
        public event Action<bool, KeyCode> OnKeyPressed;
        public void Execute(float deltaTime)
        {
            var vertical = UnityEngine.Input.GetAxis(NamesManager.AXIS_VERTICAL);
            
            var horizontal = UnityEngine.Input.GetAxis(NamesManager.AXIS_HORIZONTAL);
            OnAxisChange?.Invoke(horizontal, vertical);
            OnKeyPressed?.Invoke(UnityEngine.Input.GetKeyDown(KeyCode.Space), KeyCode.Space);
        }
    }
}