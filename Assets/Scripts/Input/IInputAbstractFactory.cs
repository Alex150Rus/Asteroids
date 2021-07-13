namespace Asteroids.Input
{
    public interface IInputAbstractFactory
    {
        public IInput Create(InputType inputType);
    }
}