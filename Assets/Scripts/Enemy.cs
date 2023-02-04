using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float maxHealth = 20;
    [SerializeField] float currentHealth;
    [SerializeField] float damage = 2.0f;
    [SerializeField] float speed = 4.0f;
    [SerializeField] Slider healthbar;

    Transform playerLoc;
    bool followPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void Movement()
    {

    }

    public void HurtEnemy(float val)
    {
        currentHealth -= val;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(this.gameObject);
    }
    public float GetDamage()
    {
        return damage;
    }
}
