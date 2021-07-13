using System;

namespace Asteroids.Input
{
    public class InputAbstractFactory : IInputAbstractFactory
    {
        private IInput _keyboardInput;
        private IInput _mouseAndKeyBoardInput;

        public InputAbstractFactory()
        {
            _keyboardInput = new InputKeyBoard();
            _mouseAndKeyBoardInput = null;
        }

        public IInput Create(InputType inputType)
        {
            switch (inputType)
            {
                case InputType.KeyBoard:
                    return _keyboardInput;
                case InputType.MouseAndKeyBoard:
                    return _mouseAndKeyBoardInput;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}