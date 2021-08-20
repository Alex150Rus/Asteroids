namespace Asteroids.AsteroidSystems
{
    public interface IAsteroidFactory
    {
        public Asteroid Create(AsteroidType asteroidType);
    }
}