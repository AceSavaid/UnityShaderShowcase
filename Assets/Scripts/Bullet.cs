using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool enemyBullet;
    public float damage = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.layer == 7 && !enemyBullet)
        {
            collision.gameObject.GetComponent<Enemy>().HurtEnemy(damage);
        }
        if (collision.gameObject.layer == 3 && enemyBullet)
        {
            collision.gameObject.GetComponent<Player>().HurtPlayer(damage);
        }

        Destroy(gameObject);
    }
}
