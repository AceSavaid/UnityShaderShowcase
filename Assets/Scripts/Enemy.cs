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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
