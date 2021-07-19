using System;
using Asteroids.Common;
using Asteroids.MoveSystems;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids.AsteroidSystems
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Asteroid : MonoBehaviour, IMovable
    {
        public AsteroidType asteroidType;
        public event Action<Transform> OnPlayerUFOCollisionEnter;
        public event Action<Transform> OnAmmoCollisionEnter;
        
        [SerializeField] private float _minspeed;
        [SerializeField] private float _maxSpeed;
        private float _speed;
        private Vector2 _direction;
        private Rigidbody2D _rigidbody;
        private IMoveRigidbody _moveSystem;
        private SetStartingMovablesPosition _setStartingMovablesPosition;
        private ScreenBorderSystem _screenBorderSystem; 
        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public Vector2 Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        public Rigidbody2D Rigidbody => _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _moveSystem = new MoveRigidbody();
            
        }

        private void OnEnable()
        {
            _setStartingMovablesPosition = new SetStartingMovablesPosition();
            _setStartingMovablesPosition.SetPosition(this.transform);
            _screenBorderSystem = new ScreenBorderSystem(transform);
            _speed = Random.Range(_minspeed, _maxSpeed);
            _direction = new Vector2(Random.Range(0.01f, 1f),Random.Range(0.01f, 1f));
            _moveSystem.Move(this);
        }

        private void Update()
        {
            _screenBorderSystem.ScreenBorderWork(transform);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(NamesManager.PLAYER_TAG) || other.CompareTag(NamesManager.UFO_TAG))
            {
                OnPlayerUFOCollisionEnter?.Invoke(transform);
            }
            
            if (other.CompareTag(NamesManager.AMMO_TAG))
            {
                OnAmmoCollisionEnter?.Invoke(transform);
            }
        }
    }
}