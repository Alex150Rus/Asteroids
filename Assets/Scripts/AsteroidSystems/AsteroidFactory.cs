using System;
using System.Collections.Generic;
using Asteroids.Common;
using UnityEngine;

namespace Asteroids.AsteroidSystems
{
    public class AsteroidFactory : IAsteroidFactory
    {
        private Dictionary<AsteroidType, Asteroid> _asteroids;

        public AsteroidFactory(int asteroidTypesQty)
        {
            _asteroids = new Dictionary<AsteroidType, Asteroid>(asteroidTypesQty);
        }

        public Asteroid Create(AsteroidType asteroidType)
        {
            var asteroid = _asteroids.ContainsKey(asteroidType)
                ? _asteroids[asteroidType]
                : _asteroids[asteroidType] = Resources.Load<Asteroid>(GetAsteroidPrefabPath(asteroidType));

            asteroid.asteroidType = asteroidType;
            var asteroidInstance = GameObject.Instantiate(asteroid);
            return _asteroids[asteroidType] = asteroidInstance;
        }

        private string GetAsteroidPrefabPath(AsteroidType type)
        {
            switch (type)
            {
                case AsteroidType.Big:
                    return NameManager.BIG_ASTEROID_PREFAB_PATH;
                case AsteroidType.Middle:
                    return NameManager.MIDDLE_ASTEROID_PREFAB_PATH;
                case AsteroidType.Small:
                    return NameManager.SMALL_ASTEROID_PREFAB_PATH;
                default:
                    throw new IndexOutOfRangeException();
            }
        }
    }
}