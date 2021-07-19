using UnityEngine;

namespace Asteroids.Ammo
{
    public class AmmoUFOFactory : IAmmoUFOFactory
    {
        public AmmoUFO Create()
        {
            var ammo = Resources.Load<AmmoUFO>("prefabs/typeRed");
            var ammoInstance = GameObject.Instantiate(ammo);
            return ammoInstance;
        }
    }
}