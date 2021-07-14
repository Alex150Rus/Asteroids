using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.WeaponSystems
{
    public class Weapon : MonoBehaviour, IFire
    {
        public void Fire(bool commandReceived)
        {
            if (commandReceived)
            {
                Debug.Log("Fire");
            }
        }
    }
}
