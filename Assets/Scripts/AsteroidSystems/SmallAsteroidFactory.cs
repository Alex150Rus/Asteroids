using UnityEngine;

namespace Asteroids.AsteroidSystems
{
    public class SmallAsteroidFactory : IAsteroidFactory
    {
        public Asteroid Create()
        {
            var asteroid = Resources.Load<Asteroid>("prefabs/asteroidSmall");
            var asteroidInstance = GameObject.Instantiate(asteroid);
            return asteroidInstance;
        }
    }
}