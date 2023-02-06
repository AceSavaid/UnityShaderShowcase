using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private Camera _camera;

    [Header("UI")] 
    [SerializeField] private TextMeshProUGUI _fuelText;

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

        _control.KeyboardMouse.Shoot.performed += ctx => isShooting = true;
        _control.KeyboardMouse.Shoot.canceled += ctx => isShooting = false;

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
        UpdateFuel();
    }

    void Move()
    {
        _rb.AddForce(new Vector3(_movementDir.x * _speed, 0 , _movementDir.y * _speed), ForceMode.Force);

        if (_rb.velocity.x >= 240.0f)
        {
            _rb.velocity = new Vector3(240.0f, 0.0f, _rb.velocity.z);
        }
        
        if (_rb.velocity.z >= 240.0f)
        {
            _rb.velocity = new Vector3(_rb.velocity.x, 0.0f, 240.0f);
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

    void UpdateFuel()
    {
        _fuelText.text = "Fuel: " + (int)_currentFuel;

        _currentFuel -= Time.deltaTime * _fuelLossMultiplier * 2.0f;
    }
}
