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
        [SerializeField] private Rigidbody2D _player;
        [SerializeField] private Transform _ammoStartingPoint;
        private AmmoPool _ammoPool;
        private long _lastShotTime;

        private void Awake()
        {
            _ammoPool = new AmmoPool(5, new AmmoFactory(_player, _ammoStartingPoint));
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
