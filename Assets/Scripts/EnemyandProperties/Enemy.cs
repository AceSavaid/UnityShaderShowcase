using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float maxHealth = 20;
    [SerializeField] float currentHealth;
    [SerializeField] float damage = 2.0f;
    [SerializeField] Slider healthbar;


    Transform playerLoc;
    bool followPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            collision.gameObject.GetComponent<Player>().HurtPlayer(damage);
        }
    }
}
