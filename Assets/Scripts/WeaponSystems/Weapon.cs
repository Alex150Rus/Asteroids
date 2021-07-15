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

        private void Awake()
        {
            _ammoCounter = new AmmoCounter(1);
            _ammoPool = new AmmoPool(15, new AmmoFactory());
        }

        public void Fire(bool commandReceived)
        {
            if (commandReceived && _ammoCounter.IsProperTimeForFire())
            {
                var ammo = _ammoPool.GetOneAmmo(AmmoType.Green); 
                ammo.Fly();
                ammo.OnScreenBorder += _ammoPool.ReturnObjectToPool;
            }
        }
    }
}
