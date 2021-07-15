using UnityEngine;

namespace Asteroids.Common
{
    public class PlayerScreenBorderWork : IScreenBorderSystem
    {
        private float _screenWidth;
        private float _screenHeight;
        
        public PlayerScreenBorderWork()
        {
            _screenWidth = ((Vector2)Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0))).x;
            _screenHeight = ((Vector2) Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0))).y;
        }
        public void ScreenBorderWork(Transform transform)
        {
            if (Mathf.Abs(transform.position.x) > _screenWidth)
                transform.position =
                    new Vector3(_screenWidth * Mathf.Sign(transform.position.x) * -1, transform.position.y, 0);


            if (Mathf.Abs(transform.position.y) > _screenHeight)
                transform.position =
                    new Vector3(transform.position.x, _screenHeight * Mathf.Sign(transform.position.y) * -1, 0);
        }
    }
}