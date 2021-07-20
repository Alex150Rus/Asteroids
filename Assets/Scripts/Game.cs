
using System;
using System.Collections;
using System.Collections.Generic;
using Asteroids.AsteroidSystems;
using Asteroids.Input;
using Asteroids.UI;
using Asteroids.WeaponSystems;
using UnityEngine;

namespace Asteroids
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private int _waitSecondsBetweenAsteroidWaves;
        [SerializeField] private float _angleOfNewAsteroids;
        [SerializeField] private UFO.UFO _ufo;
        [SerializeField] private Hud _hud;
        [SerializeField] private Weapon _weapon;
        
        private AsteroidController _asteroidController;


        private void Awake()
        {
            Time.timeScale = 0;
            _asteroidController = new AsteroidController(_waitSecondsBetweenAsteroidWaves, _angleOfNewAsteroids);
            _asteroidController.Start();
        }

        private void Start()
        {
            _weapon.ScoreSystem.OnPointsUpdate += _hud.SetScore;
            _ufo.OnCollision += () => StartCoroutine(StartUfoMoving());
            StartCoroutine(StartUfoMoving());
        }

        private void Update()
        {
            _asteroidController.Execute();
        }

        private IEnumerator StartUfoMoving()
        {
            yield return new WaitForSecondsRealtime(_ufo.TimeBetweenSpawn);
            _ufo.StartMoving();
        }
    }
}