using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.AI;

public class CJetPlayer : MonoBehaviour
{
    Rigidbody _rb;
    private Control _control;

    [Header("Jet Attributes")] 
    [SerializeField] private float _speed;
    [SerializeField] private float _maxFuel;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _shootCoolDown;
    [SerializeField] private Transform _gunPos;
    [SerializeField] private Rigidbody _bullet;

    private bool isShooting;
    private float _currentFuel;
    private float _fuelLossMultiplier;

    private Vector2 _movementDir;
    
    [SerializeField] private ParticleSystem _Particle1;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _currentFuel = _maxFuel;
        
        #region HandleControls

        _control = new Control();
        _control.Enable();

        _control.KeyboardMouse.Movement.performed += ctx =>
        {
            Vector2 v = ctx.ReadValue<Vector2>();

            _movementDir.x = v.x;
            _movementDir.y = v.y;

            _fuelLossMultiplier = 0.87f;
        };

        _control.KeyboardMouse.Movement.canceled += ctx =>_fuelLossMultiplier = 0.25f;

        _control.KeyboardMouse.Shoot.performed += ctx => isShooting = ctx.ReadValue<bool>();

        #endregion
    }

    private void OnEnable()
    {
        _control.KeyboardMouse.Enable();
        _control.UI.Disable();
    }

    private void OnDisable()
    {
        _control.KeyboardMouse.Disable();
        _control.UI.Enable();
    }

    private void FixedUpdate()
    {
        Move();
        CheckShoot();
    }

    void Move()
    {
        _rb.AddForce(new Vector3(_movementDir.x * _speed, 0 , _movementDir.y * _speed), ForceMode.Force);

        if (_rb.velocity.x >= 120.0f)
        {
            _rb.velocity = new Vector3(10.0f, 0.0f, _rb.velocity.z);
        }
        
        if (_rb.velocity.z >= 120.0f)
        {
            _rb.velocity = new Vector3(_rb.velocity.x, 0.0f, 10.0f);
        }
    }

    private float _shootTemp;
    void CheckShoot()
    {
        if (_shootTemp >= _shootCoolDown && isShooting)
        {
            _shootTemp = 0.0f;

            var _bulletPrefab = Instantiate(_bullet, _gunPos);
            _bulletPrefab.AddForce(Vector3.forward * _bulletSpeed, ForceMode.Impulse);
        }
        else
        {
            _shootTemp += Time.deltaTime;
        }
    }
}
