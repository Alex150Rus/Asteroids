using UnityEngine;

namespace Asteroids.AsteroidSystems
{
    public class MiddleAsteroidFactory : IAsteroidFactory
    {
        public Asteroid Create()
        {
            var asteroid = Resources.Load<Asteroid>("prefabs/asteroidMiddle");
            asteroid.asteroidType = AsteroidType.Middle;
            var asteroidInstance = GameObject.Instantiate(asteroid);
            return asteroidInstance;
        }
    }
}