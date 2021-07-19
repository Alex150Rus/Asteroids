
using System.Collections.Generic;
using Asteroids.AsteroidSystems;
using UnityEngine;

namespace Asteroids
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private Transform player;
        private AsteroidController _asteroidController;


        private void Awake()
        {
            _asteroidController = new AsteroidController();
            _asteroidController.Start();
        }

        private void Update()
        {
            _asteroidController.Execute();
        }
    }
}