using System.Collections.Generic;
using System.Linq;
using Asteroids.Common;
using UnityEngine;

namespace Asteroids.AsteroidSystems
{
    public class AsteroidsPool
    {
        private readonly Dictionary<string, HashSet<Asteroid>> _asteroidPool;
        private readonly int _capacityPool;
        private Transform _rootPool;
        private IAbstractAsteroidFactory _abstractAsteroidFactory;


        public AsteroidsPool(int capacityPool, IAbstractAsteroidFactory abstractAsteroidFactory)
        {
            _asteroidPool = new Dictionary<string, HashSet<Asteroid>>();
            _capacityPool = capacityPool;
            if (!_rootPool)
            {
                _rootPool = new GameObject(NameManager.POOL_ASTEROID).transform;
            }

            _abstractAsteroidFactory = abstractAsteroidFactory;
        }

        public Asteroid GetOneAsteroid(AsteroidType asteroidType)
        {
            Asteroid result;
            result = GetAsteroid(GetListAsteroids(asteroidType), asteroidType);
            return result;
        }

        private HashSet<Asteroid> GetListAsteroids(AsteroidType asteroidType)
        {
            return _asteroidPool.ContainsKey(asteroidType.ToString())
                ? _asteroidPool[asteroidType.ToString()]
                : _asteroidPool[asteroidType.ToString()] = new HashSet<Asteroid>();
        }

        private Asteroid GetAsteroid(HashSet<Asteroid> asteroids, AsteroidType asteroidType)
        {
            var asteroid = asteroids.FirstOrDefault(a => !a.gameObject.activeSelf);
            if (asteroid == null)
            {
                for (var i = 0; i < _capacityPool; i++)
                {
                    var instantiate = _abstractAsteroidFactory.Create(asteroidType);
                    ReturnToPool(instantiate.gameObject.transform);
                    asteroids.Add(instantiate);
                }

                GetAsteroid(asteroids, asteroidType);
            }

            asteroid = asteroids.FirstOrDefault(a => !a.gameObject.activeSelf);
            return asteroid;
        }

        private void ReturnToPool(Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.gameObject.SetActive(false);
            transform.SetParent(_rootPool);
        }
        
        public void ReturnObjectToPool(Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.gameObject.SetActive(false);
        }

        public void RemovePool()
        {
            Object.Destroy(_rootPool.gameObject);
        }
    }
}