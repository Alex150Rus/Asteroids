using System;
using System.Collections.Generic;
using Asteroids.Common;
using UnityEngine;

namespace Asteroids.Ammo
{
    public sealed class AmmoFactory : IAmmoFactory
    {
        private readonly Dictionary<AmmoType, Ammo> _bullets;

        public AmmoFactory(int qtyOfBulletTypes = 1)
        {
            _bullets = new Dictionary<AmmoType, Ammo>(qtyOfBulletTypes);
        }
        
        public Ammo Create(AmmoType type)
        {
            var ammo = _bullets.ContainsKey(type) 
                ? _bullets[type]
                : _bullets[type] = Resources.Load<Ammo>(GetAmmoPrefabPath(type));
            ammo.ammoType = type;
            var ammoInstance = GameObject.Instantiate(ammo);
            return ammoInstance;
        }

        private string GetAmmoPrefabPath(AmmoType type)
        {
            switch (type)
            {
                case AmmoType.Green:
                    return NameManager.AMMO_TYPE_GREEN_PATH;
                case AmmoType.Red:
                    return NameManager.AMMO_TYPE_RED_PATH;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}