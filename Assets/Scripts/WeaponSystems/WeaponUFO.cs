using System;
using System.Collections;
using System.Collections.Generic;
using Asteroids.Ammo;
using UnityEngine;

namespace Asteroids.WeaponSystems
{
    public class WeaponUFO : MonoBehaviour
    {
        [SerializeField] private int _intervalBetweenFire;
        private AmmoUFOPool _ammoPool;
        private long _lastShotTime;

        private void Awake()
        {
            _ammoPool = new AmmoUFOPool(5, new AmmoUFOFactory());
        }

        private void Update()
        {
            if (gameObject.activeSelf)
                Fire();
        }

        public void Fire()
        {
            if (DateTimeOffset.Now.ToUnixTimeSeconds() - _lastShotTime >= _intervalBetweenFire)
            {
                var ammo = _ammoPool.GetOneAmmo(AmmoType.Red); 
                ammo.Fly();
                _lastShotTime = DateTimeOffset.Now.ToUnixTimeSeconds();
                ammo.OnScreenBorder += _ammoPool.ReturnObjectToPool;
                
            }
        }
    }
}
