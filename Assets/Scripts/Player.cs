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


    [Header("Player Stats")]
    [SerializeField] float maxHealth = 20;
    [SerializeField] float currentHealth;
    [SerializeField] float invincibilityTime;
    bool hurt = false;
    float invincibilityTimer = 0.0f;
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


        //healthbar setUp
        currentHealth = maxHealth;
        healthbar.maxValue = maxHealth;
        healthbar.value = currentHealth;
    }


    private void OnEnable()
    {
        controls.Enable();
        controls.Player.Move.performed += ctx =>
        {
            Vector2 v = ctx.ReadValue<Vector2>();
            moveDirection.x = v.y;
            moveDirection.y = v.x;
        };
        controls.Player.Fire.performed += ctx => Shoot();
        controls.Player.Jump.performed += ctx => Jump();

    }

    private void OnDisable()
    {
        controls.Disable();
    }
    // Update is called once per frame
    void Update()
    {
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
    }


    void Shoot(){
        if (canshoot)
        {
            //instantiate bullet from current weapon and projects it forward
            GameObject b = Instantiate(currentWeapon.projectile);
            b.GetComponent<Rigidbody>().AddForce(Vector3.forward * currentWeapon.bulletSpeed);

            canshoot = false;
        }
        

    }

    protected void Movement()
    {

       
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            isGrounded = false;
        }

    }

}
