using UnityEngine;

namespace Asteroids.Ammo
{
    public class AmmoFactory : IAmmoFactory
    {
        public Ammo Create()
        {
            var ammo = Resources.Load<Ammo>("prefabs/typeGreen");
            var ammoInstance = GameObject.Instantiate(ammo);
            return ammoInstance;
        }
    }
}