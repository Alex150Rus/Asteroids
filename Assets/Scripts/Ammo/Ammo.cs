using System;
using Asteroids.AsteroidSystems;
using Asteroids.Common;
using Asteroids.WeaponSystems;
using UnityEngine;
using UnityEngine.Rendering;

namespace Asteroids.Ammo
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ammo : MonoBehaviour, IAmmo
    {
        public event Action<Transform> OnScreenBorder;
        public event Action<Transform> OnTargetReached;
        public event Action<AsteroidType> OnTargetDestroyed;
        [SerializeField] private float _velocity;

        private Rigidbody2D _playerRigidBody;
        private Transform _startingPoint;
        private Rigidbody2D _rigidbody;
        private ICompareDistanceWithScreenWidth _screenBorderSystem;


        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _startingPoint = FindObjectOfType<AmmoStartingPoint>().transform;
            _playerRigidBody = FindObjectOfType<Player>().gameObject.GetComponent<Rigidbody2D>();
            _screenBorderSystem = new ScreenBorderSystem(transform);
        }

        private void Update()
        {
            if (_screenBorderSystem.IsDistanceEqualScreenWith(transform))
                OnScreenBorder?.Invoke(transform);
            _screenBorderSystem.ScreenBorderWork(transform);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(NamesManager.ASTEROID_TAG))
            {
                var asteroidType = other.GetComponent<Asteroid>().asteroidType;
                
                OnTargetDestroyed?.Invoke(asteroidType);
                OnTargetReached?.Invoke(transform);
            }
            
            
            if (other.CompareTag(NamesManager.UFO_TAG))
            {
                OnTargetDestroyed?.Invoke(AsteroidType.None);
                OnTargetReached?.Invoke(transform);
            }
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

        private void OnDisable()
        {
            _screenBorderSystem.SetObjectToStartingState();
            OnScreenBorder = null;
            OnTargetReached = null;
            OnTargetDestroyed = null;

        }

        private void OnEnable()
        {
            _screenBorderSystem.SetStartingPosition(transform);
        }
    }
}