using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    Rigidbody rb;
    //PlayerControls controls;


    [Header("Player Stats")]
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;
    [SerializeField] float jumpHeight;
    [SerializeField] Weapon currentWeapon;

    [Header("UI Elements")]
    [SerializeField] Slider healthbar;
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject clearScreen;
    [SerializeField] GameObject deathScreen;


    [Header("Sound")]
    AudioClip jumpSound;
    AudioClip shootSound;

    Vector2 mouseDirection;

    bool isGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HurtPlayer(float val){
        currentHealth -= val;

    }

    public void HealPlayer(float val){
        currentHealth += val;
        if(currentHealth > maxHealth){
            currentHealth = maxHealth;
        }

    }


    void Shoot(){
        GameObject b = Instantiate(currentWeapon.projectile);
        

    }
}
