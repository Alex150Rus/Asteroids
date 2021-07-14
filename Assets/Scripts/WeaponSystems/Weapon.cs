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
        private void Awake()
        {
            _ammoCounter = new AmmoCounter(1);
        }

        public void Fire(bool commandReceived)
        {
            if (commandReceived && _ammoCounter.IsProperTimeForFire())
            {
                Debug.Log("Fire");
            }
        }
    }
}
