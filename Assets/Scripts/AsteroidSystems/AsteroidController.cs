using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids.AsteroidSystems
{
    public class AsteroidController
    {
        private AsteroidsPool _asteroidsPool;
        private int _qtyOfAteroidsOnScene = 0;
        private int _previousQtyOfAteroidsOnScene = 0;
        private float _asteroidsSpeed;
        private long _timePassedSinceAllAsteroidsKilled;

        public void Start()
        {
            _asteroidsPool = new AsteroidsPool(5, new AbstractAsteroidFactory());
            _previousQtyOfAteroidsOnScene = 2;
            for (int i = 0; i < _previousQtyOfAteroidsOnScene; i++)
            {
                var asteroid = GetAsteroid(AsteroidType.Big);
                SetAteroidSpeed(i, asteroid);
            }

            _timePassedSinceAllAsteroidsKilled = 0;
        }

        private void MoveAsteroidToPool(Transform transform)
        {
            var asteroid = transform.GetComponent<Asteroid>();
            asteroid.OnPlayerUFOCollisionEnter -= MoveAsteroidToPool;
            asteroid.OnAmmoCollisionEnter -= AfterAmmoCollisionLogic;
            _asteroidsPool.ReturnObjectToPool(transform);
            _qtyOfAteroidsOnScene--;
        }

        private Asteroid GetAsteroid(AsteroidType type)
        {
            var asteroid = _asteroidsPool.GetOneAsteroid(type);
            asteroid.gameObject.SetActive(true);
            asteroid.OnPlayerUFOCollisionEnter += MoveAsteroidToPool;
            asteroid.OnAmmoCollisionEnter += AfterAmmoCollisionLogic;
            _qtyOfAteroidsOnScene++;
            return asteroid;
        }

        private void AfterAmmoCollisionLogic(Transform transform)
        {
            var asteroid = transform.GetComponent<Asteroid>();
            if (asteroid.asteroidType == AsteroidType.Big || asteroid.asteroidType == AsteroidType.Middle)
            {
                var dir = asteroid.transform.up;
                var newDir45 = Quaternion.Euler(0f, 0f, 45) * dir;
                var newDir_45 = Quaternion.Euler(0f, 0f, -45) * dir;
                

                for (int i = 0; i < 2; i++)
                {
                    var aster = GetAsteroid(asteroid.asteroidType + 1);
                    if (i == 0)
                        aster.Direction = newDir45;
                    else aster.Direction = newDir_45;
                    
                    aster.transform.position = transform.position;
                    aster.Speed = asteroid.Speed;
                }
                MoveAsteroidToPool(transform);
            }
            else
            {
                MoveAsteroidToPool(transform);
            }
        }

        public void Execute()
        {
            Debug.Log(_qtyOfAteroidsOnScene);
            if (_qtyOfAteroidsOnScene == 0)
            {
                if (_timePassedSinceAllAsteroidsKilled == 0)
                    _timePassedSinceAllAsteroidsKilled = DateTimeOffset.Now.ToUnixTimeSeconds();

                if (DateTimeOffset.Now.ToUnixTimeSeconds() - _timePassedSinceAllAsteroidsKilled >= 2)
                {
                    _previousQtyOfAteroidsOnScene++;
                    for (int i = 0; i < _previousQtyOfAteroidsOnScene; i++)
                    {
                        var asteroid = GetAsteroid((AsteroidType) Random.Range(1, 4));
                        SetAteroidSpeed(i, asteroid);
                    }

                    _timePassedSinceAllAsteroidsKilled = 0;
                }
            }
        }

        private void SetAteroidSpeed(int i, Asteroid asteroid)
        {
            if (i == 0)
                _asteroidsSpeed = asteroid.Speed;
            else
                asteroid.Speed = _asteroidsSpeed;
        }
    }
}