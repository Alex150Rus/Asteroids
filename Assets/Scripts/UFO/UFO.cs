using System;
using System.Collections;
using System.Collections.Generic;
using Asteroids.Common;
using Asteroids.MoveSystems;
using Asteroids.WeaponSystems;
using UnityEngine;

namespace Asteroids.UFO
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class UFO: MonoBehaviour, IMovable
    {

        
        [SerializeField] private int _percentageOfBorderMargin;
        [SerializeField] private int _timeBetweenSpawn;
        [SerializeField] private WeaponUFO _weaponUfo;

        public int TimeBetweenSpawn => _timeBetweenSpawn;

        private SetStartingUFOposition _setStartingUfOposition;
        private int _sign = -1;
        private IMoveRigidbody _moveRigidbody;
        private PlayerScreenBorderWork _playerScreenBorderWork;
        public float Speed { get; set; }
        public Vector2 Direction { get; set; }
        public Rigidbody2D Rigidbody { get; private set; }

        public event Action OnCollision;

        private void Awake()
        {
            _moveRigidbody = new MoveRigidbody();
            _setStartingUfOposition = new SetStartingUFOposition(_percentageOfBorderMargin);
            _playerScreenBorderWork = new PlayerScreenBorderWork();
            Rigidbody = GetComponent<Rigidbody2D>();
            Speed = ((Vector2) Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0))).x * 2 / 10;
        }

        private void Update()
        {
            if (gameObject.activeSelf)
            {
                //_weaponUfo.Fire();
            
                _playerScreenBorderWork.ScreenBorderWork(transform);    
            }
            
        }

        public void StartMoving()
        {
            gameObject.SetActive(true);
            var prevSign = _sign;
            _sign = _setStartingUfOposition.SetPosition(transform);
            if (prevSign != _sign)
                transform.Rotate(transform.up, 180);;
            
            Direction = transform.right;
            _moveRigidbody.Move(this);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(NamesManager.ASTEROID_TAG) || 
                other.CompareTag(NamesManager.AMMO_TAG))
            {
                OnCollision?.Invoke();
                gameObject.SetActive(false);
            }
        }
    }
}