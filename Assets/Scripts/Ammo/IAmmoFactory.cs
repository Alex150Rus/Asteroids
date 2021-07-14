using System.Dynamic;

namespace Asteroids.Ammo
{
    public interface IAmmoFactory
    {
        public Ammo Create();
    }
}