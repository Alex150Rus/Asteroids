using System;
using Asteroids.WeaponSystems;
using UnityEngine;

namespace Asteroids.Ammo
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ammo: MonoBehaviour, IAmmo
    {
        [SerializeField] private float _velocity;
        
        private Rigidbody2D _playerRigidBody;
        private Transform _startingPoint;
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _startingPoint = FindObjectOfType<AmmoStartingPoint>().transform;
            _playerRigidBody = FindObjectOfType<Player>().gameObject.GetComponent<Rigidbody2D>();
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