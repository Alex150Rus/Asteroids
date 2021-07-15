using UnityEngine;

namespace Asteroids.Common
{
    public interface ICompareDistanceWithScreenWidth: ISetObjectToStartingState
    {
        public bool IsDistanceEqualScreenWith(Transform transform);
    }
}