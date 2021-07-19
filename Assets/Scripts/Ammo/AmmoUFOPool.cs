using System.Collections.Generic;
using System.Linq;
using Asteroids.Common;
using UnityEngine;

namespace Asteroids.Ammo
{
    public class AmmoUFOPool
    {
        private readonly Dictionary<string, HashSet<AmmoUFO>> _ammoPool;
        private readonly int _capacityPool;
        private Transform _rootPool;
        private IAmmoUFOFactory _ammoFactory;


        public AmmoUFOPool(int capacityPool, IAmmoUFOFactory ammoFactory)
        {
            _ammoPool = new Dictionary<string, HashSet<AmmoUFO>>();
            _capacityPool = capacityPool;
            if (!_rootPool)
            {
                _rootPool = new GameObject(NameManager.POOL_AMMO_UFO).transform;
            }

            _ammoFactory = ammoFactory;
        }

        public AmmoUFO GetOneAmmo(AmmoType ammoType)
        {
            AmmoUFO result;
            result = GetAmmo(GetListAmmos(ammoType), ammoType);
            return result;
        }

        private HashSet<AmmoUFO> GetListAmmos(AmmoType ammoType)
        {
            return _ammoPool.ContainsKey(ammoType.ToString())
                ? _ammoPool[ammoType.ToString()]
                : _ammoPool[ammoType.ToString()] = new HashSet<AmmoUFO>();
        }

        private AmmoUFO GetAmmo(HashSet<AmmoUFO> ammos, AmmoType ammoType)
        {
            var ammo = ammos.FirstOrDefault(a => !a.gameObject.activeSelf);
            if (ammo == null)
            {
                for (var i = 0; i < _capacityPool; i++)
                {
                    var instantiate = _ammoFactory.Create();
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