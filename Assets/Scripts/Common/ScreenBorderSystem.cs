using UnityEngine;

namespace Asteroids.Common
{
    public class ScreenBorderSystem: ICompareDistanceWithScreenWidth
    {
        private float _screenWidth;
        private float _screenHeight;
        private Vector2 _startPosition;
        private float _distance;
        private float _everyFrameDistance;

        public ScreenBorderSystem(Transform transform)
        {
            _screenWidth = ((Vector2)Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0))).x;
            _screenHeight = ((Vector2) Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0))).y;
            _startPosition = transform.position;
        }
        
        public bool IsDistanceEqualScreenWith(Transform transform)
        {
            if (Vector2.Distance(_startPosition, transform.position) + _distance > _screenWidth * 2)
            {
                return true;
            }

            return false;
        }

        public void ScreenBorderWork(Transform transform)
        {
            if (Mathf.Abs(transform.position.x) > _screenWidth)
            {
                _distance += Vector2.Distance(_startPosition, transform.position);
                transform.position = _startPosition =
                    new Vector3(_screenWidth * Mathf.Sign(transform.position.x) * -1, transform.position.y, 0);

            }

            if (Mathf.Abs(transform.position.y) > _screenHeight) {
                _distance += Vector2.Distance(_startPosition, transform.position);
                transform.position = _startPosition =
                    new Vector3(transform.position.x,_screenHeight * Mathf.Sign(transform.position.y) * -1,0);
            }
        }

        public void SetObjectToStartingState()
        {
        _distance = 0f;
        _everyFrameDistance = 0f;
        }

        public void SetStartingPosition(Transform transform)
        {
            _startPosition = transform.position;
        }
    }
}