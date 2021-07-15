namespace Asteroids.AsteroidSystems
{
    public interface IAbstractAsteroidFactory
    {
        public Asteroid Create(AsteroidType asteroidType);
    }
}