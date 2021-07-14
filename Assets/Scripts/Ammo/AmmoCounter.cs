using System;
using Debug = UnityEngine.Debug;

namespace Asteroids.Ammo
{
    public class AmmoCounter: IAmmoCounter
    {

        private float _shoots;
        private long _firstShootTime;
        private float _allowedTimeForThreeShots; 


        public AmmoCounter(float allowedTimeForThreeShots)
        {
            _allowedTimeForThreeShots = allowedTimeForThreeShots;
        }

        public bool IsProperTimeForFire()
        {
            _shoots++;

            switch (_shoots)
            {
                case 1:
                    _firstShootTime = DateTimeOffset.Now.ToUnixTimeSeconds();
                    return true;
                case 4:
                    if (DateTimeOffset.Now.ToUnixTimeSeconds() - _firstShootTime < _allowedTimeForThreeShots)
                    {
                        _shoots = 3;
                        return false;
                    }
                    else
                    {
                        _shoots = 0;
                        return true;
                    }
                default:
                    return true;
            }
        }
    }
}