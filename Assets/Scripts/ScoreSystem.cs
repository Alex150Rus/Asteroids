using System;
using System.Collections.Generic;
using Asteroids.AsteroidSystems;
using UnityEngine;

namespace Asteroids
{
    public class ScoreSystem
    {
        private int _points;
        public event Action<int> OnPointsUpdate;
        private Dictionary<AsteroidType, int> _pointsDictionary;

        public int Points
        {
            get => _points;
            set
            {
                _points += value;
                OnPointsUpdate?.Invoke(_points);
            }
        }

        public ScoreSystem()
        {
            _pointsDictionary = new Dictionary<AsteroidType, int>()
            {
                {AsteroidType.Big, 20},
                {AsteroidType.Middle, 50},
                {AsteroidType.Small, 100},
                {AsteroidType.None, 200},
            };
        }

        public void AddPoints(AsteroidType asteroidType)
        {
            Points = _pointsDictionary[asteroidType];
        }
    }
}