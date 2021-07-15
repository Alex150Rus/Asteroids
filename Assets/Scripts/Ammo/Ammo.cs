using System;
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

        private void OnDisable()
        {
            _screenBorderSystem.SetObjectToStartingState();
        }

        private void OnEnable()
        {
            _screenBorderSystem.SetStartingPosition(transform);
        }
    }
}