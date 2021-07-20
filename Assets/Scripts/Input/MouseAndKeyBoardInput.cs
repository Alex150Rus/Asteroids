using System;
using UnityEngine;

namespace Asteroids.Input
{
    public class MouseAndKeyBoardInput: IInput
    {
        public void Dispose()
        {
            OnAxisChange = null;
            OnKeyPressed = null;
            OnEscapeKeyPressed = null;
        }

        public event Action<float, float> OnAxisChange;
        public event Action<bool> OnKeyPressed;
        public event Action<bool> OnEscapeKeyPressed;
        
        public void Execute(float deltaTime)
        {
            
        }
        
        // "Курсор мыши - поворот - корабль игрока поворачивается за курсором мыши. Но у корабля есть скорость поворота"+
        // " - поэтому, корабль поворачивается не моментально." +
        //"Кнопка W или стрелка вверх или правая кнопка мыши - ускорение.

        public void UpdateExecute()
        {
            OnKeyPressed?.Invoke(UnityEngine.Input.GetKeyDown(KeyCode.Space));
            OnKeyPressed?.Invoke(UnityEngine.Input.GetMouseButtonDown(0));
            if( Time.timeScale == 1) {
                OnEscapeKeyPressed?.Invoke(UnityEngine.Input.GetKeyDown(KeyCode.Escape));
            }

            float vertical = 0f;
            if (UnityEngine.Input.GetMouseButton(1))
            {
                vertical = 1f;
            }
            else
            {
                vertical = UnityEngine.Input.GetAxis(NamesManager.AXIS_VERTICAL);    
            }

            var horizontal = UnityEngine.Input.GetAxis(NamesManager.AXIS_MOUSE_X);

            OnAxisChange?.Invoke(horizontal, vertical);
        }
    }
}