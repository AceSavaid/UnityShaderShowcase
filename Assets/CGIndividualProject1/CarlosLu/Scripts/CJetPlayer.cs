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
    [SerializeField] private float _shootSpeed;

    private Vector2 _movementDir;
    
    [SerializeField] private ParticleSystem _Particle1;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
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
        #region HandleControls

        _control = new Control();
        _control.Enable();

        _control.KeyboardMouse.Movement.performed += ctx =>
        {
            Vector2 v = ctx.ReadValue<Vector2>();

            _movementDir.x = v.x;
            _movementDir.y = v.y;
        };

        #endregion

    }

    private void FixedUpdate()
    {
        Move();
        Shoot();
    }

    void Move()
    {
        
    }

    void Shoot()
    {
        
    }
}
