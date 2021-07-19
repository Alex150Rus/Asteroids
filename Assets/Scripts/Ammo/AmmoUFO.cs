using System;
using Asteroids.Common;
using Asteroids.WeaponSystems;
using UnityEngine;
using UnityEngine.Rendering;

namespace Asteroids.Ammo
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class AmmoUFO : MonoBehaviour, IAmmo
    {
        [SerializeField] private float _velocity;

        private Rigidbody2D _playerRigidBody;
        private AmmoStartPointUFO _startingPoint;
        private Rigidbody2D _rigidbody;
        private ICompareDistanceWithScreenWidth _screenBorderSystem;

        public event Action<Transform> OnScreenBorder;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _startingPoint = FindObjectOfType<AmmoStartPointUFO>();
            _playerRigidBody = FindObjectOfType<Player>().gameObject.GetComponent<Rigidbody2D>();
            _screenBorderSystem = new ScreenBorderSystem(transform);
        }

        private void Update()
        {
            if (_screenBorderSystem.IsDistanceEqualScreenWith(transform))
                OnScreenBorder?.Invoke(transform);
            _screenBorderSystem.ScreenBorderWork(transform);
        }

        public void Fly()
        {
            gameObject.SetActive(true);
            MoveToStartingPoint();
            AddVelocity();
        }

        private void MoveToStartingPoint()
        {
            transform.position = _startingPoint.transform.position;
        }

        private void AddVelocity()
        {
            _rigidbody.velocity =(_playerRigidBody.transform.position - _rigidbody.transform.position).normalized 
                                 * (_velocity + _playerRigidBody.velocity.magnitude);
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