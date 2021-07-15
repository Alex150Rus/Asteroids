using UnityEngine;

namespace Asteroids.AsteroidSystems
{
    public class MiddleAsteroidFactory : IAsteroidFactory
    {
        public Asteroid Create()
        {
            var asteroid = Resources.Load<Asteroid>("prefabs/asteroidMiddle");
            var asteroidInstance = GameObject.Instantiate(asteroid);
            return asteroidInstance;
        }
    }
}