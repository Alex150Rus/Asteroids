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
        public AmmoType ammoType;
        public Rigidbody2D playerRigidBody;
        public Transform startingPoint;

        [SerializeField] private float _velocity;
        private Rigidbody2D _rigidbody;
        private ICompareDistanceWithScreenWidth _screenBorderSystem;


        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
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
            transform.position = startingPoint.position;
        }

        private void AddVelocity()
        {
            switch (ammoType)
            {
                case AmmoType.Green:
                    _rigidbody.velocity = startingPoint.up * (_velocity + playerRigidBody.velocity.magnitude);
                    break;
                case AmmoType.Red:
                    _rigidbody.velocity =(playerRigidBody.transform.position - _rigidbody.transform.position).normalized 
                    * (_velocity + playerRigidBody.velocity.magnitude);
                    break;
                default: throw new ArgumentOutOfRangeException();
            }
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