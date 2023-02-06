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
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _maxFuel;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _shootCoolDown;
    [SerializeField] private Transform _gunPos;
    [SerializeField] private Rigidbody _bullet;
    [SerializeField] private Camera _camera;

    [Header("UI")] 
    [SerializeField] private TextMeshProUGUI _fuelText;
    [SerializeField] private TextMeshProUGUI _distanceText;
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject InGameMenu;
    [SerializeField] private GameObject LossMenu;
    [SerializeField] private GameObject WinScreen;

    private bool isShooting;
    private float _currentFuel;
    private float _fuelLossMultiplier;
    private float _travelledDistance = 0.0f;
    private float _maxDistance = 4000.0f;

    private Vector2 _movementDir;

    private Vector3 temp;

    [SerializeField] private ParticleSystem _Particle1;

    private void Awake()
    {
        temp = transform.position;
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

        _control.KeyboardMouse.Pause.started += ctx =>
        {
            PauseMenu.SetActive(true);
            InGameMenu.SetActive(false);
            Time.timeScale = 0;
        };
        
        _control.KeyboardMouse.Pause.canceled += ctx =>
        {
            PauseMenu.SetActive(false);
            InGameMenu.SetActive(true);
            Time.timeScale = 1.0f;
        };

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

    private void Update()
    {
        _camera.fieldOfView = 70.0f + (5.0f * (_rb.velocity.magnitude /_maxSpeed));

        if (_currentFuel <= 0)
        {
            OnDisable();
            
            LossMenu.SetActive(true);
            InGameMenu.SetActive(false);
        }

        if (_travelledDistance >= _maxDistance)
        {
            _currentFuel = 100.0f;

            WinScreen.SetActive(true);
            InGameMenu.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        Move();
        CheckShoot();
        UpdateFuel();
        UpdateDistance();
    }

    void Move()
    {
        _rb.AddForce(new Vector3(_movementDir.x * _speed, 0 , _movementDir.y * _speed), ForceMode.Force);

        if (_rb.velocity.x >= _maxSpeed)
        {
            _rb.velocity = new Vector3(_maxSpeed, 0.0f, _rb.velocity.z);
        }
        
        if (_rb.velocity.z >= _maxSpeed)
        {
            _rb.velocity = new Vector3(_rb.velocity.x, 0.0f, _maxSpeed);
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

    void UpdateDistance()
    {
        _distanceText.text = "Distance Travelled: " + (int)_travelledDistance + " / " + (int)_maxDistance;

        _travelledDistance += Vector3.Distance(transform.position, temp);
        temp = transform.position;

    }
}
