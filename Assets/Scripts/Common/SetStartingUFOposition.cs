using UnityEngine;

namespace Asteroids.Common
{
    public class SetStartingUFOposition
    {
        private float _screenWidth;
        private float _screenHeight;
        
        public SetStartingUFOposition(int _percentageOfBorderMargin)
        {
            _screenWidth = ((Vector2)Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0))).x;
            _screenHeight = ((Vector2) Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0))).y;
            _screenHeight *= (100 - _percentageOfBorderMargin)*0.01f;
        }

        public int SetPosition(Transform transform)
        {

            if (transform.GetComponent<Rigidbody2D>().IsTouchingLayers(NamesManager.MOVABLES_LAYER))
            {
                SetPosition(transform);
            }

            var value = Random.Range(-1, 1);
            int sign = value == -1 ? -1 : 1;
            
            transform.position = new Vector3(_screenWidth * sign,
                Random.Range(-_screenHeight, _screenHeight), 0f);
            return sign;
        }
    }
}