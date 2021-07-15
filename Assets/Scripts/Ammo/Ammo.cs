using System;
using Asteroids.WeaponSystems;
using UnityEngine;
using UnityEngine.Rendering;

namespace Asteroids.Ammo
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ammo: MonoBehaviour, IAmmo
    {
        public event Action<Transform> OnScreenBorder;
        [SerializeField] private float _velocity;
        
        private Rigidbody2D _playerRigidBody;
        private Transform _startingPoint;
        private Rigidbody2D _rigidbody;
        private float _screenWidth;
        private float _screenHeight;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _startingPoint = FindObjectOfType<AmmoStartingPoint>().transform;
            _playerRigidBody = FindObjectOfType<Player>().gameObject.GetComponent<Rigidbody2D>();
            _screenWidth = ((Vector2)Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0))).x;
            _screenHeight = ((Vector2) Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0))).y;
        }

        private void Update()
        {
            if(Mathf.Abs(transform.position.x) > _screenWidth || Mathf.Abs(transform.position.y) > _screenHeight)
            {
                OnScreenBorder?.Invoke(transform);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            
        }

        public void Fly()
        {
            gameObject.SetActive(true);
            MoveToStartingPoint();
            AddVelocity();
        }

        private void MoveToStartingPoint()
        {
            transform.position = _startingPoint.position;
        }

        private void AddVelocity()
        {
            _rigidbody.velocity = _startingPoint.up * (_velocity + _playerRigidBody.velocity.magnitude);
        }
    }
}