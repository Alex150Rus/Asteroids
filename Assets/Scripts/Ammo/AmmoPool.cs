using System.Collections.Generic;
using System.Linq;
using Asteroids.Common;
using UnityEngine;

namespace Asteroids.Ammo
{
    public class AmmoPool
    {
        private readonly Dictionary<string, HashSet<Ammo>> _ammoPool;
        private readonly int _capacityPool;
        private Transform _rootPool;
        private IAmmoFactory _ammoFactory;


        public AmmoPool(int capacityPool, IAmmoFactory ammoFactory)
        {
            _ammoPool = new Dictionary<string, HashSet<Ammo>>();
            _capacityPool = capacityPool;
            if (!_rootPool)
            {
                _rootPool = new GameObject(NameManager.POOL_AMMO).transform;
            }

            _ammoFactory = ammoFactory;
        }

        public Ammo GetOneAmmo(AmmoType ammoType)
        {
            Ammo result;
            result = GetAmmo(GetListAmmos(ammoType), ammoType);
            return result;
        }

        private HashSet<Ammo> GetListAmmos(AmmoType ammoType)
        {
            return _ammoPool.ContainsKey(ammoType.ToString())
                ? _ammoPool[ammoType.ToString()]
                : _ammoPool[ammoType.ToString()] = new HashSet<Ammo>();
        }

        private Ammo GetAmmo(HashSet<Ammo> ammos, AmmoType ammoType)
        {
            var ammo = ammos.FirstOrDefault(a => !a.gameObject.activeSelf);
            if (ammo == null)
            {
                for (var i = 0; i < _capacityPool; i++)
                {
                    var instantiate = _ammoFactory.Create(ammoType);
                    ReturnToPool(instantiate.gameObject.transform);
                    ammos.Add(instantiate);
                }

                GetAmmo(ammos, ammoType);
            }

            ammo = ammos.FirstOrDefault(a => !a.gameObject.activeSelf);
            return ammo;
        }

        private void ReturnToPool(Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.gameObject.SetActive(false);
            transform.SetParent(_rootPool);
        }
        
        public void ReturnObjectToPool(Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.gameObject.SetActive(false);
        }

        public void RemovePool()
        {
            Object.Destroy(_rootPool.gameObject);
        }
    }
}