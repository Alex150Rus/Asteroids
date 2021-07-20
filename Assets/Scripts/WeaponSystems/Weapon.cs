using System;
using System.Collections;
using System.Collections.Generic;
using Asteroids.Ammo;
using Asteroids.AsteroidSystems;
using UnityEngine;

namespace Asteroids.WeaponSystems
{
    public class Weapon : MonoBehaviour, IFire
    {
        private IAmmoCounter _ammoCounter;
        private AmmoPool _ammoPool;
        private ScoreSystem _scoreSystem;
        private Sound _sound;
        
        public ScoreSystem ScoreSystem => _scoreSystem;

        private void Awake()
        {
            _ammoCounter = new AmmoCounter(1);
            _ammoPool = new AmmoPool(15, new AmmoFactory());
            _scoreSystem = new ScoreSystem();
            _sound = GetComponent<Sound>();
        }

        public void Fire(bool commandReceived)
        {
            if (commandReceived && _ammoCounter.IsProperTimeForFire() && Time.timeScale == 1)
            {
                var ammo = _ammoPool.GetOneAmmo(AmmoType.Green);
                ammo.OnTargetDestroyed += _scoreSystem.AddPoints;
                ammo.OnTargetDestroyed += PlayDemolisionSound;
                ammo.Fly();
                _sound.PlayByIndex(0);
                ammo.OnScreenBorder += _ammoPool.ReturnObjectToPool;
                ammo.OnTargetReached += _ammoPool.ReturnObjectToPool;
            }
        }

        private void PlayDemolisionSound(AsteroidType asteroidType)
        {
            if ((int) asteroidType == 0)
                asteroidType = AsteroidType.Big;
            _sound.PlayByIndex((int)asteroidType);
        }
    }
}
