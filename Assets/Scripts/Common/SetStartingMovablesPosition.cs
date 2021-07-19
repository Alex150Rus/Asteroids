using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

namespace Asteroids.Common
{
    public class SetStartingMovablesPosition: ISetStartingMovablesPosition
    {
        private float _screenWidth;
        private float _screenHeight;
        
        public SetStartingMovablesPosition()
        {
            _screenWidth = ((Vector2)Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0))).x;
            _screenHeight = ((Vector2) Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0))).y;
        }

        public void SetPosition(Transform transform)
        {

            if (transform.GetComponent<Rigidbody2D>().IsTouchingLayers(NamesManager.MOVABLES_LAYER))
            {
                SetPosition(transform);
            }
            transform.position = new Vector3(Random.Range(-_screenWidth, _screenWidth),
                Random.Range(-_screenHeight, _screenHeight), 0f);
        }
    }
}