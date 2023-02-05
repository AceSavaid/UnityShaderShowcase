using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    Rigidbody rb;
    PlayerAct controls;

    [SerializeField] private Transform head;
    [SerializeField, Range(0,89.9f)] private float viewLockY;
    private Vector2 intendedDirection;
    private Vector2 mouseDir;
    public float mouseSensitivity;


    [Header("Player Stats")]
    [SerializeField] float maxHealth = 20;
    [SerializeField] float currentHealth;
    [SerializeField] float invincibilityTime;
    bool hurt = false;
    float invincibilityTimer = 0.0f;
    [SerializeField] float speed = 4;
    [SerializeField] float jumpHeight = 4;
    [SerializeField] Weapon currentWeapon;
    bool canshoot = true;
    float shootTimer = 0.0f;

    [Header("UI Elements")]
    [SerializeField] Slider healthbar;
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject deathScreen;
    [SerializeField] GameObject HealOverlay;
    [SerializeField] GameObject HurtOverlay;


    [Header("Sound")]
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip hurtSound;
    [SerializeField] AudioClip healSound;
    [SerializeField] AudioClip dieSound;
    [SerializeField] AudioClip shootSound;

    Vector2 mouseDirection;
    Vector2 moveDirection;

    bool isGrounded = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        controls = new PlayerAct();
        controls.Enable();
        controls.Player.Look.performed += ctx => mouseDir = ctx.ReadValue<Vector2>();
        controls.Player.Move.performed += ctx =>
        {
            Vector2 v = ctx.ReadValue<Vector2>();
            moveDirection.x = v.y;
            moveDirection.y = v.x;
        };
       //controls.Player.Fire.performed += ctx => Shoot();
        controls.Player.Jump.performed += ctx => Jump();


        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //healthbar setUp
        currentHealth = maxHealth;
        healthbar.maxValue = maxHealth;
        healthbar.value = currentHealth;
    }


    private void OnEnable()
    {
        

    }

    private void OnDisable()
    {
        controls.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        Movement();
        RotateCamera(); 

        if (hurt)
        {
            invincibilityTimer += Time.deltaTime;
            if (invincibilityTimer >= invincibilityTime)
            {
                hurt = false;
                invincibilityTimer = 0.0f;
            }
        }

        if (!canshoot)
        {
            shootTimer += Time.deltaTime;
            if (shootTimer >= currentWeapon.ReloadTime)
            {
                canshoot = true;
                shootTimer = 0.0f;
            }
        }

        intendedDirection = mouseDir;
        UpdateUI();


    }

    public void HurtPlayer(float val){
        //if player is not hurt i.e is not in invincibility frames
        if (!hurt)
        {
            currentHealth -= val;
            if (currentHealth <= 0)
            {
                Die();
            }
        }
        

    }

    public void HealPlayer(float val){
        currentHealth += val;
        if(currentHealth > maxHealth){
            currentHealth = maxHealth;
        }

    }

    public void Die(){
        
        //turn on DeathScreenUI
        deathScreen.gameObject.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }


    void Shoot(){
        if (canshoot)
        {
            //instantiate bullet from current weapon and projects it forward
            GameObject b = Instantiate(currentWeapon.projectile, transform);
            b.transform.parent = null;
            b.GetComponent<Rigidbody>().AddForce(Vector3.forward * currentWeapon.bulletSpeed);
            Destroy(b, 5);
            canshoot = false;
        }
        
    }

    protected void Movement()
    {
        rb.AddForce(speed * moveDirection.x * transform.forward, ForceMode.Force);
        rb.AddForce(speed * moveDirection.y * transform.right, ForceMode.Force);
    }

    void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(jumpHeight * Vector3.up, ForceMode.Impulse);
        }
        
    }

    public void ChangeWeapon(Weapon w)
    {
        currentWeapon = w;
    }

    void UpdateUI()
    {
        healthbar.value = currentHealth;
    }

    public void WinLevel()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        winScreen.SetActive(true);
    }

    private void RotateCamera()
    {

        transform.rotation *= Quaternion.AngleAxis(intendedDirection.x * mouseSensitivity * Time.deltaTime, Vector3.up);
        head.rotation *= Quaternion.AngleAxis(intendedDirection.y * mouseSensitivity * Time.deltaTime, Vector3.right);

        Vector3 angles = Vector3.zero;

        angles.x = head.localEulerAngles.x;

        if (angles.x > 180 && angles.x < 360 - viewLockY)
        {
            angles.x = 360 - viewLockY;
        }
        else if (angles.x < 180 && angles.x > viewLockY)
        {
            angles.x = viewLockY;
            angles.x = viewLockY;
        }


        head.localEulerAngles = angles;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isGrounded = false;
        }

    }

}
