using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Asteroids.Common;
using Asteroids.Input;
using Asteroids.MoveSystems;
using Asteroids.RotateSystems;
using Asteroids.WeaponSystems;
using UnityEngine;

namespace Asteroids
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class Player : MonoBehaviour
    {
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _acceleration;
        [SerializeField] private Weapon _weapon;
        private IInput _input;
        private IMove _move;
        private IRotate _rotate;
        private PlayerScreenBorderWork _playerScreenBorderWork;

        private void Awake()
        {
            _input = new InputAbstractFactory().Create(InputType.KeyBoard);
            _input.OnAxisChange += Move;
            _input.OnAxisChange += Rotate;
            _input.OnKeyPressed += _weapon.Fire;
            var rigidBody = GetComponent<Rigidbody2D>();
            _move = new MovePhysicsWithInertia(rigidBody);
            _rotate = new RotateByPhysics(rigidBody);

            _playerScreenBorderWork = new PlayerScreenBorderWork();

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
            var deltaTime = Time.deltaTime;
            _move.Move(deltaTime, horizontal, vertical, _acceleration, _maxSpeed);
        }

        private void Rotate(float torque, float y)
        {
            _rotate.Rotate(torque * _rotationSpeed * Time.deltaTime);
        }

        private void OnDisable()
        {
            _input.OnAxisChange -= Move;
            _input.OnAxisChange -= Rotate;
        }
    }
}