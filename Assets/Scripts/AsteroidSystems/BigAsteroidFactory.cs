using UnityEngine;

namespace Asteroids.AsteroidSystems
{
    public class BigAsteroidFactory: IAsteroidFactory
    {
        public Asteroid Create()
        {
            var asteroid = Resources.Load<Asteroid>("prefabs/asteroidBig");
            asteroid.asteroidType = AsteroidType.Big;
            var asteroidInstance = GameObject.Instantiate(asteroid);
            return asteroidInstance;
        }
    }
}