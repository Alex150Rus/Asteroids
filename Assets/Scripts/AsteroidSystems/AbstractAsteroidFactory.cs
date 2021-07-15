using System;

namespace Asteroids.AsteroidSystems
{
    public class AbstractAsteroidFactory: IAbstractAsteroidFactory
    {
        private BigAsteroidFactory _bigAsteroidFactory;
        private MiddleAsteroidFactory _middleAsteroidFactory;
        private SmallAsteroidFactory _smallAsteroidFactory;

        public AbstractAsteroidFactory()
        {
            _bigAsteroidFactory = new BigAsteroidFactory();
            _middleAsteroidFactory = new MiddleAsteroidFactory();
            _smallAsteroidFactory = new SmallAsteroidFactory();
        }
        
        public Asteroid Create(AsteroidType asteroidType)
        {
            switch (asteroidType)
            {
                case AsteroidType.Big:
                    return _bigAsteroidFactory.Create();
                case AsteroidType.Middle:
                    return _middleAsteroidFactory.Create();
                case AsteroidType.Small:
                    return _smallAsteroidFactory.Create();
                default:
                    throw new IndexOutOfRangeException();
            }
        }
    }
}