using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Asteroids.Common;
using Asteroids.Input;
using Asteroids.MoveSystems;
using Asteroids.RotateSystems;
using Asteroids.UI;
using Asteroids.WeaponSystems;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Asteroids
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class Player : MonoBehaviour
    {
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _acceleration;
        [SerializeField] private float _unhurtableTime;
        [SerializeField] private int _flickerFrequencyPerSecond; 
        [SerializeField] private Weapon _weapon;
        [SerializeField] private MainMenu _mainMenu;
        private IInput _input;
        private IMove _move;
        private IRotate _rotate;
        private PlayerScreenBorderWork _playerScreenBorderWork;
        private bool isUnhurtable = true;
        private SpriteRenderer _spriteRenderer;
        private SpriteRenderer _spriteRendererWeapon;
        private Sound _sound;

        private void Awake()
        {
            if(!PlayerPrefs.HasKey(NamesManager.CONTROL_TYPE_KEY))
                PlayerPrefs.SetInt(NamesManager.CONTROL_TYPE_KEY, (int)InputType.KeyBoard);
            _input = new InputAbstractFactory().Create((InputType)PlayerPrefs.GetInt(NamesManager.CONTROL_TYPE_KEY));
            AddHandlersToInputEvents();
            var rigidBody = GetComponent<Rigidbody2D>();
            _move = new MovePhysicsWithInertia(rigidBody);
            _rotate = new RotateByPhysics(rigidBody);
            _playerScreenBorderWork = new PlayerScreenBorderWork();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRendererWeapon = _weapon.GetComponent<SpriteRenderer>();
            _sound = GetComponent<Sound>();
        }

        private void Start()
        {
            StartCoroutine(UnHurtable());
            StartCoroutine(Flick());
        }
        
        private IEnumerator UnHurtable()
        {
            while (Time.timeScale == 0)
            {
                yield return null;
            }

            isUnhurtable = true;
            yield return new WaitForSecondsRealtime(_unhurtableTime);
            isUnhurtable = false;
        }

        private IEnumerator Flick()
        {
            while (Time.timeScale == 0)
            {
                yield return null;
            }

            var time = 1 / _flickerFrequencyPerSecond;
            while (isUnhurtable)
            {
                _spriteRenderer.color = 
                    new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 0f);
                _spriteRendererWeapon.color = 
                    new Color(_spriteRendererWeapon.color.r, _spriteRendererWeapon.color.g, _spriteRendererWeapon.color.b, 0f);
                yield return new WaitForSecondsRealtime(time);
                _spriteRenderer.color = 
                    new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 1f);
                _spriteRendererWeapon.color = 
                    new Color(_spriteRendererWeapon.color.r, _spriteRendererWeapon.color.g, _spriteRendererWeapon.color.b, 1f);
                yield return null;
            }
        }

        private void Update()
        {
            _input.UpdateExecute();
            _playerScreenBorderWork.ScreenBorderWork(transform);
        }

        private void FixedUpdate()
        { 
            _input.Execute(Time.deltaTime);
        }

        private void Move (float horizontal, float vertical)
        {
            if (vertical > 0)
            {
                _sound.PlayByIndex(0);
            }
            
            var deltaTime = Time.deltaTime;
            _move.Move(deltaTime, horizontal, vertical, _acceleration, _maxSpeed);
        }

        private void Rotate(float torque, float y)
        {
            _rotate.Rotate(-torque * _rotationSpeed * Time.deltaTime);
        }

        private void OnDisable()
        {
            _input.OnAxisChange -= Move;
            _input.OnAxisChange -= Rotate;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(!isUnhurtable) {
                if (other.CompareTag(NamesManager.ASTEROID_TAG) || 
                    other.CompareTag(NamesManager.UFO_TAG) ||
                other.CompareTag(NamesManager.UFO_BULLET_TAG))
                {
                    gameObject.SetActive(false);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }

        public void ChangeControlSchemeOnyTheFly()
        {
            _input.Dispose();
            _input = new InputAbstractFactory().Create((InputType)PlayerPrefs.GetInt(NamesManager.CONTROL_TYPE_KEY));
            AddHandlersToInputEvents();
        }

        private void AddHandlersToInputEvents()
        {
            _input.OnAxisChange += Move;
            _input.OnAxisChange += Rotate;
            _input.OnKeyPressed += _weapon.Fire;
            _input.OnEscapeKeyPressed += _mainMenu.ToggleMenu;
        }
    }
}