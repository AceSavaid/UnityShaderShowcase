using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    Rigidbody rb;
    PlayerAct controls;

    [SerializeField] private Transform head;
    //[SerializeField, Range(0,89.9f)] private float viewLockY;
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

    [Header("Player Elements")]
    [SerializeField] GameObject forcefield;
    

    [Header("UI Elements")]
    [SerializeField] Slider healthbar;
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject deathScreen;
    [SerializeField] GameObject HealOverlay;
    [SerializeField] GameObject HurtOverlay;
    [SerializeField] GameObject textBox;
    [SerializeField] TMP_Text textBoxText;


    [Header("Sound")]
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip hurtSound;
    [SerializeField] AudioClip healSound;
    [SerializeField] AudioClip dieSound;
    [SerializeField] AudioClip shootSound;
    [SerializeField] AudioClip weaponChangeSound;
    [SerializeField] AudioClip forcefieldSound;

    [Header("ParticleEffects")]
    [SerializeField] ParticleSystem jumpParticles;
    [SerializeField] ParticleSystem landParticles;
    [SerializeField] ParticleSystem hurtParticles;
    [SerializeField] ParticleSystem deathParticles;

    [Header("Other")]

    Vector2 moveDirection;

    bool isGrounded = true;
    bool isPaused = false;

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
       controls.Player.Fire.performed += ctx => Shoot();
        controls.Player.Jump.performed += ctx => Jump();
        controls.Player.Pause.performed += ctx => PauseGame();


        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //healthbar setUp
        currentHealth = maxHealth;
        healthbar.maxValue = maxHealth;
        healthbar.value = currentHealth;
    }


    private void OnEnable()
    {

        controls.Enable();
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
            if (currentHealth <= 0)
            {
                Die();
            }
            else
            {
                currentHealth -= val; 
                PlayVisualEffect(hurtParticles);
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
        
        PlayVisualEffect(deathParticles);

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
            b.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * currentWeapon.bulletSpeed);
            b.gameObject.transform.parent = null;
            Destroy(b, 5);
            canshoot = false;
        }
        
    }

    protected void Movement()
    {
        rb.AddForce(speed* moveDirection.x * transform.forward, ForceMode.Acceleration);
        rb.AddForce(speed * moveDirection.y * transform.right, ForceMode.Acceleration);
        //gameObject.transform.localPosition += new Vector3(moveDirection.y, 0, moveDirection.x)* speed * Time.deltaTime;
       

    }

    void Jump()
    {
        if (isGrounded)
        {
            PlaySoundEffect(jumpSound);
            PlayVisualEffect(jumpParticles);
            rb.AddForce(jumpHeight * Vector3.up, ForceMode.Impulse);
        }
        
    }

    void ToggleForceField(bool toggle)
    {
        PlaySoundEffect(forcefieldSound);
        forcefield.SetActive(toggle);
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
        transform.rotation *= Quaternion.Euler(intendedDirection.x * mouseSensitivity * Time.deltaTime * Vector3.up);
        head.rotation *= Quaternion.AngleAxis(-intendedDirection.y * mouseSensitivity * Time.deltaTime, Vector3.right);

        Vector3 angles = Vector3.right * head.localEulerAngles.x;

        if (angles.x < -180) // I can see that you are trying to set limit for camera angle - fixed
        {
            angles.x = -180;
        }
        
        if (angles.x > 360)
        {
            angles.x = 180;
        }


        head.localEulerAngles = angles;
    }
    
    public void PauseGame()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            PauseMenu.SetActive(false);
            Time.timeScale = 1;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void ToggleDebug()
    {

    }

    public void ShowTextBox(string text)
    {
        textBoxText.text = text;
        textBox.SetActive(true);
    }
    
    public void HideTextBox()
    {
        textBox.SetActive(false);
    }

    private void PlaySoundEffect(AudioClip sound)
    {
        if (sound)
        {
            AudioSource.PlayClipAtPoint(sound, this.transform.position);
        }
    }

    private void PlayVisualEffect(ParticleSystem particle)
    {
        if (particle)
        {
            particle.Clear();
            particle.Play();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isGrounded = true;
        }
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            HurtPlayer(0.1f);
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
