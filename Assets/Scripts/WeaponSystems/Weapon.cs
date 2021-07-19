using System;
using System.Collections;
using System.Collections.Generic;
using Asteroids.Ammo;
using UnityEngine;

namespace Asteroids.WeaponSystems
{
    public class Weapon : MonoBehaviour, IFire
    {
        private IAmmoCounter _ammoCounter;
        private AmmoPool _ammoPool;
        private ScoreSystem _scoreSystem;
        
        public ScoreSystem ScoreSystem => _scoreSystem;

        private void Awake()
        {
            _ammoCounter = new AmmoCounter(1);
            _ammoPool = new AmmoPool(15, new AmmoFactory());
            _scoreSystem = new ScoreSystem();
        }

        public void Fire(bool commandReceived)
        {
            if (commandReceived && _ammoCounter.IsProperTimeForFire())
            {
                var ammo = _ammoPool.GetOneAmmo(AmmoType.Green); 
                ammo.OnTargetDestroyed += _scoreSystem.AddPoints;
                ammo.Fly();
                ammo.OnScreenBorder += _ammoPool.ReturnObjectToPool;
                ammo.OnTargetReached += _ammoPool.ReturnObjectToPool;
            }
        }
    }
}
