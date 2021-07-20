using System;
using UnityEngine;

namespace Asteroids.Input
{
    public class InputKeyBoard: IInput, IDisposable
    {
        public event Action<float, float> OnAxisChange;
        public event Action<bool> OnKeyPressed;
        public event Action<bool> OnEscapeKeyPressed;
        public void Execute(float deltaTime)
        {
            var vertical = UnityEngine.Input.GetAxis(NamesManager.AXIS_VERTICAL);
            
            var horizontal = UnityEngine.Input.GetAxis(NamesManager.AXIS_HORIZONTAL);
            OnAxisChange?.Invoke(horizontal, vertical);
        }

        public void UpdateExecute()
        {
            OnKeyPressed?.Invoke(UnityEngine.Input.GetKeyDown(KeyCode.Space));
            if(Time.timeScale == 1)
                OnEscapeKeyPressed?.Invoke(UnityEngine.Input.GetKeyDown(KeyCode.Escape));
        }

        public void Dispose()
        {
            OnAxisChange = null;
            OnKeyPressed = null;
            OnEscapeKeyPressed = null;
        }
    }
}