using UnityEngine;

namespace Asteroids.Common
{
    public interface ISetObjectToStartingState: IScreenBorderSystem
    {
        public void SetObjectToStartingState();
        public void SetStartingPosition(Transform transform);
    }
}